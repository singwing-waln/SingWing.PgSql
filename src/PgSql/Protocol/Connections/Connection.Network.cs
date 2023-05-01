using SingWing.PgSql.Balancing;
using SingWing.PgSql.Protocol.Messages.Frontend;

namespace SingWing.PgSql.Protocol.Connections;

internal sealed partial class Connection
{
    /// <summary>
    /// Opens a new connection to the specified database node, 
    /// and completes the authentication for the database user.
    /// </summary>
    /// <param name="node">The node to connect to.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A connection that has been authenticated.</returns>
    /// <exception cref="OpeningFailedException">Failed to open the connection or authenticate the user.</exception>
    internal static async ValueTask<Connection> OpenAsync(
        Node node,
        CancellationToken cancellationToken)
    {
        var host = node.Server.Host;

        if (host.IsUnixEndPoint)
        {
            var (connection, exception, reason) = await ConnectAndAuthenticateAsync(
                node,
                host.ResolveUnixEndPoint(),
                cancellationToken);

            if (connection is not null)
            {
                return connection;
            }

            throw new OpeningFailedException(node, reason!.Value, exception);
        }
        else
        {
            Exception? exception = null;
            var reason = OpeningFailedReason.ConnectionFailed;

            var endPoints = await host.ResolveIPEndPointsAsync(cancellationToken);

            foreach (var endPoint in endPoints)
            {
                var (connection, innerException, innerReason) = await ConnectAndAuthenticateAsync(
                    node,
                    endPoint,
                    cancellationToken);

                if (connection is not null)
                {
                    return connection;
                }

                if (innerReason == OpeningFailedReason.AuthenticationFailed)
                {
                    exception = innerException ?? exception;
                    reason = OpeningFailedReason.AuthenticationFailed;
                }
                else
                {
                    exception ??= innerException;
                }
            }

            throw new OpeningFailedException(node, reason, exception);
        }

        static async ValueTask<(Connection?, Exception?, OpeningFailedReason?)> ConnectAndAuthenticateAsync(
            Node node,
            EndPoint point,
            CancellationToken cancellationToken)
        {
            var (socket, exception) = await ConnectAsync(node, point, cancellationToken);

            if (socket is null)
            {
                return (null, exception, OpeningFailedReason.ConnectionFailed);
            }

            try
            {
                var connection = new Connection(node, socket);
                await connection.AuthenticateAsync(node.Database, cancellationToken);
                if (connection.IsReady)
                {
                    return (connection, null, null);
                }
            }
            catch (Exception exc)
            {
                exception = exc;
            }

            socket.Close();
            return (null, exception, OpeningFailedReason.AuthenticationFailed);

            static async ValueTask<(Socket?, Exception?)> ConnectAsync(
                Node node, 
                EndPoint point, 
                CancellationToken cancellationToken)
            {
                var socket = CreateSocket(point);

                try
                {
                    SetSocketOptions(socket);

                    using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    cts.CancelAfter(node.ConnectTimeoutSeconds * 1000);

                    await socket.ConnectAsync(point, cts.Token);

                    return (socket, null);
                }
                catch (Exception exc)
                {
                    socket.Close();
                    return (null, exc);
                }
            }

            static Socket CreateSocket(EndPoint endPoint)
            {
                var family = endPoint.AddressFamily;
                var type =
                    family is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6
                    ? ProtocolType.Tcp          // TCP
                    : ProtocolType.Unspecified; // Unix Domain Socket

                return new Socket(family, SocketType.Stream, type);
            }

            static void SetSocketOptions(Socket socket)
            {
                if (socket.AddressFamily == AddressFamily.InterNetwork || 
                    socket.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    // Disable Nagle algorithm to improve real-time performance.
                    socket.NoDelay = true;
                }

                // According to TcpClient.ReceiveBufferSize:
                // 
                // Your network buffer should be at least as large as your application buffer
                // to ensure that the desired data will be available when you call the Read method. 
                // 
                // If the network buffer is smaller than the amount of data you request in the Read method,
                // you will not be able to retrieve the desired amount of data in one read operation.
                // This incurs the overhead of additional calls to the Read method.
                if (socket.ReceiveBufferSize < Reader.BufferSize)
                {
                    socket.ReceiveBufferSize = Reader.BufferSize;
                }

                // According to TcpClient.SendBufferSize:
                // 
                // Your network buffer should be at least as large as your application buffer
                // to ensure that the desired data will be stored and sent in one operation.
                // 
                // If the network buffer is smaller than the amount of data you provide the Write method,
                // several network send operations will be performed for every call you make to the Write method.
                // You can achieve greater data throughput by ensuring that your network buffer is
                // at least as large as your application buffer.
                if (socket.SendBufferSize < Writer.BufferSize)
                {
                    socket.SendBufferSize = Writer.BufferSize;
                }
            }
        }
    }

    /// <summary>
    /// Closes the connection.
    /// </summary>
    internal void Close()
    {
        if (Interlocked.CompareExchange(ref _closed, 1, 0) == 1)
        {
            return;
        }

        try
        {
            Rows.OwnsConnection = false;
            Rows.Dispose();
            Transaction.OwnsConnection = false;
            Transaction.Dispose();
            SendTerminateMessage();
        }
        catch (Exception exc)
        {
            Db.Logger.LogError(exc.Message, exc);
        }

        try
        {
            Socket.Shutdown(SocketShutdown.Both);
        }
        catch (Exception exc)
        {
            Db.Logger.LogError(exc.Message, exc);
        }
        finally
        {
            Socket.Close();
            Reader.Dispose();
            Writer.Dispose();
            Error = null;
            Node.Remove(Id);
        }

        void SendTerminateMessage()
        {
            if (IsBroken)
            {
                return;
            }

            Writer.SendTerminateMessageAsync()
                .AsTask().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }

    /// <summary>
    /// Tests that the current connection remains connected to the server.
    /// </summary>
    /// <returns><see langword="true"/> if the connection is still connected to the server, <see langword="false"/> otherwise.</returns>
    /// <remarks>
    /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.connected?view=net-7.0"/>.
    /// </remarks>
    internal async ValueTask<bool> TestAsync()
    {
        if (!Socket.Connected)
        {
            return false;
        }

        // The value of the Connected property reflects the state of the connection as of the most recent operation.
        // If you need to determine the current state of the connection, make a nonblocking, zero-byte Send call.
        // If the call returns successfully or throws a WAEWOULDBLOCK error code (10035), then the socket is still connected;
        // otherwise, the socket is no longer connected.

        // We hope that the test time does not last long.
        const int TestTimeout = 5 * 1000;
        using var cts = new CancellationTokenSource();

        try
        {
            cts.CancelAfter(TestTimeout);
            await Socket.SendAsync(Array.Empty<byte>(), cts.Token);
            return true;
        }
        catch (SocketException exc)
        {
            return exc.SocketErrorCode == SocketError.WouldBlock;
        }
        catch
        {
            return false;
        }
    }
}
