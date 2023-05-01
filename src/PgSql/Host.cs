using System.Globalization;

namespace SingWing.PgSql;

/// <summary>
/// Provides the host name (or IP address in string) and port number of a PostgreSQL host.
/// </summary>
public readonly struct Host : IEquatable<Host>
{
    /// <summary>
    /// The default host name.
    /// </summary>
    private const string DefaultName = "localhost";

    /// <summary>
    /// The host name or IP address of the PostgreSQL service.
    /// </summary>
    private readonly string _name = DefaultName;

    /// <summary>
    /// The port number of the PostgreSQL service.
    /// </summary>
    private readonly int _port = Db.DefaultPort;

    /// <summary>
    /// Initializes a new instance of the <see cref="Host"/> struct with the specified host name and port number.
    /// </summary>
    /// <param name="name">
    /// The host name or IP address of the PostgreSQL service.
    /// If this name is empty, "localhost" is used.
    /// </param>
    /// <param name="port">
    /// The port number of the PostgreSQL service.
    /// If this port number is invalid or zero, the default port number is used.
    /// </param>
    public Host(string name, int port = Db.DefaultPort)
    {
        var nameSpan = name.AsSpan().Trim();

        if (nameSpan.IsEmpty)
        {
            _name = string.Empty;
        }
        else
        {
            _name = nameSpan.Equals(DefaultName, StringComparison.InvariantCultureIgnoreCase)
                ? DefaultName : (nameSpan.Length == name.Length ? name : nameSpan.ToString());
        }

        _port = port <= IPEndPoint.MinPort || port > IPEndPoint.MaxPort ? Db.DefaultPort : port;
    }

    /// <summary>
    /// Gets the host name or IP address of the PostgreSQL service.
    /// If it starts with '/', it represents a Unix Domain Socket endpoint.
    /// </summary>
    /// <value>The default value is "localhost".</value>
    public string Name => string.IsNullOrEmpty(_name) ? DefaultName : _name;

    /// <summary>
    /// Gets the port number of the PostgreSQL service.
    /// </summary>
    /// <value>The default value is <see cref="Db.DefaultPort"/>.</value>
    public int Port => 
        _port <= IPEndPoint.MinPort || _port > IPEndPoint.MaxPort ? Db.DefaultPort : _port;

    /// <summary>
    /// Gets a value that indicates whether the host is a Unix Domain Socket endpoint.
    /// </summary>
    internal bool IsUnixEndPoint => Name.Length > 0 && Name[0] == '/';

    /// <summary>
    /// Returns the string form of this <see cref="Host"/> information.
    /// </summary>
    /// <returns>A string in the format "{Name}:{Port}".</returns>
    public override string ToString() => $"{Name}:{Port}";

    /// <summary>
    /// Resolves this host information into a Unix Domain Socket endpoint.
    /// </summary>
    /// <returns>The resolved Unix Domain Socket endpoint.</returns>
    internal UnixDomainSocketEndPoint ResolveUnixEndPoint()
    {
        var path = Path.Combine(Name, $".s.PGSQL.{Port}");
        return new UnixDomainSocketEndPoint(path);
    }

    /// <summary>
    /// Resolves this host information into a collection of IP endpoints.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the asynchronous operation.</param>
    /// <returns>The resolved collection of IP endpoints.</returns>
    internal async ValueTask<IEnumerable<IPEndPoint>> ResolveIPEndPointsAsync(
        CancellationToken cancellationToken)
    {
        if (Name.Length == 0 || Name[0] == '/')
        {
            return Array.Empty<IPEndPoint>();
        }

        IPAddress[] addresses;

        try
        {
            addresses = await Dns.GetHostAddressesAsync(Name, cancellationToken);
        }
        catch (Exception exc)
        {
            Db.Logger.LogError(
                string.Format(CultureInfo.CurrentCulture, Strings.FailedToGetIPAddresses, Name),
                exc);
            return Array.Empty<IPEndPoint>();
        }

        if (addresses.Length == 0)
        {
            return Array.Empty<IPEndPoint>();
        }

        var points = new List<IPEndPoint>(addresses.Length);
        foreach (var address in addresses)
        {
            if (address.AddressFamily is
                not AddressFamily.InterNetwork and
                not AddressFamily.InterNetworkV6)
            {
                continue;
            }

            points.Add(new IPEndPoint(address, Port));
        }

        return points.Count == 0 ? Array.Empty<IPEndPoint>() : points;
    }

    /// <inheritdoc/>
    public bool Equals(Host other) => Port == other.Port && Name == other.Name;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Host host && Equals(host);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Name, Port);

    /// <summary>
    /// Compares whether two <see cref="Host"/>s represent the same PostgreSQL host.
    /// </summary>
    /// <param name="left">The first host to compare.</param>
    /// <param name="right">The second host to compare.</param>
    /// <returns><see langword="true"/> if the two hosts represent the same PostgreSQL host, otherwise <see langword="false"/>.</returns>
    public static bool operator ==(Host left, Host right)
    {
        return left.Port == right.Port && left.Name == right.Name;
    }

    /// <summary>
    /// Compares whether two <see cref="Host"/>s represent the different PostgreSQL host.
    /// </summary>
    /// <param name="left">The first host to compare.</param>
    /// <param name="right">The second host to compare.</param>
    /// <returns><see langword="true"/> if the two hosts represent the different PostgreSQL host, otherwise <see langword="false"/>.</returns>
    public static bool operator !=(Host left, Host right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Parses a string in the format "name:port" as a Host. The port in the string can be omitted.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <remarks>If the port number is invalid or emitted, the default port number is used.</remarks>
    public static implicit operator Host(string s)
    {
        var span = s.AsSpan().Trim();
        if (span.IsEmpty)
        {
            return default;
        }

        var index = span.IndexOf(':');
        if (index == -1)
        {
            return new Host(span.Length == s.Length ? s : span.ToString());
        }

        if (int.TryParse(span[(index + 1)..], out var port) &&
            port >= IPEndPoint.MinPort &&
            port <= IPEndPoint.MaxPort)
        {
            return new Host(span[..index].ToString(), port);
        }

        return new Host(span[..index].ToString());
    }
}
