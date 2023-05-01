using SingWing.PgSql.Protocol.Messages;
using SingWing.PgSql.Protocol.Messages.Frontend.Parameters;
using System.Buffers;

namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Represents a writer that can send message data to a backend.
/// </summary>
internal sealed class Writer : IDisposable
{
    #region Fields

    /// <summary>
    /// The size of the send buffer (8 KiB).
    /// </summary>
    internal const int BufferSize = 8 * 1024;

    /// <summary>
    /// The underlying connection.
    /// </summary>
    private readonly Connection _connection;

    /// <summary>
    /// The buffer used to send data to database server.
    /// </summary>
    private readonly byte[] _buffer;

    /// <summary>
    /// The number of bytes currently written to the buffer.
    /// </summary>
    private int _length;

    /// <summary>
    /// The string encoder used to encode strings and write encoded data to the buffer.
    /// </summary>
    private readonly Encoder _encoder;

    /// <summary>
    /// Signals to the CancellationToken for a data sending operation should be time out.
    /// </summary>
    private readonly TimeoutTimer _timer;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Writer"/> class
    /// with the specified underlying connection.
    /// </summary>
    /// <param name="connection">The underlying connection.</param>
    internal Writer(Connection connection)
    {
        _connection = connection;
        _buffer = new byte[BufferSize * 1024];
        _length = 0;
        _encoder = Encoding.UTF8.GetEncoder();
        _timer = new(connection.Node, TimeoutTimer.Operation.Sending);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Writer"/> class with the specified underlying buffer.
    /// </summary>
    /// <param name="buffer">The underlying buffer.</param>
    /// <remarks>Used to create <see cref="Writer"/> instances for temporary use.</remarks>
    internal Writer(byte[] buffer)
    {
        _connection = default!;
        _buffer = buffer;
        _length = 0;
        _encoder = Encoding.UTF8.GetEncoder();
        _timer = default!;
    }

    #endregion

    #region IDisposable

    /// <inheritdoc/>
    public void Dispose() => _timer.Dispose();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the size of the remaining space that has not yet been written to.
    /// </summary>
    private int EmptySpace => _buffer.Length - _length;

    #endregion

    #region WriteByte

    /// <summary>
    /// Writes a <see langword="byte"/> value to the buffer. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask WriteByteAsync(
        byte value,
        CancellationToken cancellationToken)
    {
        if (EmptySpace < sizeof(byte))
        {
            await FlushAsync(cancellationToken);
        }

        _buffer[_length++] = value;
    }

    /// <summary>
    /// Writes a <see langword="byte"/> value to the buffer. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteByte(byte value)
    {
        DebugAssertEnoughSpace(sizeof(byte));
        _buffer[_length++] = value;
    }

    /// <summary>
    /// Writes a <see langword="byte"/> value to the buffer. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="value">The value to write, between 0 and 255.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask WriteByteAsync(
        char value,
        CancellationToken cancellationToken)
    {
        Debug.Assert(value is >= (char)byte.MinValue and <= (char)byte.MaxValue);
        return WriteByteAsync((byte)value, cancellationToken);
    }

    /// <summary>
    /// Writes a <see langword="byte"/> value to the buffer. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="value">The value to write, between 0 and 255.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteByte(char value)
    {
        Debug.Assert(value is >= (char)byte.MinValue and <= (char)byte.MaxValue);
        WriteByte((byte)value);
    }

    #endregion

    #region WriteInt

    /// <summary>
    /// Writes a <see langword="short"/> value to the buffer in network byte order. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask WriteInt16Async(
        short value,
        CancellationToken cancellationToken) =>
        WriteRawIntAsync(value.ToNetworkOrder(), sizeof(short), cancellationToken);

    /// <summary>
    /// Writes a <see langword="short"/> value to the buffer in network byte order. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteInt16(short value) => WriteInt(value.ToNetworkOrder(), sizeof(short));

    /// <summary>
    /// Writes an <see langword="int"/> value to the buffer in network byte order. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask WriteInt32Async(
        int value,
        CancellationToken cancellationToken) =>
        WriteRawIntAsync(value.ToNetworkOrder(), sizeof(int), cancellationToken);

    /// <summary>
    /// Writes an <see langword="int"/> value to the buffer in network byte order. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteInt32(int value) => WriteInt(value.ToNetworkOrder(), sizeof(int));

    /// <summary>
    /// Writes an <see langword="uint"/> value to the buffer in network byte order. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteUInt32(uint value) => WriteInt32(unchecked((int)value));

    /// <summary>
    /// Writes a <see langword="long"/> value to the buffer in network byte order. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask WriteInt64Async(
        long value,
        CancellationToken cancellationToken) =>
        WriteRawIntAsync(value.ToNetworkOrder(), sizeof(long), cancellationToken);

    /// <summary>
    /// Writes a <see langword="long"/> value to the buffer without converting the byte order. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="value">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteRawInt64(long value) => WriteInt(value, sizeof(long));

    /// <summary>
    /// Writes an integer value to the buffer without converting the byte order. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <typeparam name="T">The data type of the interger value.</typeparam>
    /// <param name="value">The value to write.</param>
    /// <param name="binaryLength">The length of value in bytes.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private async ValueTask WriteRawIntAsync<T>(
        T value,
        int binaryLength,
        CancellationToken cancellationToken) where T : struct
    {
        if (EmptySpace < binaryLength)
        {
            await FlushAsync(cancellationToken);
        }

        Unsafe.WriteUnaligned(ref _buffer[_length], value);
        _length += binaryLength;
    }

    /// <summary>
    /// Writes an integer value to the buffer without converting the byte order. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <typeparam name="T">The data type of the interger value.</typeparam>
    /// <param name="value">The value to write.</param>
    /// <param name="binaryLength">The length of value in bytes.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteInt<T>(T value, int binaryLength) where T : struct
    {
        DebugAssertEnoughSpace(binaryLength);
        Unsafe.WriteUnaligned(ref _buffer[_length], value);
        _length += binaryLength;
    }

    #endregion

    #region WriteString

    /// <summary>
    /// Writes a read-only memory string value to the buffer. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="text">The string to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask WriteStringAsync(
        ReadOnlyMemory<char> text,
        CancellationToken cancellationToken)
    {
        var textLength = text.Length;
        if (textLength == 0)
        {
            return;
        }

        var charIndex = 0;
        using var charsHandle = text.Pin();
        using var bytesHandle = _buffer.AsMemory().Pin();

        while (true)
        {
            UnsafeWriteChars(
                charsHandle,
                charIndex,
                textLength - charIndex,
                bytesHandle,
                out var charsUsed,
                out var completed);

            if (completed)
            {
                break;
            }

            charIndex += charsUsed;

            // 尚未完成，表明写入缓冲区不足以容纳所有字符串的数据，
            // 将数据写入基础网络流。
            await FlushAsync(cancellationToken);
        }

        _encoder.Reset();
    }

    /// <summary>
    /// Writes a C-style string ('\0' terminated char array) value to the buffer. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="text">The string to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask WriteStringWithTerminatorAsync(
        ReadOnlyMemory<char> text,
        CancellationToken cancellationToken)
    {
        await WriteStringAsync(text, cancellationToken);
        await WriteByteAsync(0, cancellationToken);
    }

    /// <summary>
    /// Writes the encoded data of a string to the buffer in an unsafe manner.
    /// </summary>
    /// <param name="charsHandle">The pointer to the string to write.</param>
    /// <param name="charIndex">The position of the first character in the string.</param>
    /// <param name="charCount">The number of characters to write.</param>
    /// <param name="bytesHandle">The pointer to the buffer to write data to.</param>
    /// <param name="charsUsed">Returns the number of characters written.</param>
    /// <param name="completed">Indicates whether all characters have been written to the buffer.</param>
    private unsafe void UnsafeWriteChars(
        MemoryHandle charsHandle,
        int charIndex,
        int charCount,
        MemoryHandle bytesHandle,
        out int charsUsed,
        out bool completed)
    {
        var chars = (char*)charsHandle.Pointer;

        if (EmptySpace < _encoder.GetByteCount(
            chars + charIndex,
            char.IsHighSurrogate(*(chars + charIndex)) ? 2 : 1,
            flush: false))
        {
            // There is not enough space left to write a character.
            charsUsed = 0;
            completed = false;
            return;
        }

        var bytes = (byte*)bytesHandle.Pointer;

        _encoder.Convert(
            chars + charIndex,
            charCount,
            bytes + _length,
            EmptySpace,
            flush: true,
            out charsUsed,
            out var bytesUsed,
            out completed);

        _length += bytesUsed;
    }

    /// <summary>
    /// Writes a string value to the buffer. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="text">The value to write.</param>
    /// <param name="binaryLength">The length of the string binary data. If less than 0, it's calculated internally.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteString(in ReadOnlySpan<char> text, int binaryLength = -1)
    {
        if (text.Length == 0)
        {
            return;
        }

        if (binaryLength < 0)
        {
            binaryLength = Encoding.UTF8.GetByteCount(text);
        }

        if (binaryLength == 0)
        {
            return;
        }

        DebugAssertEnoughSpace(binaryLength);
        _ = Encoding.UTF8.GetBytes(text, _buffer.AsSpan(_length));
        _length += binaryLength;
    }

    /// <summary>
    /// Writes a C-style string value to the buffer. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="text">The value to write.</param>
    /// <param name="binaryLength">The length of the string binary data. If less than 0, it's calculated internally.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteStringWithTerminator(in ReadOnlySpan<char> text, int binaryLength = -1)
    {
        WriteString(text, binaryLength);
        WriteByte(0);
    }

    /// <summary>
    /// Writes a C-style string terminator to the buffer. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask WriteStringTerminatorAsync(
        CancellationToken cancellationToken) => WriteByteAsync(0, cancellationToken);

    /// <summary>
    /// Writes a C-style string terminator to the buffer. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteStringTerminator() => WriteByte(0);

    #endregion

    #region WriteBinary

    /// <summary>
    /// Writes binary data to the buffer. 
    /// If there is not enough buffer space, the data is sent first.
    /// </summary>
    /// <param name="bytes">The value to write.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask WriteBinaryAsync(
        ReadOnlyMemory<byte> bytes,
        CancellationToken cancellationToken)
    {
        while (bytes.Length > 0)
        {
            var space = EmptySpace;

            if (space == 0)
            {
                await FlushAsync(cancellationToken);
                space = _buffer.Length;
            }

            if (space >= bytes.Length)
            {
                bytes.CopyTo(_buffer.AsMemory(_length));
                _length += bytes.Length;
                break;
            }

            bytes[..space].CopyTo(_buffer.AsMemory(_length));
            _length += space;
            bytes = bytes[space..];
        }
    }

    /// <summary>
    /// Writes binary data to the buffer. 
    /// The caller should ensure that there is enough buffer space left before writing.
    /// </summary>
    /// <param name="bytes">The value to write.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void WriteBinary(in ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length == 0)
        {
            return;
        }

        DebugAssertEnoughSpace(bytes.Length);
        bytes.CopyTo(_buffer.AsSpan(_length));
        _length += bytes.Length;
    }

    #endregion

    #region WriteArray

    /// <summary>
    /// Writes the header of an array-typed value to the buffer.
    /// </summary>
    /// <param name="arrayLength">The length of array.</param>
    /// <param name="elementDataType">The data type code of the array element.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask WriteArrayHeaderAsync(
        int arrayLength,
        DataType elementDataType,
        CancellationToken cancellationToken)
    {
        if (EmptySpace < ArrayHelper.HeaderBinaryLengthForSending)
        {
            await FlushAsync(cancellationToken);
        }

        // dimensions
        WriteInt32(1);
        // has_nulls(true)/flags
        WriteInt32(1);
        // type OID
        WriteUInt32((uint)elementDataType);
        // Only one-dimensional arrays are supported, so there is only one pair of array length and lower bound.
        // array length
        WriteInt32(arrayLength);
        // lower bound
        WriteInt32(1);
    }

    #endregion

    #region WriteStream

    /// <summary>
    /// Sends binary data from the specified stream to the server using the current buffer.
    /// </summary>
    /// <param name="stream">The stream containing the data to send.</param>
    /// <param name="length">The length of the data to send.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal async ValueTask WriteStreamAsync(
        Stream stream,
        int length,
        CancellationToken cancellationToken)
    {
        while (length > 0)
        {
            if (EmptySpace == 0)
            {
                await FlushAsync(cancellationToken);
            }

            var read = await stream.ReadAsync(
                _buffer.AsMemory(_length),
                cancellationToken);

            if (read <= 0)
            {
                return;
            }

            length -= read;
            _length += read;
        }
    }

    #endregion

    #region Send Data

    /// <summary>
    /// Writes the data in the buffer to the underlying connection, and clears the buffer.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal async ValueTask FlushAsync(CancellationToken cancellationToken)
    {
        while (_length > 0)
        {
            var send = await SendAsync(
                _buffer.AsMemory(0, _length),
                cancellationToken);
            _length -= send;
        }
    }

    /// <summary>
    /// Writes the data in the specified buffer to the underlying connection.
    /// </summary>
    /// <param name="buffer">The buffer for the data to send.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <exception cref="EndOfStreamException">
    /// Reading is attempted past the end of the network stream.
    /// </exception>
    /// <returns>The number of bytes sent.</returns>
    private async ValueTask<int> SendAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken)
    {
        var token = _timer.Start(cancellationToken);

        try
        {
            var count = await _connection.Socket.SendAsync(
                buffer,
                SocketFlags.None,
                token);

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

    #region DebugAssertEnoughSpace

    /// <summary>
    /// Checks whether there is enough space left in the buffer to hold the specified number of bytes.
    /// </summary>
    /// <param name="binaryLength">The number of bytes to be written.</param>
    [Conditional("DEBUG")]
    internal void DebugAssertEnoughSpace(int binaryLength) => Debug.Assert(binaryLength <= EmptySpace);

    #endregion

    #region Helper

    /// <summary>
    /// Ensures that there is enough space in the write buffer.
    /// </summary>
    /// <param name="size">The number of bytes to reserve in the buffer.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal ValueTask EnsureSpaceAsync(
        int size,
        CancellationToken cancellationToken) =>
        EmptySpace < size ? FlushAsync(cancellationToken) : ValueTask.CompletedTask;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int Min(int x, int y) => x > y ? y : x;

    #endregion
}
