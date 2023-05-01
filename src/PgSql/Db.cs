using SingWing.PgSql.Balancing;
using SingWing.PgSql.Protocol.Connections;
using System.Globalization;

namespace SingWing.PgSql;

/// <summary>
/// Provides the entry point for accessing PostgreSQL databases.
/// </summary>
public static class Db
{
    /// <summary>
    /// The default port number of PostgreSQL.
    /// </summary>
    public const int DefaultPort = 5432;

    #region Get

    /// <summary>
    /// Gets the database for the specified hosts, database name, user name and password.
    /// </summary>
    /// <param name="hosts">The name (IP address) and port numbers of one or more hosts.</param>
    /// <param name="database">The name of the database to connect to.</param>
    /// <param name="user">The user name to be used when connecting to PostgreSQL.</param>
    /// <param name="password">The password for the PostgreSQL user.</param>
    /// <param name="id">The unique database ID provided by the caller.</param>
    /// <returns>
    /// An <see cref="IDatabase"/> instance that can be used to execute commands or start transactions.
    /// </returns>
    /// <exception cref="ArgumentException">One or more parameters are invalid.</exception>
    /// 
    /// <example>
    /// <code>
    /// var db = Db.Get("dbserver:5432", "dbname", "dbuser", "dbpassword");
    /// 
    /// var rows = await db.QueryAsync("SELECT ...");
    /// await foreach (var row in rows)
    /// {
    ///     await foreach (var col in row)
    ///     {
    ///         ...
    ///     }
    /// }
    /// </code>
    /// </example>
    /// 
    /// <remarks>
    /// <para>
    /// This method does not establish connections to PostgreSQL servers immediately. 
    /// Connections are established the first time a command is executed or a transaction is started.
    /// </para>
    /// <list type="table">
    /// 
    /// <item>
    /// <term>hosts</term>
    /// <description>
    /// Required. 
    /// A string of one or more PostreSQL hosts in the format: "host1:port1|proportion1,host2:port2|proportion2,...".
    /// Each host contains the host name (or IP address) and port number separated by ':'.
    /// The host name can be a Unix Domain Socket path, such as "/var/run/postgresql".
    /// If no port number is provided, the default port number of 5432 is used.
    /// Optionally, each host can be suffixed with a number greater than 0 and less than or equal to 1, separated by '|'. 
    /// This number specifies the proportion of the maximum number of connections that can be established by the current client to the host. 
    /// If the number is not provided, it defaults to 1.
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>database</term>
    /// <description>
    /// Required. 
    /// The name of the database to connect to. The maximum length is 63.
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>user</term>
    /// <description>
    /// Required. 
    /// The user name to be used when connecting to PostgreSQL. The maximum length is 63.
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>password</term>
    /// <description>
    /// Optional. 
    /// The password for the PostgreSQL user. The maximum length is 63.
    /// If no password is provided, the empty password will be used.
    /// </description>
    /// </item>
    /// 
    /// </list>
    /// <para>
    /// If the <paramref name="id"/> is omitted, an id is calculated as the ID of the database 
    /// based on the hash codes of the database name and user name.
    /// By default, databases with the same name and user name return the same <see cref="IDatabase"/> instance unless the caller provides its own <paramref name="id"/>.
    /// </para>
    /// </remarks>
    public static IDatabase Get(string hosts, string database, string user, string password, Guid? id = null)
    {
        var (message, parameterName) = CheckParameters(hosts, database, user, password);

        if (message.Length > 0)
        {
            // The connection string is invalid or contains invalid parameter(s).
            throw new ArgumentException(message, parameterName);
        }

        var hostList = ConnectionString.ParseHosts(hosts);
        if (hostList.Count == 0)
        {
            throw new ArgumentException(Strings.InvalidHosts, nameof(hosts));
        }

        return Manager.Shared.EnsureDatabase(hostList, database, user, password, id);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static (string, string) CheckParameters(string hosts, string database, string user, string password)
        {
            const int MaxDatabaseNameLength = 63;
            const int MaxUserNameLength = 63;
            const int MaxPasswordLength = 63;

            if (hosts.Length == 0)
            {
                return (Strings.InvalidHosts, nameof(hosts));
            }

            if (database.Length == 0)
            {
                return (Strings.DatabaseNameIsEmpty, nameof(database));
            }

            if (database.Length > MaxDatabaseNameLength)
            {
                return (string.Format(
                    CultureInfo.CurrentCulture, Strings.DatabaseNameExceedsLimit, MaxDatabaseNameLength),
                    nameof(database));
            }

            if (user.Length == 0)
            {
                return (Strings.UserNameIsEmpty, nameof(user));
            }

            if (user.Length > MaxUserNameLength)
            {
                return (string.Format(
                    CultureInfo.CurrentCulture, Strings.UserNameExceedsLimit, MaxUserNameLength),
                    nameof(user));
            }

            if (password.Length > MaxPasswordLength)
            {
                return (string.Format(
                    CultureInfo.CurrentCulture, Strings.PasswordExceedsLimit, MaxPasswordLength),
                nameof(password));
            }

            return (string.Empty, string.Empty);
        }
    }

    /// <summary>
    /// Gets the database for the specified connection string.
    /// </summary>
    /// <param name="connectionString">The connection string used to open the PostgreSQL database.</param>
    /// <param name="id">The unique database ID provided by the caller.</param>
    /// <returns>
    /// An <see cref="IDatabase"/> that can be used to execute commands or start transactions.
    /// </returns>
    /// 
    /// <exception cref="ArgumentException">
    /// <paramref name="connectionString"/> is not a valid connection string or contains invalid parameter(s).
    /// </exception>
    /// 
    /// <example>
    /// <code>
    /// var db = Db.Get("dbserver:5432", "dbname", "dbuser", "dbpassword");
    /// 
    /// var rows = await db.QueryAsync("SELECT ...");
    /// await foreach (var row in rows)
    /// {
    ///     await foreach (var col in row)
    ///     {
    ///         ...
    ///     }
    /// }
    /// </code>
    /// </example>
    /// 
    /// <remarks>
    /// <para>
    /// This method does not establish connections to PostgreSQL servers immediately. 
    /// Connections are established the first time a command is executed or a transaction is started.
    /// </para>
    /// <list type="table">
    /// 
    /// <item>
    /// <term>hosts</term>
    /// <description>
    /// Required. 
    /// A string of one or more PostreSQL hosts in the format: "host1:port1|proportion1,host2:port2|proportion2,...".
    /// Each host contains the host name (or IP address) and port number separated by ':'.
    /// The host name can be a Unix Domain Socket path, such as "/var/run/postgresql".
    /// If no port number is provided, the default port number of 5432 is used.
    /// Optionally, each host can be suffixed with a number greater than 0 and less than or equal to 1, separated by '|'. 
    /// This number specifies the proportion of the maximum number of connections that can be established by the current client to the host. 
    /// If the number is not provided, it defaults to 1.
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>database</term>
    /// <description>
    /// Required. 
    /// The name of the database to connect to. The maximum length is 63.
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>user</term>
    /// <description>
    /// Required. 
    /// The user name to be used when connecting to PostgreSQL. The maximum length is 63.
    /// </description>
    /// </item>
    /// 
    /// <item>
    /// <term>password</term>
    /// <description>
    /// Optional. 
    /// The password for the PostgreSQL user. The maximum length is 63.
    /// If no password is provided, the empty password will be used.
    /// </description>
    /// </item>
    /// 
    /// </list>
    /// <para>
    /// If the <paramref name="id"/> is omitted, an id is calculated as the ID of the database 
    /// based on the hash codes of the database name and user name.
    /// By default, databases with the same name and user name return the same <see cref="IDatabase"/> instance unless the caller provides its own <paramref name="id"/>.
    /// </para>
    /// </remarks>
    public static IDatabase Get(string connectionString, Guid? id = null)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException(Strings.ConnectionStringIsEmpty);
        }

        ConnectionString.Parse(connectionString,
            out var hosts,
            out var databaseName,
            out var userName,
            out var password);

        return Get(hosts, databaseName, userName, password, id);
    }

    #endregion

    #region Find

    /// <summary>
    /// Finds the database for the specified ID.
    /// </summary>
    /// <param name="id">The unique database ID.</param>
    /// <returns>The found <see cref="IDatabase"/> instance, <see langword="null"/> if not found.</returns>
    public static IDatabase? Find(Guid id) => Manager.Shared.FindDatabase(id);

    #endregion

    #region ApplicationName

    /// <summary>
    /// The name of the current client application.
    /// </summary>
    private static string _applicationName = string.Empty;

    /// <summary>
    /// Gets the binary data of the application name.
    /// </summary>
    internal static byte[] ApplicationNameBinary { get; private set; } = Array.Empty<byte>();

    /// <summary>
    /// Gets or sets the name of the current client application.
    /// </summary>
    /// <value>
    /// The default value is empty string.
    /// If the name length exceeds 63, it is truncated. 
    /// It should contain only ASCII printable characters, 
    /// other characters will be replaced with '?'.
    /// </value>
    public static string ApplicationName
    {
        get => _applicationName;
        set
        {
            const int MaxApplicationNameLength = 63;

            if (value.Length > 0)
            {
                var length = value.Length > MaxApplicationNameLength
                    ? MaxApplicationNameLength
                    : value.Length;

                Span<char> chars = stackalloc char[length];

                for (var i = 0; i < length; i++)
                {
                    chars[i] = value[i] is >= '!' and <= '~' ? value[i] : '?';
                }

                _applicationName = chars.ToString();
                ApplicationNameBinary = Encoding.UTF8.GetBytes(_applicationName);
            }
            else
            {
                _applicationName = string.Empty;
                ApplicationNameBinary = Array.Empty<byte>();
            }
        }
    }

    #endregion

    #region Logging

    /// <summary>
    /// Gets or sets the global logger for accessing PostgreSQL.
    /// </summary>
    /// <value>The default logger is the <see cref="ILogger.Empty"/> which does nothing.</value>
    /// <remarks>
    /// <para>
    /// This logger is used internally by PgSql to log NoticeResponses, ErrorResponses and other messages, 
    /// such as when the host IP addresses cannot be resolved.
    /// </para>
    /// <para>
    /// In some cases, ErrorResponses may be logged instead of raising <see cref="ServerException"/>s. 
    /// For example, when we receive an ErrorResponse while waiting for the ReadyForQuery message.
    /// </para>
    /// <para>
    /// For more information about the PostgreSQL message formats, see:
    /// <see href="https://www.postgresql.org/docs/current/protocol-message-formats.html"/>.
    /// </para>
    /// </remarks>
    public static ILogger Logger { get; set; } = ILogger.Empty;

    #endregion

    #region Misc

    /// <summary>
    /// Represents the largest possible value of TimeOnly in PostgreSQL.
    /// </summary>
    /// <remarks>
    /// Due to the difference in precision, the value retrieved from PostgreSQL for TimeOnly.MaxValue (23:59:59.9999999) is: 23:59:59.999999.
    /// </remarks>
    public static readonly TimeOnly MaxTime = new(23, 59, 59, 999, 999);

    #endregion
}
