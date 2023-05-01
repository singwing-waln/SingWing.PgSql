namespace SingWing.PgSql.Protocol.Messages.Frontend.Parameters;

/// <summary>
/// Provides helper methods for parameter values of array type.
/// </summary>
internal static class ArrayHelper
{
    /// <summary>
    /// The number of bytes occupied by the array type value header in the data received from PostgreSQL servers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// array_send:
    /// <see href="https://doxygen.postgresql.org/arrayfuncs_8c.html#a98c4fd6e001257fcec018241c95fdfc4"/>.
    /// </para>
    /// <para>
    /// We do not support multidimensional arrays, so when a multidimensional array is received from the server, 
    /// the array data will be discarded and the value will be set to <see langword="null"/>.
    /// </para>
    /// <para>
    /// When an array is empty, the complete column data will not contain the length and lower bound of the first dimension.
    /// </para>
    /// </remarks>
    internal const int HeaderBinaryLengthForReceiving =
        // dimensions (ndim = 1)
        sizeof(int) +
        // has_nulls (flags)
        sizeof(int) +
        // type OID (element_type)
        sizeof(uint);

    /// <summary>
    /// The number of bytes occupied by the array type value header in the Bind message sent to PostgreSQL servers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// array_recv:
    /// <see href="https://doxygen.postgresql.org/arrayfuncs_8c.html#a315b67e6e01e8f283326b5a6b27e07c9"/>.
    /// </para>
    /// </remarks>
    internal const int HeaderBinaryLengthForSending =
        // dimensions (ndim = 1)
        sizeof(int) +
        // has_nulls (flags)
        sizeof(int) +
        // type OID (element_type)
        sizeof(uint) +
        // ndim(1) * (array length + lower bound)
        sizeof(int) + sizeof(int);

    /// <summary>
    /// Checks whether a collection is empty.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements in the collection.</typeparam>
    /// <param name="elements">The collection to check.</param>
    /// <returns><see langword="true"/> is the collection is empty, <see langword="false"/> otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsEmpty<TElement>(this IEnumerable<TElement> elements) => !elements.Any();
}
