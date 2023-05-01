using SingWing.PgSql.Balancing;
using System.Globalization;

namespace SingWing.PgSql;

/// <summary>
/// Represents an exception thrown when a connection establishment fails or user authentication fails.
/// </summary>
public sealed class OpeningFailedException : ApplicationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpeningFailedException"/> class 
    /// with the failed node and failure reason.
    /// </summary>
    /// <param name="node">The failed node.</param>
    /// <param name="reason">The reason for the failure.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    internal OpeningFailedException(Node node, OpeningFailedReason reason, Exception? innerException)
        : base(MessageFor(node, reason), innerException) => (Node, Reason) = (node, reason);

    /// <summary>
    /// Gets the failed node.
    /// </summary>
    public INode Node { get; }

    /// <summary>
    /// Gets the reason for the failure.
    /// </summary>
    public OpeningFailedReason Reason { get; }

    /// <summary>
    /// Generates an error message for the specified node and failure reason.
    /// </summary>
    /// <param name="node">The failed node.</param>
    /// <param name="reason">The reason for the failure.</param>
    /// <returns>The error message.</returns>
    private static string MessageFor(Node node, OpeningFailedReason reason) =>
        reason switch
        {
            OpeningFailedReason.AuthenticationFailed => string.Format(
                CultureInfo.CurrentCulture,
                Strings.AuthenticationFailed,
                node.Database.UserName,
                node.Server.Host.Name,
                node.Server.Host.Port,
                node.Database.DatabaseName),
            _ => string.Format(
                CultureInfo.CurrentCulture,
                Strings.ConnectionFailed,
                node.Server.Host.Name,
                node.Server.Host.Port)
        };
}
