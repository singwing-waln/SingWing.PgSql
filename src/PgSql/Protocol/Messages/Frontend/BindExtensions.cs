using SingWing.PgSql.Protocol.Connections;
using System.Buffers;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Sends Bind messages to database servers.
/// </summary>
internal static class BindExtensions
{
    /// <summary>
    /// Writes a Bind message to the output stream.
    /// </summary>
    /// <param name="writer">The output stream to the database service.</param>
    /// <param name="parameters">A list of parameters to bind to the current command.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static async ValueTask WriteBindMessageAsync(
        this Writer writer,
        ReadOnlyMemory<Parameter> parameters,
        CancellationToken cancellationToken)
    {
        const short FormatCodeToAll = 1;
        // The format code of parameters, always use binary format.
        const short BinaryFormatCode = 1;
        // https://www.postgresql.org/docs/current/protocol-message-formats.html
        const char MessageType = 'B';

        var length =
            // Length of message contents in bytes, including self.
            sizeof(int) +
            // The name of the destination portal
            // (an empty string selects the unnamed portal).
            sizeof(byte) +
            // The name of the source prepared statement
            // (an empty string selects the unnamed prepared statement).
            sizeof(byte) +
            // The number of parameter format codes...or one,
            // in which case the binary format code is applied to all parameters.
            sizeof(short) +
            sizeof(short) +
            // The number of parameter values...
            sizeof(short) +
            // The number of result-column format codes that follow...or one,
            // in which case the specified format code is applied to all result columns.
            sizeof(short) +
            sizeof(short);

        if (parameters.Length == 0)
        {
            await WriteHeaderAsync();
        }
        else if (parameters.Length == 1)
        {
            var parameter = parameters.Span[0];
            // The length of the parameter value, in bytes (this count does not include itself).
            // Can be zero. As a special case, -1 indicates a NULL parameter value.
            // No value bytes follow in the NULL case.
            length += sizeof(int);
            // The value length of the parameter, in binary format.
            var binaryLength = parameter.BinaryLength;
            length += binaryLength == -1 ? 0 : binaryLength;

            await WriteHeaderAsync();

            await writer.WriteInt32Async(binaryLength, cancellationToken);
            await parameter.WriteAsync(writer, cancellationToken);
        }
        else
        {
            var binaryLengths = ArrayPool<int>.Shared.Rent(parameters.Length);

            try
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    // The length of the parameter value, in bytes (this count does not include itself).
                    // Can be zero. As a special case, -1 indicates a NULL parameter value.
                    // No value bytes follow in the NULL case.
                    length += sizeof(int);
                    // The value length of the parameter, in binary format.
                    var binaryLength = parameters.Span[i].BinaryLength;
                    length += binaryLength == -1 ? 0 : binaryLength;
                    binaryLengths[i] = binaryLength;
                }

                await WriteHeaderAsync();

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters.Span[i];
                    await writer.WriteInt32Async(binaryLengths[i], cancellationToken);
                    await parameter.WriteAsync(writer, cancellationToken);
                }
            }
            finally
            {
                ArrayPool<int>.Shared.Return(binaryLengths);
            }
        }

        // 1 denotes the specified format code is applied to all result columns.
        await writer.WriteInt16Async(FormatCodeToAll, cancellationToken);
        await writer.WriteInt16Async(BinaryFormatCode, cancellationToken);

        async ValueTask WriteHeaderAsync()
        {
            await writer.WriteByteAsync(MessageType, cancellationToken);
            await writer.WriteInt32Async(length, cancellationToken);
            // unnamed portal
            await writer.WriteStringTerminatorAsync(cancellationToken);
            // unnamed prepared statement
            await writer.WriteStringTerminatorAsync(cancellationToken);
            // 1 denotes the binary format code is applied to all parameters.
            await writer.WriteInt16Async(FormatCodeToAll, cancellationToken);
            await writer.WriteInt16Async(BinaryFormatCode, cancellationToken);
            await writer.WriteInt16Async((short)parameters.Length, cancellationToken);
        }
    }
}
