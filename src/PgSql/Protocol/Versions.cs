namespace SingWing.PgSql.Protocol;

/// <summary>
/// Provides the protocol version number and jsonb version number.
/// </summary>
internal static class Versions
{
    /// <summary>
    /// The version number of protocol (3.0).
    /// </summary>
    /// <remarks>
    /// The most significant 16 bits are the major version number (3 for the protocol described here). 
    /// The least significant 16 bits are the minor version number (0 for the protocol described here).
    /// </remarks>
    internal const int ProtocolVersion = 3 << 16;

    /// <summary>
    /// The version number of PostgreSQL jsonb (1).
    /// </summary>
    /// <remarks>
    /// <code>if (version == 1) ... else ERROR</code>
    /// <para>
    /// <see href="https://doxygen.postgresql.org/jsonb_8c.html#a144090a28d298e1f60399ae47c67979e"/>.
    /// </para>
    /// </remarks>
    internal const byte JsonbVersion = 1;
}
