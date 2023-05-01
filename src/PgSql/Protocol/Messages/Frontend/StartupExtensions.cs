using SingWing.PgSql.Protocol.Connections;

namespace SingWing.PgSql.Protocol.Messages.Frontend;

/// <summary>
/// Provides methods to send Startup messages to database servers.
/// </summary>
internal static class StartupExtensions
{
    #region Constants

    /// <summary>
    /// The binary data of the "user" string.
    /// </summary>
    private static readonly byte[] UserNameBinary =
        Encoding.UTF8.GetBytes("user");

    /// <summary>
    /// The binary data of the "database" string.
    /// </summary>
    private static readonly byte[] DatabaseNameBinary =
        Encoding.UTF8.GetBytes("database");

    /// <summary>
    /// The binary data of the "application_name" string.
    /// </summary>
    private static readonly byte[] ApplicationNameNameBinary =
        Encoding.UTF8.GetBytes("application_name");

    /// <summary>
    /// The binary data of the "client_encoding" string.
    /// </summary>
    private static readonly byte[] ClientEncodingNameBinary =
        Encoding.UTF8.GetBytes("client_encoding");

    /// <summary>
    /// The binary data of the "UTF8" string.
    /// </summary>
    private static readonly byte[] ClientEncodingValueBinary =
        Encoding.UTF8.GetBytes("UTF8");

    #endregion

    /// <summary>
    /// Sends the Startup message to the specified server.
    /// </summary>
    /// <param name="writer">The output writer connected to the server.</param>
    /// <param name="databaseName">The database name.</param>
    /// <param name="userName">The database use name.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    internal static ValueTask SendStartupMessageAsync(
        this Writer writer,
        string databaseName,
        string userName,
        CancellationToken cancellationToken)
    {
        // https://www.postgresql.org/docs/current/libpq-connect.html#LIBPQ-PARAMKEYWORDS

        var userValueBinaryLength = Encoding.UTF8.GetByteCount(userName);
        var databaseValueBinaryLength = Encoding.UTF8.GetByteCount(databaseName);

        var length =
            // Length of message contents in bytes, including self.
            sizeof(int) +
            // The protocol version number.
            sizeof(int) +
            // The "user" parameter name.
            UserNameBinary.Length + sizeof(byte) +
            // The "user" parameter value.
            userValueBinaryLength + sizeof(byte) +
            // The "database" parameter name.
            DatabaseNameBinary.Length + sizeof(byte) +
            // The "database" parameter value.
            databaseValueBinaryLength + sizeof(byte) +
            // The "client_encoding" parameter name.
            ClientEncodingNameBinary.Length + sizeof(byte) +
            // The "client_encoding" parameter value.
            ClientEncodingValueBinary.Length + sizeof(byte);

        if (Db.ApplicationNameBinary.Length > 0)
        {
            // The "application_name" parameter name.
            length += ApplicationNameNameBinary.Length + sizeof(byte);
            // The "application_name" parameter value.
            length += Db.ApplicationNameBinary.Length + sizeof(byte);
        }

        // A zero byte is required as a terminator after the last name/value pair.
        length += sizeof(byte);

        writer.DebugAssertEnoughSpace(length);

        // The length of the user name and database name has been checked.
        writer.WriteInt32(length);
        writer.WriteInt32(Versions.ProtocolVersion);

        // user
        writer.WriteBinary(UserNameBinary);
        writer.WriteStringTerminator();
        writer.WriteString(userName, userValueBinaryLength);
        writer.WriteStringTerminator();

        // database
        writer.WriteBinary(DatabaseNameBinary);
        writer.WriteStringTerminator();
        writer.WriteString(databaseName, databaseValueBinaryLength);
        writer.WriteStringTerminator();

        // client_encoding
        writer.WriteBinary(ClientEncodingNameBinary);
        writer.WriteStringTerminator();
        writer.WriteBinary(ClientEncodingValueBinary);
        writer.WriteStringTerminator();

        if (Db.ApplicationNameBinary.Length > 0)
        {
            // application_name
            writer.WriteBinary(ApplicationNameNameBinary);
            writer.WriteStringTerminator();
            writer.WriteBinary(Db.ApplicationNameBinary);
            writer.WriteStringTerminator();
        }

        // A zero byte is required as a terminator after the last name/value pair.
        writer.WriteStringTerminator();

        return writer.FlushAsync(cancellationToken);
    }
}
