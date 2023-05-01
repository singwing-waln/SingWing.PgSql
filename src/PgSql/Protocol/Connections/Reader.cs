using SingWing.PgSql.Protocol.Messages;
using SingWing.PgSql.Protocol.Messages.Backend;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Buffers;

namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Represents a reader that can receive message data from a backend.
/// </summary>
internal sealed class Reader : IDisposable
{
    #region Fields

    /// <summary>
    /// The size of the receive buffer (8 KiB).
    /// </summary>
    internal const int BufferSize = 8 * 1024;

    /// <summary>
    /// There will only be one <see cref="CommandComplete"/> on the same connection at a time, 
    /// so use a predefined message instead of creating a new object each time.
    /// </summary>
    private readonly CommandComplete PredefinedCommandComplete;

    /// <summary>
    /// There will only be one <see cref="ErrorResponse"/> on the same connection at a time, 
    /// so use a predefined message instead of creating a new object each time.
    /// </summary>
    private readonly ErrorResponse PredefinedErrorResponse;

    /// <summary>
    /// There will only be one <see cref="NoticeResponse"/> on the same connection at a time, 
    /// so use a predefined message instead of creating a new object each time.
    /// </summary>
    private readonly NoticeResponse PredefinedNoticeResponse;

    /// <summary>
    /// There will only be one <see cref="RowDescription"/> on the same connection at a time, 
    /// so use a predefined message instead of creating a new object each time.
    /// </summary>
    private readonly RowDescription PredefinedRowDescription;

    /// <summary>
    /// The underlying network connection.
    /// </summary>
    private readonly Connection _connection;

    /// <summary>
    /// The buffer used to receive data from database server.
    /// </summary>
    private readonly byte[] _buffer;

    /// <summary>
    /// The position where the caller reads data from the buffer.
    /// </summary>
    private int _offset;

    /// <summary>
    /// The length of data read from the network and written to the buffer.
    /// This length is calculated from the start of the buffer, 
    /// and the available data starts at _offset, so _offset may be less than this value.
    /// </summary>
    private int _length;

    /// <summary>
    /// The description of the most recently read row.
    /// </summary>
    private RowDescription _rowDescription;

    /// <summary>
    /// The text decoder responsible for reading characters from the buffer.
    /// </summary>
    private readonly Decoder _decoder;

    /// <summary>
    /// Signals to the CancellationToken for a data receiving operation should be time out.
    /// </summary>
    private readonly TimeoutTimer _timer;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Reader"/> class
    /// with the specified underlying network connection.
    /// </summary>
    /// <param name="connection">The underlying network connection.</param>
    internal Reader(Connection connection)
    {
        PredefinedCommandComplete = new(rowCount: 0L);
        PredefinedErrorResponse = new();
        PredefinedNoticeResponse = new();
        PredefinedRowDescription = new();

        _connection = connection;
        _buffer = new byte[BufferSize];
        _offset = 0;
        _length = 0;
        _rowDescription = PredefinedRowDescription;
        _decoder = Encoding.UTF8.GetDecoder();
        _timer = new(connection.Node, TimeoutTimer.Operation.Receiving);
    }

    #endregion

    #region IDisposable

    /// <inheritdoc/>
    public void Dispose() => _timer.Dispose();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the size of the remaining space that has not been filled with data.
    /// </summary>
    private int EmptySpace => _buffer.Length - _length;

    /// <summary>
    /// Gets the length of data available in the buffer.
    /// </summary>
    private int DataLength => _length - _offset;

    #endregion

    #region ReadMessage

    /// <summary>
    /// Receives a backend message from the server. 
    /// When this method returns, there may still be some message data that has not been read.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The received <see cref="IBackendMessage"/>.</returns>
    internal async ValueTask<IBackendMessage> ReadMessageAsync(CancellationToken cancellationToken)
    {
        // At least one byte for the message type and 4 bytes for the message length.
        await EnsureAsync(sizeof(byte) + sizeof(int), cancellationToken);

        var type = ReadByte();
        // The length contains 4 bytes of the length itself, but does not contain the 1-byte message type.
        var length = ReadInt32() - sizeof(int);

        return (BackendMessageType)type switch
        {
            BackendMessageType.AuthenticationRequest =>
                await AuthenticationRequest.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.BackendKeyData =>
                await BackendKeyData.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.BindComplete => BindComplete.Shared,
            BackendMessageType.CloseComplete => CloseComplete.Shared,
            BackendMessageType.CommandComplete =>
                await CommandComplete.ReadAsync(
                    this, length, PredefinedCommandComplete, cancellationToken),
            BackendMessageType.CopyData =>
                await CopyData.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.CopyDone => CopyDone.Shared,
            BackendMessageType.CopyInResponse =>
                await CopyInResponse.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.CopyOutResponse =>
                await CopyOutResponse.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.CopyBothResponse =>
                await CopyBothResponse.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.DataRow => await DataRow.ReadAsync(
                this, _rowDescription, length, _connection.Rows.Row, cancellationToken),
            BackendMessageType.EmptyQueryResponse => EmptyQueryResponse.Shared,
            BackendMessageType.ErrorResponse =>
                await ProblemResponse.ReadAsync(
                    this, length, PredefinedErrorResponse, cancellationToken),
            BackendMessageType.FunctionCallResponse =>
                await FunctionCallResponse.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.NegotiateProtocolVersion =>
                await NegotiateProtocolVersion.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.NoData => NoData.Shared,
            BackendMessageType.NoticeResponse =>
                await ProblemResponse.ReadAsync(
                    this, length, PredefinedNoticeResponse, cancellationToken),
            BackendMessageType.NotificationResponse =>
                await NotificationResponse.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.ParameterDescription =>
                await ParameterDescription.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.ParameterStatus =>
                await ParameterStatus.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.ParseComplete => ParseComplete.Shared,
            BackendMessageType.PortalSuspended => PortalSuspended.Shared,
            BackendMessageType.ReadyForQuery =>
                await ReadyForQuery.ReadAsync(
                    this, length, cancellationToken),
            BackendMessageType.RowDescription => _rowDescription =
                await RowDescription.ReadAsync(
                    this, length, PredefinedRowDescription, cancellationToken),
            _ => await Unknown.ReadAsync(this, type, length, cancellationToken),
        };
    }

    #endregion

    #region ReadByte

    /// <summary>
    /// Reads the first <see langword="byte"/> value.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see langword="byte"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask<byte> ReadByteAsync(
        CancellationToken cancellationToken)
    {
        await EnsureAsync(sizeof(byte), cancellationToken);
        return _buffer[_offset++];
    }

    /// <summary>
    /// Gets the first <see langword="byte"/> value, and advances the read position.
    /// The caller should ensure that the buffer contains at least 1 byte.
    /// </summary>
    /// <returns>The <see langword="byte"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private byte ReadByte()
    {
        DebugAssertEnoughData(sizeof(byte));
        return _buffer[_offset++];
    }

    #endregion

    #region ReadInt

    /// <summary>
    /// Reads the first <see langword="short"/> value, and converts it to host byte order.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see langword="short"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask<short> ReadInt16Async(CancellationToken cancellationToken) =>
        (await ReadIntAsync<short>(sizeof(short), cancellationToken)).ToHostOrder();

    /// <summary>
    /// Gets the first <see langword="short"/> value, converts it to host byte order, and advances the read position.
    /// The caller should ensure that the buffer contains at least 2 bytes.
    /// </summary>
    /// <returns>The <see langword="short"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal short ReadInt16() => ReadInt<short>(sizeof(short)).ToHostOrder();

    /// <summary>
    /// Gets the first <see langword="ushort"/> value, converts it to host byte order, and advances the read position.
    /// The caller should ensure that the buffer contains at least 2 bytes.
    /// </summary>
    /// <returns>The <see langword="ushort"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ushort ReadUInt16() => ReadInt<ushort>(sizeof(ushort)).ToHostOrder();

    /// <summary>
    /// Reads the first <see langword="int"/> value, and converts it to host byte order.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see langword="int"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask<int> ReadInt32Async(CancellationToken cancellationToken) =>
        (await ReadIntAsync<int>(sizeof(int), cancellationToken)).ToHostOrder();

    /// <summary>
    /// Gets the first <see langword="int"/> value, converts it to host byte order, and advances the read position.
    /// The caller should ensure that the buffer contains at least 4 bytes.
    /// </summary>
    /// <returns>The <see langword="int"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal int ReadInt32() => ReadInt<int>(sizeof(int)).ToHostOrder();

    /// <summary>
    /// Reads the first <see langword="uint"/> value, and converts it to host byte order.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see langword="uint"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask<uint> ReadUInt32Async(CancellationToken cancellationToken) =>
        (await ReadIntAsync<uint>(sizeof(uint), cancellationToken)).ToHostOrder();

    /// <summary>
    /// Gets the first <see langword="uint"/> value, converts it to host byte order, and advances the read position.
    /// The caller should ensure that the buffer contains at least 4 bytes.
    /// </summary>
    /// <returns>The <see langword="uint"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private uint ReadUInt32() => ReadInt<uint>(sizeof(uint)).ToHostOrder();

    /// <summary>
    /// Reads the first <see langword="long"/> value, and converts it to host byte order.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The <see langword="long"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask<long> ReadInt64Async(CancellationToken cancellationToken) =>
        (await ReadIntAsync<long>(sizeof(long), cancellationToken)).ToHostOrder();

    /// <summary>
    /// Gets the first <see langword="long"/> value, and advances the read position.
    /// The caller should ensure that the buffer contains at least 8 bytes.
    /// </summary>
    /// <returns>The <see langword="long"/> value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal long ReadRawInt64() => ReadInt<long>(sizeof(long));

    /// <summary>
    /// Reads the first integer value of the specified type in the specified number of bytes from the buffer.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <typeparam name="T">The type of the return value.</typeparam>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The integer value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async ValueTask<T> ReadIntAsync<T>(
        int binaryLength,
        CancellationToken cancellationToken) where T : struct
    {
        await EnsureAsync(binaryLength, cancellationToken);

        var value = Unsafe.ReadUnaligned<T>(ref _buffer[_offset]);
        _offset += binaryLength;
        return value;
    }

    /// <summary>
    /// Reads the first integer value of the specified type in the specified number of bytes from the buffer.
    /// The caller should ensure that the buffer contains enough bytes.
    /// </summary>
    /// <typeparam name="T">The type of the return value.</typeparam>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <returns>The integer value read.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T ReadInt<T>(int binaryLength) where T : struct
    {
        DebugAssertEnoughData(binaryLength);
        var value = Unsafe.ReadUnaligned<T>(ref _buffer[_offset]);
        _offset += binaryLength;
        return value;
    }

    #endregion

    #region ReadString

    /// <summary>
    /// Reads the name of the column in the current <see cref="RowDescription"/> in C-style string.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="chars">The buffer into which characters are to be written.</param>
    /// <param name="maxBytes">The maximum number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The numbers of characters and bytes read (including '\0').</returns>
    /// <exception cref="InvalidDataException">The server did not provide a valid column name.</exception>
    /// <remarks>
    /// If the buffer is not large enough to hold all characters, the string is truncated, 
    /// but all bytes up to '\0' are processed, as well as '\0' itself.
    /// </remarks>
    internal async ValueTask<(int, int)> ReadColumnNameAsync(
        Memory<char> chars,
        int maxBytes,
        CancellationToken cancellationToken)
    {
        // maxBytes represents the length of the remaining data in the current RowDescription message that has not yet been processed.
        // It is always assumed that the column name length will not exceed ColumnDescription.MaxNameLength,
        // so the column name length will not exceed the size of the current buffer (16KiB).
        await EnsureAsync(
            maxBytes > _buffer.Length ? _buffer.Length : maxBytes,
            cancellationToken);

        var index = IndexOfStringTerminator(maxBytes);
        if (index == -1)
        {
            // It means that the server sent an invalid RowDescription message,
            // or the length of the column name is too long for the buffer size.
            throw new InvalidDataException(Strings.StringTerminatorNotFound);
        }

        // Although we define that the column name length should not exceed ColumnDescription.MaxNameLength,
        // it does not mean that the server will not send longer column names,
        // when this happens, column names will be truncated.
        // So uses Convert instead of Encoding.GetChars to avoid exceptions when chars run out of space.
        // 
        // If bytesUsed is not the same as index - _offset at the end of the conversion,
        // the server-supplied column name is longer than the size of chars, and the name is truncated.
        _decoder.Convert(
            _buffer.AsSpan(_offset, index - _offset),
            chars.Span,
            flush: true,
            out var bytesUsed,
            out var charsUsed,
            out var _);
        _decoder.Reset();

        if (index + 1 >= _length)
        {
            _offset = 0;
            _length = 0;
        }
        else
        {
            _offset = index + 1;
        }

        return (charsUsed, bytesUsed + 1);
    }

    /// <summary>
    /// Reads the first C-style string.
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="maxBytes">The maximum number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The string and the numbers of bytes read (including '\0').</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask<(string, int)> ReadNullTerminatedStringAsync(
        int maxBytes,
        CancellationToken cancellationToken)
    {
        return maxBytes <= _buffer.Length
            ? ReadShortNullTerminatedStringAsync(maxBytes, cancellationToken)
            : ReadLongNullTerminatedStringAsync(maxBytes, cancellationToken);

        async ValueTask<(string, int)> ReadShortNullTerminatedStringAsync(
            int maxBytes,
            CancellationToken cancellationToken)
        {
            await EnsureAsync(maxBytes, cancellationToken);

            var index = IndexOfStringTerminator(maxBytes);
            if (index == -1)
            {
                // The server sent an invalid message.
                throw new InvalidDataException(Strings.StringTerminatorNotFound);
            }

            return GetNullTerminatedString(index);
        }

        async ValueTask<(string, int)> ReadLongNullTerminatedStringAsync(
            int maxBytes,
            CancellationToken cancellationToken)
        {
            await EnsureAsync(_buffer.Length, cancellationToken);
            var index = IndexOfStringTerminator(maxBytes);

            if (index == 0)
            {
                return (string.Empty, 1);
            }

            if (index > 0)
            {
                // The string is already completely in the buffer.
                return GetNullTerminatedString(index);
            }

            // More data needs to be read, and the MessageReader's buffer is no longer used.
            var dataLength = DataLength;
            // First tries a temporary buffer twice the size of the current buffer.
            var buffer = TempByteBuffer.Acquire(_buffer.Length + BufferSize);

            try
            {
                var data = buffer.Data;
                // Copies existing data into a temporary buffer.
                Array.Copy(_buffer, _offset, data, 0, dataLength);
                _offset = 0;
                _length = 0;
                // The starting position for finding the string terminator('\0').
                var start = dataLength + 1;

                while (true)
                {
                    dataLength += await ReceiveAsync(
                        data.AsMemory(dataLength),
                        cancellationToken);
                    index = IndexOfStringTerminatorIn(data, start, dataLength);

                    if (index > 0)
                    {
                        // Copies the remaining data in the temporary buffer into the current buffer.
                        if (index + 1 < dataLength)
                        {
                            _length = dataLength - index - 1;
                            Array.Copy(data, index + 1, _buffer, 0, _length);
                        }

                        return GetNullTerminatedString(index);
                    }

                    start = dataLength + 1;
                    if (start >= maxBytes)
                    {
                        // The server sent an invalid message.
                        throw new InvalidDataException(Strings.StringTerminatorNotFound);
                    }

                    // If the temporary buffer space is full, enlarges the capacity of the buffer.
                    if (dataLength == data.Length)
                    {
                        buffer = buffer.Enlarge(BufferSize, dataLength);
                        data = buffer.Data;
                    }
                }
            }
            finally
            {
                TempByteBuffer.Release(buffer);
            }

            static unsafe int IndexOfStringTerminatorIn(byte[] buffer, int start, int length)
            {
                fixed (byte* bytes = buffer)
                {
                    for (var i = start; i < length; i++)
                    {
                        if (*(bytes + i) == '\0')
                        {
                            return i;
                        }
                    }
                }

                return -1;
            }
        }
    }

    /// <summary>
    /// Gets a '\0' terminated string from the current buffer.
    /// </summary>
    /// <param name="index">The position of '\0' in the buffer.</param>
    /// <returns>The string and the numbers of bytes read (including '\0').</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private (string, int) GetNullTerminatedString(int index)
    {
        var binaryLength = index - _offset;
        var s = Encoding.UTF8.GetString(_buffer.AsSpan(_offset, binaryLength));
        Advance(binaryLength + 1);
        return (s, binaryLength + 1);
    }

    /// <summary>
    /// Reads the specified number of bytes and converts it to a string.
    /// </summary>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The string read.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The number of bytes to read exceeds the maximum limit.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask<ReadOnlyMemory<char>?> ReadStringAsync(
        int binaryLength,
        CancellationToken cancellationToken) =>
        ReadCharMemoryAsync(Memory<char>.Empty, binaryLength, cancellationToken);

    /// <summary>
    /// Reads the specified number of bytes and converts it to a string.
    /// </summary>
    /// <param name="chars">
    /// The buffer into which characters are to be written.
    /// If the buffer size is not large enough, temporary memory is allocated internally.
    /// </param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The string read.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The number of bytes to read exceeds the maximum limit.</exception>
    internal ValueTask<ReadOnlyMemory<char>?> ReadCharMemoryAsync(
        Memory<char>? chars,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return new ValueTask<ReadOnlyMemory<char>?>((ReadOnlyMemory<char>?)null);
        }

        if (binaryLength == 0)
        {
            return new ValueTask<ReadOnlyMemory<char>?>(ReadOnlyMemory<char>.Empty);
        }

        if (binaryLength <= _buffer.Length)
        {
            return ReadCharMemoryInPlaceAsync(chars, binaryLength, cancellationToken);
        }

        if (binaryLength <= TempByteBuffer.PoolThreshold)
        {
            return ReadCharMemoryUsePooledBufferAsync(chars, binaryLength, cancellationToken);
        }

        return ReadCharMemoryUseTempBufferAsync(chars, binaryLength, cancellationToken);

        async ValueTask<ReadOnlyMemory<char>?> ReadCharMemoryInPlaceAsync(
            Memory<char>? chars,
            int binaryLength,
            CancellationToken cancellationToken)
        {
            await EnsureAsync(binaryLength, cancellationToken);
            var charLength = Encoding.UTF8.GetCharCount(_buffer.AsSpan(_offset, binaryLength));
            if (chars is not null && charLength <= chars.Value.Length)
            {
                _ = Encoding.UTF8.GetChars(_buffer.AsSpan(_offset, binaryLength), chars.Value.Span);
                Advance(binaryLength);
                return chars.Value[..charLength];
            }
            else
            {
                var s = Encoding.UTF8.GetString(_buffer.AsSpan(_offset, binaryLength));
                Advance(binaryLength);
                return s.AsMemory();
            }
        }

        async ValueTask<ReadOnlyMemory<char>?> ReadCharMemoryUsePooledBufferAsync(
            Memory<char>? chars,
            int binaryLength,
            CancellationToken cancellationToken)
        {
            var bytes = ArrayPool<byte>.Shared.Rent(binaryLength);

            try
            {
                return await ReadCharMemoryUseBufferAsync(chars, bytes, cancellationToken);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(bytes);
            }
        }

        ValueTask<ReadOnlyMemory<char>?> ReadCharMemoryUseTempBufferAsync(
            Memory<char>? chars,
            int binaryLength,
            CancellationToken cancellationToken)
        {
            var bytes = GC.AllocateUninitializedArray<byte>(binaryLength);
            return ReadCharMemoryUseBufferAsync(chars, bytes, cancellationToken);
        }

        async ValueTask<ReadOnlyMemory<char>?> ReadCharMemoryUseBufferAsync(
            Memory<char>? chars,
            byte[] buffer,
            CancellationToken cancellationToken)
        {
            await ReadAsync(buffer, buffer.Length, cancellationToken);
            var charLength = Encoding.UTF8.GetCharCount(buffer);
            if (chars is not null && charLength <= chars.Value.Length)
            {
                _ = Encoding.UTF8.GetChars(buffer, chars.Value.Span);
                return chars.Value[..charLength];
            }
            else
            {
                return Encoding.UTF8.GetString(buffer).AsMemory();
            }
        }
    }

    /// <summary>
    /// Reads the specified number of bytes and writes it to the specified writer as a string.
    /// </summary>
    /// <param name="writer">The writer to which to write data.</param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The number of characters actually written to the writer.</returns>
    internal async ValueTask<int> ReadStringAsync(
        IBufferWriter<char> writer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength <= 0)
        {
            return 0;
        }

        var buffer = TempCharBuffer.Acquire(binaryLength);

        try
        {
            var count = await ReadStringAsync(
                buffer.Data, binaryLength, cancellationToken);

            writer.Write(buffer.Data.AsSpan(0, count));

            return count;
        }
        finally
        {
            TempCharBuffer.Release(buffer);
        }
    }

    /// <summary>
    /// Reads the specified number of bytes and writes it to the specified writer as a string.
    /// </summary>
    /// <param name="writer">The writer to which to write data.</param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The number of characters actually written to the writer.</returns>
    internal async ValueTask<int> ReadStringAsync(
        TextWriter writer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength <= 0)
        {
            return 0;
        }

        var buffer = TempCharBuffer.Acquire(binaryLength);

        try
        {
            var count = await ReadStringAsync(
                buffer.Data, binaryLength, cancellationToken);

            await writer.WriteAsync(
                buffer.Data.AsMemory(0, count),
                cancellationToken);

            return count;
        }
        finally
        {
            TempCharBuffer.Release(buffer);
        }
    }

    /// <summary>
    /// Reads the specified number of bytes and writes it to the specified buffer as a string.
    /// </summary>
    /// <param name="buffer">The buffer to which to write data.</param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The number of characters actually written to the writer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">There is not enough space in the character buffer.</exception>
    /// <exception cref="EndOfStreamException">Reading is attempted past the end of stream.</exception>
    internal async ValueTask<int> ReadStringAsync(
        Memory<char> buffer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength <= 0)
        {
            return 0;
        }

        // The number of characters that have been written to the buffer.
        var charCount = 0;
        // The number of bytes that have been processed.
        var byteCount = 0;

        using var charsHandle = buffer.Pin();
        using var bytesHandle = _buffer.AsMemory().Pin();

        while (true)
        {
            if (buffer.Length - charCount == 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(buffer),
                    Strings.NotEnoughBufferSpace);
            }

            var dataLength = DataLength;

            // Moves existing data to the beginning of the buffer.
            if (_offset > 0)
            {
                if (dataLength > 0)
                {
                    Array.Copy(_buffer, _offset, _buffer, 0, dataLength);
                }

                _offset = 0;
                _length = dataLength;
            }

            // If the buffer still has space to receive data,
            // and the sum of the number of bytes already processed and the length of the buffer data is less than the number of bytes to be processed,
            // we need to receive more data first.
            if (EmptySpace > 0 && byteCount + dataLength < binaryLength)
            {
                _length += await ReceiveAsync(
                    _buffer.AsMemory(_offset, EmptySpace),
                    cancellationToken);
            }

            UnsafeRead(
                charsHandle,
                bytesHandle,
                buffer.Length - charCount,
                binaryLength - byteCount,
                out var charsUsed,
                out var bytesUsed,
                out var completed);

            charCount += charsUsed;
            byteCount += bytesUsed;

            if (completed)
            {
                break;
            }
        }

        _decoder.Reset();
        return charCount;

        unsafe void UnsafeRead(
            MemoryHandle charsHandle,
            MemoryHandle bytesHandle,
            int charsRemain,
            int bytesRemain,
            out int charsUsed,
            out int bytesUsed,
            out bool completed)
        {
            var dataLength = DataLength;
            var chars = (char*)charsHandle.Pointer;
            var bytes = (byte*)bytesHandle.Pointer;

            _decoder.Convert(
                bytes + _offset,
                bytesRemain < dataLength ? bytesRemain : dataLength,
                chars,
                charsRemain,
                flush: bytesRemain <= dataLength,
                out bytesUsed,
                out charsUsed,
                out completed);

            Advance(bytesUsed);
        }
    }

    /// <summary>
    /// Finds the position of the string terminator('\0') in the buffer.
    /// </summary>
    /// <param name="maxBytes">The maximum number of bytes to read。</param>
    /// <returns>The position of '\0' in the buffer, or -1 if not found.</returns>
    private unsafe int IndexOfStringTerminator(int maxBytes)
    {
        fixed (byte* bytes = _buffer)
        {
            var end = _offset + maxBytes;
            if (end > _length)
            {
                end = _length;
            }

            for (var i = _offset; i < end; i++)
            {
                if (*(bytes + i) == '\0')
                {
                    return i;
                }
            }
        }

        return -1;
    }

    #endregion

    #region ReadBinary

    /// <summary>
    /// Reads bytes of the specified length.
    /// </summary>
    /// <param name="buffer">
    /// The buffer into which bytes are to be written.
    /// If the buffer size is not large enough, temporary memory is allocated internally.
    /// </param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The bytes read. Returns <see langword="null"/> if binaryLength is less than 0.</returns>
    internal async ValueTask<ReadOnlyMemory<byte>?> ReadBinaryAsync(
        Memory<byte>? buffer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 0)
        {
            return null;
        }

        if (binaryLength == 0)
        {
            return ReadOnlyMemory<byte>.Empty;
        }

        if (binaryLength <= _buffer.Length)
        {
            await EnsureAsync(binaryLength, cancellationToken);
            var bytes = _buffer.AsMemory(_offset, binaryLength);
            Advance(binaryLength);
            return bytes;
        }

        if (buffer is not null && buffer.Value.Length >= binaryLength)
        {
            await ReadAsync(buffer.Value, binaryLength, cancellationToken);
            return buffer.Value[..binaryLength];
        }

        var binary = GC.AllocateUninitializedArray<byte>(binaryLength);
        await ReadAsync(binary, binaryLength, cancellationToken);
        return binary;
    }

    /// <summary>
    /// Reads byte data of the specified length and writes to the specified stream.
    /// </summary>
    /// <param name="stream">The output stream to write data to.</param>
    /// <param name="binaryLength">The number of bytes to read and write to the output stream.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask ReadBinaryAsync(
        Stream stream,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 1)
        {
            return;
        }

        if (binaryLength <= _buffer.Length)
        {
            await EnsureAsync(binaryLength, cancellationToken);
            await stream.WriteAsync(_buffer.AsMemory(_offset, binaryLength), cancellationToken);
            Advance(binaryLength);
            return;
        }

        // The number of bytes that have been written to the output stream.
        var count = 0;
        while (count < binaryLength)
        {
            var length = binaryLength - count;
            await EnsureAsync(
                Min(length, _buffer.Length),
                cancellationToken);

            length = Min(length, DataLength);
            await stream.WriteAsync(
                _buffer.AsMemory(_offset, length),
                cancellationToken);
            Advance(length);
            count += length;
        }
    }

    /// <summary>
    /// Reads byte data of the specified length and writes to the specified writer.
    /// </summary>
    /// <param name="writer">The output writer to write data to.</param>
    /// <param name="binaryLength">The number of bytes to read and write to the output writer.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask ReadBinaryAsync(
        IBufferWriter<byte> writer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 1)
        {
            return;
        }

        if (binaryLength <= _buffer.Length)
        {
            await EnsureAsync(binaryLength, cancellationToken);
            writer.Write(_buffer.AsSpan(_offset, binaryLength));
            Advance(binaryLength);
            return;
        }

        // The number of bytes that have been written to the output stream.
        var count = 0;
        while (count < binaryLength)
        {
            var length = binaryLength - count;
            await EnsureAsync(
                Min(length, _buffer.Length),
                cancellationToken);

            length = Min(length, DataLength);
            writer.Write(_buffer.AsSpan(_offset, length));
            Advance(length);
            count += length;
        }
    }

    /// <summary>
    /// Reads byte data of the specified length and writes to the specified buffer.
    /// </summary>
    /// <param name="buffer">The output buffer to write data to.</param>
    /// <param name="binaryLength">The number of bytes to read and write to the output buffer.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="ArgumentOutOfRangeException">There is not enough buffer space to hold all the bytes.</exception>
    internal ValueTask ReadBinaryAsync(
        Memory<byte> buffer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 1)
        {
            return ValueTask.CompletedTask;
        }

        return buffer.Length < binaryLength
            ? throw new ArgumentOutOfRangeException(
                nameof(buffer),
                Strings.NotEnoughBufferSpace)
            : ReadAsync(buffer, binaryLength, cancellationToken);
    }

    #endregion

    #region ReadJson

    /// <summary>
    /// Reads bytes of the specified length and writes it to the specified writer as JSON data.
    /// </summary>
    /// <param name="writer">The writer to write data to.</param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask ReadJsonAsync(
        Utf8JsonWriter writer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 1)
        {
            return;
        }

        if (binaryLength <= _buffer.Length)
        {
            await EnsureAsync(binaryLength, cancellationToken);
            // Skip json version
            writer.WriteRawValue(_buffer.AsSpan(_offset + 1, binaryLength - 1), skipInputValidation: true);
            Advance(binaryLength);
        }
        else
        {
            // Skip json version
            _ = await ReadByteAsync(cancellationToken);
            binaryLength--;

            // The number of bytes that have been written to the output stream.
            var count = 0;
            while (count < binaryLength)
            {
                var length = binaryLength - count;
                await EnsureAsync(
                    Min(length, _buffer.Length),
                    cancellationToken);

                length = Min(length, DataLength);
                writer.WriteRawValue(_buffer.AsSpan(_offset, length), skipInputValidation: true);
                Advance(length);
                count += length;
            }
        }
    }

    /// <summary>
    /// Reads bytes of the specified length and writes it as JSON text to the specified writer as JSON data.
    /// </summary>
    /// <param name="writer">The writer to write data to.</param>
    /// <param name="binaryLength">The number of bytes to read.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>
    /// <see href="https://doxygen.postgresql.org/json_8c.html#a683cb3dbd31b72783dabaf10576c5e74"/>.
    /// </remarks>
    internal async ValueTask ReadJsonTextAsync(
        Utf8JsonWriter writer,
        int binaryLength,
        CancellationToken cancellationToken)
    {
        if (binaryLength < 1)
        {
            return;
        }

        var buffer = TempCharBuffer.Acquire(binaryLength);

        try
        {
            var charCount = await ReadStringAsync(buffer.Data.AsMemory(), binaryLength, cancellationToken);
            writer.WriteRawValue(buffer.Data.AsSpan(0, charCount));
        }
        finally
        {
            TempCharBuffer.Release(buffer);
        }
    }

    #endregion

    #region ReadArray

    /// <summary>
    /// Reads the header information of an array type value.
    /// </summary>
    /// <param name="maxBytes">The total binary length of the array value.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>
    /// The length of the array and the number of consumed bytes. 
    /// If the value is not an array, the array length returns -1.
    /// </returns>
    internal async ValueTask<(int, int)> ReadArrayHeaderAsync(
        int maxBytes,
        CancellationToken cancellationToken)
    {
        if (maxBytes < ArrayHelper.HeaderBinaryLengthForReceiving)
        {
            // Invalid array data.
            await DiscardAsync(maxBytes, cancellationToken);
            return (-1, maxBytes);
        }

        await EnsureAsync(ArrayHelper.HeaderBinaryLengthForReceiving, cancellationToken);

        // dimensions (ndim = 1)
        var dimensions = ReadInt32();
        // has nulls
        _ = ReadInt32();
        // type OID
        _ = ReadUInt32();

        // The number of bytes read.
        var count = ArrayHelper.HeaderBinaryLengthForReceiving;

        if (dimensions != 1)
        {
            await DiscardAsync(maxBytes - count, cancellationToken);
            // Converts to null if more than one dimension.
            // If it is less than one dimension, it is regarded as an empty array.
            return (dimensions < 1 ? 0 : -1, maxBytes);
        }

        // array length + lower bound
        await EnsureAsync(sizeof(int) + sizeof(int), cancellationToken);

        var arrayLength = ReadInt32();
        // ignore lower bound
        _ = ReadInt32();
        count += sizeof(int) + sizeof(int);

        if (arrayLength < 1)
        {
            // Empty array.
            await DiscardAsync(maxBytes - count, cancellationToken);
            return (0, maxBytes);
        }

        return (arrayLength, count);
    }

    #endregion

    #region Discard

    /// <summary>
    /// Reads and discards the specified number of bytes of data.
    /// </summary>
    /// <param name="byteCount">The number of bytes to discard.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask DiscardAsync(int byteCount, CancellationToken cancellationToken)
    {
        if (byteCount < 1)
        {
            return;
        }

        if (DataLength >= byteCount)
        {
            // There is more data in the buffer than we want to discard.
            Advance(byteCount);
            return;
        }

        // Discard all data in the buffer.
        byteCount -= DataLength;
        _offset = 0;
        _length = 0;

        while (byteCount > 0)
        {
            byteCount -= await ReceiveAsync(
                _buffer.AsMemory(0, Min(byteCount, _buffer.Length)),
                cancellationToken);
        }
    }

    #endregion

    #region Receive Data

    /// <summary>
    /// Ensures that the buffer contains the specified number of bytes (less than or equal to the buffer size).
    /// Receives data from the underlying network if necessary.
    /// </summary>
    /// <param name="count">The number of bytes, this value should not exceed the size of the buffer.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="EndOfStreamException">
    /// Reading is attempted past the end of the network stream.
    /// </exception>
    /// <remarks>The actual number of bytes read may exceed <paramref name="count"/>.</remarks>
    internal async ValueTask EnsureAsync(
        int count,
        CancellationToken cancellationToken)
    {
        Debug.Assert(count <= _buffer.Length);

        while (DataLength < count)
        {
            if (_offset > 0 && EmptySpace < count - DataLength)
            {
                // Not enough space left.
                // If the current data does not start at the beginning of the buffer,
                // move the existing data to the beginning of the buffer.
                Array.Copy(_buffer, _offset, _buffer, 0, DataLength);
                _length -= _offset;
                _offset = 0;
            }

            _length += await ReceiveAsync(
                _buffer.AsMemory(_length, EmptySpace),
                cancellationToken);
        }
    }

    /// <summary>
    /// Reads and fills the specified number of bytes of data into the specified buffer.
    /// </summary>
    /// <param name="buffer">The destination buffer.</param>
    /// <param name="count">The number of bytes, this value should not exceed the size of the buffer.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <remarks>The actual number of bytes read is exactly the same as <paramref name="count"/>.</remarks>
    private async ValueTask ReadAsync(
        Memory<byte> buffer,
        int count,
        CancellationToken cancellationToken)
    {
        Debug.Assert(buffer.Length >= count);

        var total = DataLength;
        // Writes data that has been read but not yet processed into the buffer.
        if (total > 0)
        {
            if (total > count)
            {
                total = count;
            }

            _buffer.AsMemory(_offset, total).CopyTo(buffer);
            Advance(total);
        }

        while (total < count)
        {
            total += await ReceiveAsync(buffer[total..count], cancellationToken);
        }
    }

    /// <summary>
    /// Receives data from the underlying connection.
    /// </summary>
    /// <param name="buffer">The buffer for the received data.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="EndOfStreamException">
    /// Reading is attempted past the end of the network stream.
    /// </exception>
    /// <returns>The number of bytes received.</returns>
    private async ValueTask<int> ReceiveAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken)
    {
        var token = _timer.Start(cancellationToken);

        try
        {
            var count = await _connection.Socket.ReceiveAsync(
                buffer,
                SocketFlags.None,
                token);

            // https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.socket.receiveasync?view=net-7.0
            // For byte streams, zero bytes having been read indicates graceful closure and that no more bytes will ever be read. 
            if (count == 0)
            {
                throw new EndOfStreamException();
            }

            _timer.Stop();
            return count;
        }
        catch (OperationCanceledException exc)
        {
            _timer.Stop();
            _connection.Break(exc);

            if (exc.CancellationToken == cancellationToken)
            {
                // Cancellation was initiated externally.
                throw;
            }
            else
            {
                // Cancellation was initiated by the _timer (Time out).
                throw new TimeoutException(Strings.OperationTimeout, exc);
            }
        }
        catch (Exception exc)
        {
            _timer.Stop();
            _connection.Break(exc);
            throw;
        }
    }

    #endregion

    #region Advance

    /// <summary>
    /// Advances the current buffer position by the specified number of bytes.
    /// </summary>
    /// <param name="bytes">The number of bytes to advance.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Advance(int bytes)
    {
        if (_offset + bytes >= _length)
        {
            _offset = 0;
            _length = 0;
        }
        else
        {
            _offset += bytes;
        }
    }

    #endregion

    #region DebugAssertEnoughData

    /// <summary>
    /// Checks whether the length of the existing data is greater than or equal to the specified value.
    /// </summary>
    /// <param name="binaryLength">The number of bytes expected to be contained in the buffer.</param>
    [Conditional("DEBUG")]
    internal void DebugAssertEnoughData(int binaryLength) => Debug.Assert(DataLength >= binaryLength);

    #endregion

    #region Helper

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int Min(int x, int y) => x > y ? y : x;

    #endregion

    #region TempBuffer

    /// <summary>
    /// Represents a temporary byte buffer used to receive or send data.
    /// </summary>
    private readonly struct TempByteBuffer
    {
        /// <summary>
        /// When the number of bytes to read is less than or equal to this value, 
        /// the buffer from the ArrayPool&lt;byte&gt; is used, otherwise a temporarily allocated memory is used.
        /// </summary>
        internal const int PoolThreshold = 80 * 1024;

        /// <summary>
        /// The size of the buffer.
        /// </summary>
        internal readonly int Size;

        /// <summary>
        /// The data buffer.
        /// </summary>
        internal readonly byte[] Data;

        /// <summary>
        /// Initializes a new instance of <see cref="TempByteBuffer"/> 
        /// with the specified size and underlying data array.
        /// </summary>
        /// <param name="size">The size of the buffer.</param>
        /// <param name="data">The underlying data array.</param>
        private TempByteBuffer(int size, byte[] data) : this() => (Size, Data) = (size, data);

        /// <summary>
        /// Enlarges the size of the buffer.
        /// </summary>
        /// <param name="increment">The newly increased size.</param>
        /// <param name="dataLength">The length of the existing data in the buffer.</param>
        /// <returns>The new buffer.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal TempByteBuffer Enlarge(int increment, int dataLength)
        {
            var newBuffer = Acquire(Size + increment);
            Array.Copy(Data, 0, newBuffer.Data, 0, dataLength);
            Release(in this);
            return newBuffer;
        }

        /// <summary>
        /// Gets a buffer of the specified size.
        /// </summary>
        /// <param name="size">The minimum size of the returned buffer.</param>
        /// <returns>A buffer whose underlying buffer size is at least <paramref name="size"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TempByteBuffer Acquire(int size) => size <= PoolThreshold
                ? new TempByteBuffer(size, ArrayPool<byte>.Shared.Rent(size))
                : new TempByteBuffer(size, GC.AllocateUninitializedArray<byte>(size));

        /// <summary>
        /// Returns a temporary buffer.
        /// </summary>
        /// <param name="buffer">The buffer to return.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Release(in TempByteBuffer buffer)
        {
            if (buffer.Size <= PoolThreshold)
            {
                ArrayPool<byte>.Shared.Return(buffer.Data);
            }
        }
    }

    /// <summary>
    /// Represents a temporary character buffer used to receive or send data.
    /// </summary>
    private readonly struct TempCharBuffer
    {
        /// <summary>
        /// When the number of bytes to read is less than or equal to this value, 
        /// the buffer from the ArrayPool&lt;char&gt; is used, otherwise a temporarily allocated memory is used.
        /// </summary>
        internal const int PoolThreshold = TempByteBuffer.PoolThreshold / sizeof(char);

        /// <summary>
        /// The size of the buffer.
        /// </summary>
        internal readonly int Size;

        /// <summary>
        /// The data buffer.
        /// </summary>
        internal readonly char[] Data;

        /// <summary>
        /// Initializes a new instance of <see cref="TempCharBuffer"/> 
        /// with the specified size and underlying data array.
        /// </summary>
        /// <param name="size">The size of the buffer.</param>
        /// <param name="data">The underlying data array.</param>
        private TempCharBuffer(int size, char[] data) : this() => (Size, Data) = (size, data);

        /// <summary>
        /// Gets a buffer of the specified size.
        /// </summary>
        /// <param name="size">The minimum size of the returned buffer.</param>
        /// <returns>A buffer whose underlying buffer size is at least <paramref name="size"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static TempCharBuffer Acquire(int size) => size <= PoolThreshold
            ? new TempCharBuffer(size, ArrayPool<char>.Shared.Rent(size))
            : new TempCharBuffer(size, GC.AllocateUninitializedArray<char>(size));

        /// <summary>
        /// Returns a temporary buffer.
        /// </summary>
        /// <param name="buffer">The buffer to return.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Release(in TempCharBuffer buffer)
        {
            if (buffer.Size <= PoolThreshold)
            {
                ArrayPool<char>.Shared.Return(buffer.Data);
            }
        }
    }

    #endregion
}
