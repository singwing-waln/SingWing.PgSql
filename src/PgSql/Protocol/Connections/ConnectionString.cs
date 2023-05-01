namespace SingWing.PgSql.Protocol.Connections;

/// <summary>
/// Provides helper methods for parsing and creating connection strings.
/// </summary>
internal static class ConnectionString
{
    /// <summary>
    /// Parses a connection string.
    /// </summary>
    /// <param name="connectionString">The connection string to parse.</param>
    /// <param name="hosts">The parsed hosts.</param>
    /// <param name="databaseName">The parsed database name.</param>
    /// <param name="userName">The parsed user name.</param>
    /// <param name="password">The parsed user password.</param>
    internal static void Parse(
        string connectionString,
        out string hosts,
        out string databaseName,
        out string userName,
        out string password)
    {
        const int MaxParameterCount = 4;
        const string HostsParameterName = "hosts";
        const string DatabaseParameterName = "database";
        const string UserParameterName = "user";
        const string PasswordParameterName = "password";

        hosts = string.Empty;
        databaseName = string.Empty;
        userName = string.Empty;
        password = string.Empty;

        if (connectionString.Length == 0)
        {
            return;
        }

        var count = 0;
        var query = connectionString.AsMemory();

        foreach (var pair in query.Split('&'))
        {
            if (pair.Length == 0)
            {
                continue;
            }

            var index = pair.Span.IndexOf('=');
            if (index == 0)
            {
                // No paramter name is provided;
                continue;
            }

            if (index == -1)
            {
                // No parameter value is provided.
                index = pair.Length;
            }

            var name = pair[0..index].Span;
            var value = index < pair.Length ? pair[(index + 1)..] : ReadOnlyMemory<char>.Empty;

            if (name.Equals(HostsParameterName, StringComparison.OrdinalIgnoreCase))
            {
                hosts = value.Length > 0 ? Uri.UnescapeDataString(value.ToString()) : string.Empty;
                count++;
            }
            else if (name.Equals(DatabaseParameterName, StringComparison.OrdinalIgnoreCase))
            {
                databaseName = value.Length > 0 ? Uri.UnescapeDataString(value.ToString()) : string.Empty;
                count++;
            }
            else if (name.Equals(UserParameterName, StringComparison.OrdinalIgnoreCase))
            {
                userName = value.Length > 0 ? Uri.UnescapeDataString(value.ToString()) : string.Empty;
                count++;
            }
            else if (name.Equals(PasswordParameterName, StringComparison.OrdinalIgnoreCase))
            {
                password = value.Length > 0 ? Uri.UnescapeDataString(value.ToString()) : string.Empty;
                count++;
            }

            if (count == MaxParameterCount)
            {
                break;
            }
        }
    }

    /// <summary>
    /// Parses the hosts string into a <see cref="Host"/> list.
    /// </summary>
    /// <param name="hostsString">The hosts string.</param>
    /// <returns>The parsed host list.</returns>
    internal static IList<(Host, double)> ParseHosts(string hostsString)
    {
        if (hostsString.Length == 0)
        {
            return Array.Empty<(Host, double)>();
        }

        var hosts = new List<(Host, double)>(8);
        var hostsMemory = hostsString.AsMemory().Trim();
        var nameMemory = ReadOnlyMemory<char>.Empty;
        var port = Db.DefaultPort;
        var proportion = 1d;

        foreach (var pair in hostsMemory.Split(','))
        {
            nameMemory = ReadOnlyMemory<char>.Empty;
            port = Db.DefaultPort;
            proportion = 1d;

            var colonIndex = pair.Span.IndexOf(':');
            var barIndex = pair.Span.LastIndexOf('|');
            if (colonIndex == 0)
            {
                // No host name is provided.
                continue;
            }

            if (colonIndex == -1)
            {
                // No port number is provided.
                port = Db.DefaultPort;
                if (barIndex == -1)
                {
                    nameMemory = pair;
                }
                else
                {
                    nameMemory = pair[0..barIndex];
                    if (!double.TryParse(pair[(barIndex + 1)..].Span.Trim(), out proportion) ||
                        proportion <= 0d || proportion > 1d)
                    {
                        proportion = 1d;
                    }
                }
            }
            else
            {
                nameMemory = pair[0..colonIndex];

                if (barIndex == -1)
                {
                    if (!int.TryParse(pair[(colonIndex + 1)..].Span.Trim(), out port) ||
                        port < IPEndPoint.MinPort ||
                        port > IPEndPoint.MaxPort)
                    {
                        port = Db.DefaultPort;
                    }
                }
                else
                {
                    if (!int.TryParse(pair[(colonIndex + 1)..barIndex].Span.Trim(), out port) ||
                        port < IPEndPoint.MinPort ||
                        port > IPEndPoint.MaxPort)
                    {
                        port = Db.DefaultPort;
                    }

                    if (!double.TryParse(pair[(barIndex + 1)..].Span.Trim(), out proportion) ||
                        proportion <= 0d || proportion > 1d)
                    {
                        proportion = 1d;
                    }
                }
            }

            nameMemory = nameMemory.Trim();

            if (nameMemory.IsEmpty)
            {
                continue;
            }

            if (!ContainsHost(hosts, nameMemory.Span, port))
            {
                hosts.Add((new Host(nameMemory.ToString(), port), proportion));
            }
        }

        // Multiple connection strings, if only their host orders are different,
        // they should be treated as the same connection string.
        // Sort the host list to avoid the side effects of different orders.
        hosts.Sort((a, b) => string.Compare(a.Item1.Name, b.Item1.Name, StringComparison.OrdinalIgnoreCase));

        return hosts;

        static bool ContainsHost(IList<(Host, double)> hosts, in ReadOnlySpan<char> name, int port)
        {
            foreach (var host in hosts)
            {
                if (name.Equals(host.Item1.Name, StringComparison.OrdinalIgnoreCase) &&
                    host.Item1.Port == port)
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Splits a string into ReadOnlyMemory&lt;char&gt;s.
    /// </summary>
    /// <param name="value">The string to split.</param>
    /// <param name="separator">A character that delimits the substrings in <paramref name="value"/>.</param>
    /// <returns>
    /// A <see cref="StringMemoryTokenizer"/> whose elements contain the ReadOnlyMemory&lt;char&gt;s from 
    /// <paramref name="value"/> that are delimited by <paramref name="separator"/>.</returns>
    private static IEnumerable<ReadOnlyMemory<char>> Split(
        this in ReadOnlyMemory<char> value,
        char separator) => new StringMemoryTokenizer(value, separator);

    /// <summary>
    /// Tokenizes a ReadOnlyMemory&lt;char&gt; into ReadOnlyMemory&lt;char&gt;s.
    /// </summary>
    private struct StringMemoryTokenizer : IEnumerable<ReadOnlyMemory<char>>, IEnumerator<ReadOnlyMemory<char>>
    {
        /// <summary>
        /// The string to split.
        /// </summary>
        private readonly ReadOnlyMemory<char> _value;

        /// <summary>
        /// A character that delimits the substrings.
        /// </summary>
        private readonly char _separator;

        /// <summary>
        /// The starting position of the next substring.
        /// </summary>
        private int _index;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringMemoryTokenizer"/> struct
        /// with the specified string and separator.
        /// </summary>
        /// <param name="value">The string to split.</param>
        /// <param name="separator">A character that delimits the substrings in <paramref name="value"/>.</param>
        internal StringMemoryTokenizer(in ReadOnlyMemory<char> value, char separator)
        {
            _value = value;
            _separator = separator;
            _index = 0;
            Current = default;
        }

        #region IEnumerable

        IEnumerator<ReadOnlyMemory<char>> IEnumerable<ReadOnlyMemory<char>>.GetEnumerator() => this;

        IEnumerator IEnumerable.GetEnumerator() => this;

        #endregion

        #region IEnumerator

        public ReadOnlyMemory<char> Current { get; private set; }

        object IEnumerator.Current => Current;

        void IDisposable.Dispose()
        {
        }

        bool IEnumerator.MoveNext()
        {
            if (_value.IsEmpty || _index > _value.Length)
            {
                Current = default;
                return false;
            }

            var next = _value[_index..].Span.IndexOf(_separator);
            if (next == -1)
            {
                // No separator found. Consume the remainder of the string.
                next = _value.Length;
            }
            else
            {
                next += _index;
            }

            Current = _value[_index..next];
            _index = next + 1;

            return true;
        }

        void IEnumerator.Reset()
        {
            _index = 0;
            Current = default;
        }

        #endregion
    }
}
