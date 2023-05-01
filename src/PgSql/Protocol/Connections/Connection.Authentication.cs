using SingWing.PgSql.Balancing;
using SingWing.PgSql.Protocol.Messages;
using SingWing.PgSql.Protocol.Messages.Backend;
using SingWing.PgSql.Protocol.Messages.Frontend;

namespace SingWing.PgSql.Protocol.Connections;

internal sealed partial class Connection
{
    /// <summary>
    /// Performs authentication.
    /// If the authentication is successful, the <see cref="IsReady"/> should be <see langword="true"/>.
    /// </summary>
    /// <param name="database">The <see cref="Database"/> to authenticate.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    private async ValueTask AuthenticateAsync(
        Database database,
        CancellationToken cancellationToken)
    {
        // 1. To begin a session, a frontend opens a connection to
        // the server and sends a startup message.
        // https://www.postgresql.org/docs/current/protocol-flow.html#id-1.10.5.7.3
        await Writer.SendStartupMessageAsync(
            database.DatabaseName,
            database.UserName,
            cancellationToken);

        // 2. The server then sends an appropriate authentication
        // request message...
        var message = await Reader.ReadMessageAsync(cancellationToken);
        if (message is AuthenticationOk)
        {
            // After having received AuthenticationOk,
            // the frontend must wait for further messages from the server.
            await WaitReadyForAuthenticationAsync(cancellationToken);
            return;
        }

        // 3. If the frontend does not support the authentication method
        // requested by the server, then it should immediately close the connection.
        if (message is not AuthenticationSasl)
        {
            return;
        }

        // 4. Send client first message(SASLInitialResponse) for SCRAM-SHA-256.
        // https://www.rfc-editor.org/rfc/rfc7677.html#section-3
        // https://www.postgresql.org/docs/current/sasl-authentication.html
        var (clientFirstMessageBare, clientNonce) =
            await Writer.SendClientFirstMessageAsync(cancellationToken);

        // 5. Receive server first message(AuthenticationSASLContinue).
        message = await Reader.ReadMessageAsync(cancellationToken);
        if (message is not AuthenticationSaslContinue serverFirstMessage ||
            !serverFirstMessage.Verify(clientNonce))
        {
            return;
        }

        // 6. Send client final message(SASLResponse).
        var serverSignature = await Writer.SendClientFinalMessageAsync(
            database.Password,
            clientFirstMessageBare,
            serverFirstMessage,
            cancellationToken);

        // 7. Receive server final message(AuthenticationSASLFinal).
        message = await Reader.ReadMessageAsync(cancellationToken);
        if (message is not AuthenticationSaslFinal serverFinalMessage)
        {
            return;
        }

        // 8. The client then authenticates the server by computing the
        // ServerSignature and comparing it to the value sent by the server.
        // https://www.rfc-editor.org/rfc/rfc5802#section-5
        if (!serverFinalMessage.Verify(serverSignature))
        {
            return;
        }

        // 9. Server sends an AuthenticationSASLFinal message,
        // with the SCRAM server-final-message, followed immediately
        // by an AuthenticationOk message.
        // https://www.postgresql.org/docs/current/sasl-authentication.html#SASL-SCRAM-SHA-256
        message = await Reader.ReadMessageAsync(cancellationToken);
        if (message is AuthenticationOk)
        {
            await WaitReadyForAuthenticationAsync(cancellationToken);
        }

        async ValueTask WaitReadyForAuthenticationAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                var message = await Reader.ReadMessageAsync(cancellationToken);

                if (message.Type == BackendMessageType.ReadyForQuery)
                {
                    SetReady();
                    return;
                }

                if (message is ErrorResponse error)
                {
                    // The connection attempt has been rejected.
                    // The server then immediately closes the connection.
                    throw new ServerException(error);
                }

                if (message is NoticeResponse notice)
                {
                    notice.Log();
                }

                // DataRow should not be received during authentication cycle, just in case.
                if (message is DataRow row)
                {
                    await row.DiscardAsync(cancellationToken);
                }
            }
        }
    }
}
