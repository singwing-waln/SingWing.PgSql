namespace SingWing.PgSql.Protocol.Messages.DataTypes;

/// <summary>
/// When setting <see cref="Parameter.Value"/>, 
/// converts an <see langword="object"/>? to a value that can be sent to PostgreSQL.
/// </summary>
internal static class Conversion
{
    /// <summary>
    /// Converts the specified object to a <see langword="bool"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="bool"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static bool? ToBoolean(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is bool @bool)
        {
            return @bool;
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte != 0;
        }

        if (value is byte @byte)
        {
            return @byte != 0;
        }

        if (value is short @short)
        {
            return @short != 0;
        }

        if (value is ushort @ushort)
        {
            return @ushort != 0;
        }

        if (value is int @int)
        {
            return @int != 0;
        }

        if (value is uint @uint)
        {
            return @uint != 0;
        }

        if (value is long @long)
        {
            return @long != 0;
        }

        if (value is ulong @ulong)
        {
            return @ulong != 0;
        }

        if (value is nint @nint)
        {
            return @nint != 0;
        }

        if (value is nuint @nuint)
        {
            return @nuint != 0;
        }

        if (value is Int128 @int128)
        {
            return @int128 != 0;
        }

        if (value is UInt128 @uint128)
        {
            return @uint128 != 0;
        }

        if (value is char @char)
        {
            if (@char is 'T' or 't')
            {
                return true;
            }

            if (@char is 'F' or 'f')
            {
                return false;
            }

            return null;
        }

        if (value is float @float)
        {
            return @float != 0;
        }

        if (value is double @double)
        {
            return @double != 0;
        }

        if (value is decimal @decimal)
        {
            return @decimal != 0;
        }

        if (value is Half half)
        {
            return half != Half.Zero;
        }

        if (value is BigInteger bigInt)
        {
            return bigInt != BigInteger.Zero;
        }

        if (value is string @string)
        {
            return StringToBoolean(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToBoolean(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToBoolean(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToBoolean(arraySegment.AsSpan());
        }

        if (value is IConvertible convertible)
        {
            try
            {
                return convertible.ToBoolean(null);
            }
            catch
            {
            }
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static bool? StringToBoolean(in ReadOnlySpan<char> s)
        {
            if (s.IsEmpty)
            {
                return null;
            }

            if (s.EqualsIgnoreCase("true") ||
                s.EqualsIgnoreCase("t") ||
                s.EqualsIgnoreCase("1") ||
                s.EqualsIgnoreCase("yes") ||
                s.EqualsIgnoreCase("y") ||
                s.EqualsIgnoreCase("on"))
            {
                return true;
            }

            if (s.EqualsIgnoreCase("false") ||
                s.EqualsIgnoreCase("f") ||
                s.EqualsIgnoreCase("0") ||
                s.EqualsIgnoreCase("no") ||
                s.EqualsIgnoreCase("n") ||
                s.EqualsIgnoreCase("off"))
            {
                return false;
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a ReadOnlyMemory&lt;byte&gt;? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted ReadOnlyMemory&lt;byte&gt;? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static ReadOnlyMemory<byte>? ToByteMemory(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is ReadOnlyMemory<byte> readonlyMemory)
        {
            return readonlyMemory;
        }

        if (value is Memory<byte> memory)
        {
            return memory;
        }

        if (value is byte[] bytes)
        {
            return bytes.AsMemory();
        }

        if (value is ArraySegment<byte> arraySegment)
        {
            return arraySegment.AsMemory();
        }

        if (value is string @string)
        {
            return StringToBinary(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyChars)
        {
            return StringToBinary(readonlyChars.Span);
        }

        if (value is Memory<char> chars)
        {
            return StringToBinary(chars.Span);
        }

        if (value is ArraySegment<char> charArraySegment)
        {
            return StringToBinary(charArraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ReadOnlyMemory<byte>? StringToBinary(in ReadOnlySpan<char> s)
        {
            if (s.IsEmpty)
            {
                return ReadOnlyMemory<byte>.Empty;
            }

            if (s.Length >= 2 && s[0] == '0' && (s[1] == 'x' || s[1] == 'X'))
            {
                return HexStringToBinary(s[2..]);
            }

            ReadOnlyMemory<byte>? binary = null;
            if (s.Length % 2 == 0)
            {
                binary = HexStringToBinary(s);
            }

            binary ??= Base64StringToBinary(s);

            return binary;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ReadOnlyMemory<byte>? HexStringToBinary(in ReadOnlySpan<char> s)
        {
            try
            {
                return Convert.FromHexString(s);
            }
            catch
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ReadOnlyMemory<byte>? Base64StringToBinary(in ReadOnlySpan<char> s)
        {
            var binary = GC.AllocateUninitializedArray<byte>((s.Length >> 2) * 3);
            if (Convert.TryFromBase64Chars(s, binary, out var length))
            {
                return binary.AsMemory(0, length);
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a DateOnly? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted DateOnly? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static DateOnly? ToDateOnly(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is DateOnly date)
        {
            return date;
        }

        if (value is DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime.ToUniversalTime());
        }

        if (value is DateTimeOffset dateTimeOffset)
        {
            return DateOnly.FromDateTime(dateTimeOffset.UtcDateTime);
        }

        if (value is TimeSpan timeSpan)
        {
            return DateOnly.FromDayNumber(unchecked((int)(timeSpan.Ticks / TimeSpan.TicksPerDay)));
        }

        if (value is int @int)
        {
            return Int64ToDateOnly(@int);
        }

        if (value is uint @uint)
        {
            return Int64ToDateOnly(@uint);
        }

        if (value is long @long)
        {
            return Int64ToDateOnly(@long);
        }

        if (value is ulong @ulong)
        {
            return @ulong > long.MaxValue ? DateOnly.MaxValue : Int64ToDateOnly(unchecked((long)@ulong));
        }

        if (value is nint @nint)
        {
            return Int64ToDateOnly(@nint);
        }

        if (value is nuint @nuint)
        {
            return @nuint > long.MaxValue ? DateOnly.MaxValue : Int64ToDateOnly(unchecked((long)@nuint));
        }

        if (value is Int128 @int128)
        {
            return @int128 > long.MaxValue ? DateOnly.MaxValue : Int64ToDateOnly(unchecked((long)@int128));
        }

        if (value is UInt128 @uint128)
        {
            return @uint128 > long.MaxValue ? DateOnly.MaxValue : Int64ToDateOnly(unchecked((long)@uint128));
        }

        if (value is string @string)
        {
            return StringToDateOnly(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToDateOnly(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToDateOnly(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToDateOnly(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static DateOnly? StringToDateOnly(in ReadOnlySpan<char> s)
        {
            if (DateOnly.TryParse(s, out var date))
            {
                return date;
            }

            if (DateTime.TryParse(s, out var dateTime))
            {
                return DateOnly.FromDateTime(dateTime.ToUniversalTime());
            }

            if (DateTimeOffset.TryParse(s, out var dateTimeOffset))
            {
                return DateOnly.FromDateTime(dateTimeOffset.UtcDateTime);
            }

            if (TimeSpan.TryParse(s, out var timeSpan))
            {
                return DateOnly.FromDayNumber(
                    unchecked((int)(timeSpan.Ticks / TimeSpan.TicksPerDay)));
            }

            if (long.TryParse(s, out var @long))
            {
                return Int64ToDateOnly(@long);
            }

            return null;
        }

        // An integer value in the format yyyymmdd.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static DateOnly? Int64ToDateOnly(long value)
        {
            const long MinValue = 0001_01_01L;
            const long MaxValue = 9999_12_31L;

            if (value <= MinValue)
            {
                return DateOnly.MinValue;
            }

            if (value >= MaxValue)
            {
                return DateOnly.MaxValue;
            }

            var day = (int)(value % 100);
            var month = (int)(value / 100 % 100);
            var year = (int)(value / 10_000);

            try
            {
                return new DateOnly(year, month, day);
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Converts the specified object to a DateTime? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted DateTime? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static DateTime? ToDateTime(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is DateTime dateTime)
        {
            return dateTime.ToUniversalTime();
        }

        if (value is DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.UtcDateTime;
        }

        if (value is DateOnly date)
        {
            return date.ToDateTime(TimeOnly.MinValue).ToUniversalTime();
        }

        if (value is TimeSpan timeSpan)
        {
            return new DateTime(timeSpan.Ticks);
        }

        if (value is long @long)
        {
            return Int64ToDateTime(@long);
        }

        if (value is ulong @ulong)
        {
            return @ulong > long.MaxValue ? DateTime.MaxValue.ToUniversalTime() : Int64ToDateTime(unchecked((long)@ulong));
        }

        if (value is nint @nint)
        {
            return Int64ToDateTime(@nint);
        }

        if (value is nuint @nuint)
        {
            return @nuint > long.MaxValue ? DateTime.MaxValue.ToUniversalTime() : Int64ToDateTime(unchecked((long)@nuint));
        }

        if (value is Int128 @int128)
        {
            return @int128 > long.MaxValue ? DateTime.MaxValue.ToUniversalTime() : Int64ToDateTime(unchecked((long)@int128));
        }

        if (value is UInt128 @uint128)
        {
            return @uint128 > long.MaxValue ? DateTime.MaxValue.ToUniversalTime() : Int64ToDateTime(unchecked((long)@uint128));
        }

        if (value is string @string)
        {
            return StringToDateTime(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToDateTime(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToDateTime(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToDateTime(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static DateTime? StringToDateTime(in ReadOnlySpan<char> s)
        {
            if (DateTime.TryParse(s, out var dateTime))
            {
                return dateTime.ToUniversalTime();
            }

            if (DateTimeOffset.TryParse(s, out var dateTimeOffset))
            {
                return dateTimeOffset.UtcDateTime;
            }

            if (DateOnly.TryParse(s, out var date))
            {
                return date.ToDateTime(TimeOnly.MinValue).ToUniversalTime();
            }

            if (TimeSpan.TryParse(s, out var timeSpan))
            {
                return new DateTime(timeSpan.Ticks);
            }

            if (long.TryParse(s, out var @long))
            {
                return Int64ToDateTime(@long);
            }

            return null;
        }

        // An integer value in the format yyyyMMddHHmmsszzz.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static DateTime? Int64ToDateTime(long value)
        {
            const long MinValue = 0001_01_01_00_00_00_000L;
            const long MaxValue = 9999_12_31_23_59_59_999L;

            if (value < MinValue)
            {
                return DateTime.MinValue.ToUniversalTime();
            }

            if (value > MaxValue)
            {
                return DateTime.MaxValue.ToUniversalTime();
            }

            var millisecond = (int)(value % 1_000L);
            var second = (int)(value / 1_000L % 100);
            var minute = (int)(value / 100_000L % 100);
            var hour = (int)(value / 10_000_000L % 100);
            var day = (int)(value / 1_000_000_000L % 100);
            var month = (int)(value / 100_000_000_000L % 100);
            var year = (int)(value / 10_000_000_000_000L);

            try
            {
                return new DateTime(year, month, day, hour, minute, second, millisecond).ToUniversalTime();
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Converts the specified object to a TimeOnly? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted TimeOnly? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static TimeOnly? ToTimeOnly(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is TimeOnly time)
        {
            return time;
        }

        if (value is DateTime dateTime)
        {
            return TimeOnly.FromDateTime(dateTime.ToUniversalTime());
        }

        if (value is DateTimeOffset dateTimeOffset)
        {
            return TimeOnly.FromDateTime(dateTimeOffset.UtcDateTime);
        }

        if (value is TimeSpan timeSpan)
        {
            try
            {
                return TimeOnly.FromTimeSpan(timeSpan);
            }
            catch
            {
                return null;
            }
        }

        if (value is int @int)
        {
            return Int64ToTimeOnly(@int);
        }

        if (value is uint @uint)
        {
            return Int64ToTimeOnly(@uint);
        }

        if (value is long @long)
        {
            return Int64ToTimeOnly(@long);
        }

        if (value is ulong @ulong)
        {
            return @ulong > long.MaxValue ? TimeOnly.MaxValue : Int64ToTimeOnly(unchecked((long)@ulong));
        }

        if (value is nint @nint)
        {
            return Int64ToTimeOnly(@nint);
        }

        if (value is nuint @nuint)
        {
            return @nuint > long.MaxValue ? TimeOnly.MaxValue : Int64ToTimeOnly(unchecked((long)@nuint));
        }

        if (value is Int128 @int128)
        {
            return @int128 > long.MaxValue ? TimeOnly.MaxValue : Int64ToTimeOnly(unchecked((long)@int128));
        }

        if (value is UInt128 @uint128)
        {
            return @uint128 > long.MaxValue ? TimeOnly.MaxValue : Int64ToTimeOnly(unchecked((long)@uint128));
        }

        if (value is string @string)
        {
            return StringToTimeOnly(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToTimeOnly(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToTimeOnly(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToTimeOnly(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TimeOnly? StringToTimeOnly(in ReadOnlySpan<char> s)
        {
            if (TimeOnly.TryParse(s, out var time))
            {
                return time;
            }

            if (DateTime.TryParse(s, out var dateTime))
            {
                return TimeOnly.FromDateTime(dateTime.ToUniversalTime());
            }

            if (DateTimeOffset.TryParse(s, out var dateTimeOffset))
            {
                return TimeOnly.FromDateTime(dateTimeOffset.UtcDateTime);
            }

            if (TimeSpan.TryParse(s, out var timeSpan))
            {
                try
                {
                    return TimeOnly.FromTimeSpan(timeSpan);
                }
                catch
                {
                    return null;
                }
            }

            if (long.TryParse(s, out var @long))
            {
                return Int64ToTimeOnly(@long);
            }

            return null;
        }

        // An integer value in the format hhmmsszzz.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TimeOnly? Int64ToTimeOnly(long value)
        {
            const long MinValue = 00_00_00_000L;
            const long MaxValue = 23_59_59_999L;

            if (value < MinValue)
            {
                return TimeOnly.MinValue;
            }

            if (value > MaxValue)
            {
                return TimeOnly.MaxValue;
            }

            var millisecond = (int)(value % 1_000L);
            var second = (int)(value / 1_000L % 100);
            var minute = (int)(value / 100_000L % 100);
            var hour = (int)(value / 10_000_000L % 100);

            try
            {
                return new TimeOnly(hour, minute, second, millisecond);
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Converts the specified object to a TimeSpan? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted TimeSpan? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static TimeSpan? ToTimeSpan(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is TimeSpan timeSpan)
        {
            return timeSpan;
        }

        if (value is TimeOnly time)
        {
            return TimeSpan.FromTicks(time.Ticks);
        }

        if (value is DateTime dateTime)
        {
            return TimeSpan.FromTicks(dateTime.ToUniversalTime().Ticks);
        }

        if (value is DateTimeOffset dateTimeOffset)
        {
            return TimeSpan.FromTicks(dateTimeOffset.UtcDateTime.Ticks);
        }

        if (value is DateOnly date)
        {
            return TimeSpan.FromTicks(date.DayNumber * TimeSpan.TicksPerDay);
        }

        if (value is long @long)
        {
            return Int64ToTimeSpan(@long);
        }

        if (value is ulong @ulong)
        {
            return @ulong > long.MaxValue ? TimeSpan.MaxValue : Int64ToTimeSpan(unchecked((long)@ulong));
        }

        if (value is nint @nint)
        {
            return Int64ToTimeSpan(@nint);
        }

        if (value is nuint @nuint)
        {
            return @nuint > long.MaxValue ? TimeSpan.MaxValue : Int64ToTimeSpan(unchecked((long)@nuint));
        }

        if (value is Int128 @int128)
        {
            return @int128 > long.MaxValue ? TimeSpan.MaxValue : Int64ToTimeSpan(unchecked((long)@int128));
        }

        if (value is UInt128 @uint128)
        {
            return @uint128 > long.MaxValue ? TimeSpan.MaxValue : Int64ToTimeSpan(unchecked((long)@uint128));
        }

        if (value is string @string)
        {
            return StringToTimeSpan(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToTimeSpan(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToTimeSpan(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToTimeSpan(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TimeSpan? StringToTimeSpan(in ReadOnlySpan<char> s)
        {
            if (TimeSpan.TryParse(s, out var timeSpan))
            {
                return timeSpan;
            }

            if (TimeOnly.TryParse(s, out var time))
            {
                return new TimeSpan(time.Ticks);
            }

            if (DateTime.TryParse(s, out var dateTime))
            {
                return new TimeSpan(dateTime.ToUniversalTime().Ticks);
            }

            if (DateTimeOffset.TryParse(s, out var dateTimeOffset))
            {
                return new TimeSpan(dateTimeOffset.UtcDateTime.Ticks);
            }

            if (DateOnly.TryParse(s, out var date))
            {
                return new TimeSpan(date.DayNumber * TimeSpan.TicksPerDay);
            }

            if (long.TryParse(s, out var @long))
            {
                return Int64ToTimeSpan(@long);
            }

            return null;
        }

        // An integer value in the format ddddddddhhmmsszzz.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TimeSpan? Int64ToTimeSpan(long value)
        {
            const long MinValue = -10_675_199_02_48_05_477L;
            const long MaxValue = 10_675_199_02_48_05_477L;

            if (value < MinValue)
            {
                return TimeSpan.MinValue;
            }

            if (value > MaxValue)
            {
                return TimeSpan.MaxValue;
            }

            var milliseconds = (int)(value % 1_000L);
            var seconds = (int)(value / 1_000L % 100);
            var minutes = (int)(value / 100_000L % 100);
            var hours = (int)(value / 10_000_000L % 100);
            var days = (int)(value / 1_000_000_000L);

            try
            {
                return days < 0
                    ? -new TimeSpan(-days, -hours, -minutes, -seconds, -milliseconds)
                    : new TimeSpan(days, hours, minutes, seconds, milliseconds);
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Converts the specified object to a <see langword="double"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="double"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static double? ToDouble(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is double @double)
        {
            return @double;
        }

        if (value is float @float)
        {
            return @float;
        }

        if (value is decimal @decimal)
        {
            return unchecked((double)@decimal);
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte;
        }

        if (value is byte @byte)
        {
            return @byte;
        }

        if (value is short @short)
        {
            return @short;
        }

        if (value is ushort @ushort)
        {
            return @ushort;
        }

        if (value is int @int)
        {
            return @int;
        }

        if (value is uint @uint)
        {
            return @uint;
        }

        if (value is long @long)
        {
            return @long;
        }

        if (value is ulong @ulong)
        {
            return @ulong;
        }

        if (value is nint @nint)
        {
            return @nint;
        }

        if (value is nuint @nuint)
        {
            return @nuint;
        }

        if (value is Int128 @int128)
        {
            return unchecked((double)@int128);
        }

        if (value is UInt128 @uint128)
        {
            return unchecked((double)@uint128);
        }

        if (value is Half half)
        {
            return (double)half;
        }

        if (value is BigInteger bigInt)
        {
            return unchecked((double)bigInt);
        }

        if (value is string @string)
        {
            return StringToDouble(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToDouble(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToDouble(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToDouble(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static double? StringToDouble(in ReadOnlySpan<char> s) =>
            double.TryParse(s, out var @double) ? @double : null;
    }

    /// <summary>
    /// Converts the specified object to a <see langword="float"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="float"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static float? ToSingle(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is float @float)
        {
            return @float;
        }

        if (value is double @double)
        {
            return unchecked((float)@double);
        }

        if (value is decimal @decimal)
        {
            return unchecked((float)@decimal);
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte;
        }

        if (value is byte @byte)
        {
            return @byte;
        }

        if (value is short @short)
        {
            return @short;
        }

        if (value is ushort @ushort)
        {
            return @ushort;
        }

        if (value is int @int)
        {
            return @int;
        }

        if (value is uint @uint)
        {
            return @uint;
        }

        if (value is long @long)
        {
            return @long;
        }

        if (value is ulong @ulong)
        {
            return @ulong;
        }

        if (value is nint @nint)
        {
            return @nint;
        }

        if (value is nuint @nuint)
        {
            return @nuint;
        }

        if (value is Int128 @int128)
        {
            return unchecked((float)@int128);
        }

        if (value is UInt128 @uint128)
        {
            return unchecked((float)@uint128);
        }

        if (value is Half half)
        {
            return (float)half;
        }

        if (value is BigInteger bigInt)
        {
            return unchecked((float)bigInt);
        }

        if (value is string @string)
        {
            return StringToSingle(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToSingle(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToSingle(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToSingle(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static float? StringToSingle(in ReadOnlySpan<char> s)
        {
            if (float.TryParse(s, out var @float))
            {
                return @float;
            }

            if (double.TryParse(s, out var @double))
            {
                return unchecked((float)@double);
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a <see langword="short"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="short"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static short? ToInt16(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is short @short)
        {
            return @short;
        }

        if (value is ushort @ushort)
        {
            return unchecked((short)@ushort);
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte;
        }

        if (value is byte @byte)
        {
            return @byte;
        }

        if (value is int @int)
        {
            return unchecked((short)@int);
        }

        if (value is uint @uint)
        {
            return unchecked((short)@uint);
        }

        if (value is long @long)
        {
            return unchecked((short)@long);
        }

        if (value is ulong @ulong)
        {
            return unchecked((short)@ulong);
        }

        if (value is nint @nint)
        {
            return unchecked((short)@nint);
        }

        if (value is nuint @nuint)
        {
            return unchecked((short)@nuint);
        }

        if (value is float @float)
        {
            return unchecked((short)@float);
        }

        if (value is double @double)
        {
            return unchecked((short)@double);
        }

        if (value is decimal @decimal)
        {
            return unchecked((short)@decimal);
        }

        if (value is Int128 @int128)
        {
            return unchecked((short)@int128);
        }

        if (value is UInt128 @uint128)
        {
            return unchecked((short)@uint128);
        }

        if (value is Half half)
        {
            return unchecked((short)(double)half);
        }

        if (value is BigInteger bigInt)
        {
            return unchecked((short)bigInt);
        }

        if (value is string @string)
        {
            return StringToInt16(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToInt16(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToInt16(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToInt16(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static short? StringToInt16(in ReadOnlySpan<char> s)
        {
            if (short.TryParse(s, out var @short))
            {
                return @short;
            }

            if (long.TryParse(s, out var @long))
            {
                return unchecked((short)@long);
            }

            if (double.TryParse(s, out var @double))
            {
                return unchecked((short)@double);
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a <see langword="int"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="int"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static int? ToInt32(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is int @int)
        {
            return @int;
        }

        if (value is uint @uint)
        {
            return unchecked((int)@uint);
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte;
        }

        if (value is byte @byte)
        {
            return @byte;
        }

        if (value is short @short)
        {
            return @short;
        }

        if (value is ushort @ushort)
        {
            return @ushort;
        }

        if (value is long @long)
        {
            return unchecked((int)@long);
        }

        if (value is ulong @ulong)
        {
            return unchecked((int)@ulong);
        }

        if (value is nint @nint)
        {
            return unchecked((int)@nint);
        }

        if (value is nuint @nuint)
        {
            return unchecked((int)@nuint);
        }

        if (value is float @float)
        {
            return unchecked((int)@float);
        }

        if (value is double @double)
        {
            return unchecked((int)@double);
        }

        if (value is decimal @decimal)
        {
            return unchecked((int)@decimal);
        }

        if (value is Int128 @int128)
        {
            return unchecked((int)@int128);
        }

        if (value is UInt128 @uint128)
        {
            return unchecked((int)@uint128);
        }

        if (value is Half half)
        {
            return unchecked((int)(double)half);
        }

        if (value is BigInteger bigInt)
        {
            return unchecked((int)bigInt);
        }

        if (value is string @string)
        {
            return StringToInt32(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToInt32(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToInt32(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToInt32(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int? StringToInt32(in ReadOnlySpan<char> s)
        {
            if (int.TryParse(s, out var @int))
            {
                return @int;
            }

            if (long.TryParse(s, out var @long))
            {
                return unchecked((int)@long);
            }

            if (double.TryParse(s, out var @double))
            {
                return unchecked((int)@double);
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a <see langword="long"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="long"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static long? ToInt64(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is long @long)
        {
            return @long;
        }

        if (value is ulong @ulong)
        {
            return unchecked((long)@ulong);
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte;
        }

        if (value is byte @byte)
        {
            return @byte;
        }

        if (value is short @short)
        {
            return @short;
        }

        if (value is ushort @ushort)
        {
            return @ushort;
        }

        if (value is int @int)
        {
            return @int;
        }

        if (value is uint @uint)
        {
            return @uint;
        }

        if (value is nint @nint)
        {
            return @nint;
        }

        if (value is nuint @nuint)
        {
            return unchecked((long)@nuint);
        }

        if (value is float @float)
        {
            return unchecked((long)@float);
        }

        if (value is double @double)
        {
            return unchecked((long)@double);
        }

        if (value is decimal @decimal)
        {
            return unchecked((long)@decimal);
        }

        if (value is Int128 @int128)
        {
            return unchecked((long)@int128);
        }

        if (value is UInt128 @uint128)
        {
            return unchecked((long)@uint128);
        }

        if (value is Half half)
        {
            return unchecked((long)(double)half);
        }

        if (value is BigInteger bigInt)
        {
            return unchecked((long)bigInt);
        }

        if (value is string @string)
        {
            return StringToInt64(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToInt64(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToInt64(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToInt64(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static long? StringToInt64(in ReadOnlySpan<char> s)
        {
            if (long.TryParse(s, out var @long))
            {
                return @long;
            }

            if (double.TryParse(s, out var @double))
            {
                return unchecked((long)@double);
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a <see langword="decimal"/>? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted <see langword="decimal"/>? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static decimal? ToDecimal(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is decimal @decimal)
        {
            return @decimal;
        }

        if (value is sbyte @sbyte)
        {
            return @sbyte;
        }

        if (value is byte @byte)
        {
            return @byte;
        }

        if (value is short @short)
        {
            return @short;
        }

        if (value is ushort @ushort)
        {
            return @ushort;
        }

        if (value is int @int)
        {
            return @int;
        }

        if (value is uint @uint)
        {
            return @uint;
        }

        if (value is long @long)
        {
            return @long;
        }

        if (value is ulong @ulong)
        {
            return @ulong;
        }

        if (value is nint @nint)
        {
            return @nint;
        }

        if (value is nuint @nuint)
        {
            return @nuint;
        }

        if (value is float @float)
        {
            return unchecked((decimal)@float);
        }

        if (value is double @double)
        {
            return unchecked((decimal)@double);
        }

        if (value is Int128 @int128)
        {
            return unchecked((decimal)@int128);
        }

        if (value is UInt128 @uint128)
        {
            return unchecked((decimal)@uint128);
        }

        if (value is Half half)
        {
            return unchecked((decimal)(double)half);
        }

        if (value is BigInteger bigInt)
        {
            return unchecked((decimal)bigInt);
        }

        if (value is string @string)
        {
            return StringToDecimal(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToDecimal(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToDecimal(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToDecimal(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static decimal? StringToDecimal(in ReadOnlySpan<char> s)
        {
            if (decimal.TryParse(s, out var @decimal))
            {
                return @decimal;
            }

            if (double.TryParse(s, out var @double))
            {
                return unchecked((decimal)@double);
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a Guid? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted Guid? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static Guid? ToGuid(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is Guid guid)
        {
            return guid;
        }

        if (value is Int128 @int128)
        {
            Span<byte> bytes = stackalloc byte[16];
            _ = ((IBinaryInteger<Int128>)@int128).WriteBigEndian(bytes);
            return new Guid(bytes);
        }

        if (value is UInt128 @uint128)
        {
            Span<byte> bytes = stackalloc byte[16];
            _ = ((IBinaryInteger<UInt128>)@uint128).WriteBigEndian(bytes);
            return new Guid(bytes);
        }

        if (value is string @string)
        {
            return StringToGuid(@string.AsSpan());
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return StringToGuid(readonlyMemory.Span);
        }

        if (value is Memory<char> memory)
        {
            return StringToGuid(memory.Span);
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return StringToGuid(arraySegment.AsSpan());
        }

        return null;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static Guid? StringToGuid(in ReadOnlySpan<char> s)
        {
            if (Guid.TryParse(s, out var guid))
            {
                return guid;
            }

            return null;
        }
    }

    /// <summary>
    /// Converts the specified object to a ReadOnlyMemory&lt;char&gt;? value.
    /// </summary>
    /// <param name="value">The object to convert.</param>
    /// <returns>
    /// The converted ReadOnlyMemory&lt;char&gt;? value, or <see langword="null"/> if no conversion is possible.
    /// </returns>
    internal static ReadOnlyMemory<char>? ToCharMemory(object? value)
    {
        if (value is null)
        {
            return null;
        }

        if (value is ReadOnlyMemory<char> readonlyMemory)
        {
            return readonlyMemory;
        }

        if (value is Memory<char> memory)
        {
            return memory;
        }

        if (value is string @string)
        {
            return @string.AsMemory();
        }

        if (value is char[] chars)
        {
            return chars.AsMemory();
        }

        if (value is ArraySegment<char> arraySegment)
        {
            return arraySegment.AsMemory();
        }

        if (value is byte[] bytes)
        {
            return BinaryToString(bytes.AsSpan());
        }

        if (value is ReadOnlyMemory<byte> byteReadonlyMemory)
        {
            return BinaryToString(byteReadonlyMemory.Span);
        }

        if (value is Memory<byte> byteMemory)
        {
            return BinaryToString(byteMemory.Span);
        }

        if (value is ArraySegment<byte> byteArraySegment)
        {
            return BinaryToString(byteArraySegment.AsSpan());
        }

        return value.ToString().AsMemory();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static ReadOnlyMemory<char> BinaryToString(in ReadOnlySpan<byte> bytes)
        {
            if (bytes.IsEmpty)
            {
                return ReadOnlyMemory<char>.Empty;
            }

            try
            {
                return Convert.ToHexString(bytes).AsMemory();
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Converts the specified value to the specified type.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    /// <param name="dataType">The data type of the converted value.</param>
    /// <returns>The converted value, or <see langword="null"/> if conversion is not possible.</returns>
    internal static object? ToElement(object? value, DataType dataType) => dataType switch
    {
        DataType.Int2 => ToInt16(value),
        DataType.Int4 => ToInt32(value),
        DataType.Int8 => ToInt64(value),
        DataType.Float4 => ToSingle(value),
        DataType.Float8 => ToDouble(value),
        DataType.Numeric => ToDecimal(value),
        DataType.Boolean => ToBoolean(value),
        DataType.Varchar => ToCharMemory(value),
        DataType.Bytea => ToByteMemory(value),
        DataType.Date => ToDateOnly(value),
        DataType.Time => ToTimeOnly(value),
        DataType.TimestampTz => ToDateTime(value),
        DataType.Interval => ToTimeSpan(value),
        DataType.Uuid => ToGuid(value),
        DataType.Jsonb => ToByteMemory(value),
        _ => null,
    };

    /// <summary>
    /// Compares two strings for equality, case-insensitive.
    /// </summary>
    /// <param name="x">The first string to compare.</param>
    /// <param name="y">The second string to compare.</param>
    /// <returns><see langword="true"/> if the two strings are equal, <see langword="false"/> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EqualsIgnoreCase(this in ReadOnlySpan<char> x, in ReadOnlySpan<char> y) =>
        x.Equals(y, StringComparison.OrdinalIgnoreCase);
}
