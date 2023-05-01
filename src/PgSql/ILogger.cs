namespace SingWing.PgSql;

/// <summary>
/// Provides methods for logging messages during database accesses.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// An <see cref="ILogger"/> that does nothing.
    /// This is the default value for <see cref="Db.Logger"/>.
    /// </summary>
    public static readonly ILogger Empty = EmptyLogger.Shared;

    /// <summary>
    /// Writes an informational log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception to log.</param>
    void LogInformation(string message, Exception? exception = null);

    /// <summary>
    /// Writes a debug log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception to log.</param>
    void LogDebug(string message, Exception? exception = null);

    /// <summary>
    /// Writes a trace log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception to log.</param>
    void LogTrace(string message, Exception? exception = null);

    /// <summary>
    /// Writes a warning log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception to log.</param>
    void LogWarning(string message, Exception? exception = null);

    /// <summary>
    /// Writes an error log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception to log.</param>
    void LogError(string message, Exception? exception = null);

    /// <summary>
    /// Writes a fatal log message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="exception">The exception to log.</param>
    void LogFatal(string message, Exception? exception = null);

    #region EmptyLogger

    /// <summary>
    /// An <see cref="ILogger"/> implementation that does nothing.
    /// </summary>
    private sealed class EmptyLogger : ILogger
    {
        /// <summary>
        /// A shared singleton instance of the <see cref="EmptyLogger"/> class.
        /// </summary>
        internal static readonly EmptyLogger Shared = new();

        private EmptyLogger() { }

        void ILogger.LogDebug(string message, Exception? exception)
        {
        }

        void ILogger.LogError(string message, Exception? exception)
        {
        }

        void ILogger.LogFatal(string message, Exception? exception)
        {
        }

        void ILogger.LogInformation(string message, Exception? exception)
        {
        }

        void ILogger.LogTrace(string message, Exception? exception)
        {
        }

        void ILogger.LogWarning(string message, Exception? exception)
        {
        }
    }

    #endregion
}
