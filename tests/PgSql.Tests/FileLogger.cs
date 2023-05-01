using System.Runtime.Loader;

namespace SingWing.PgSql.Tests;

public sealed class FileLogger : ILogger, IDisposable
{
    public const string FileName = "tests.log";

    public static readonly FileLogger Shared;

    static FileLogger()
    {
        Shared = new FileLogger();
        Db.Logger = Shared;

        AssemblyLoadContext.Default.Unloading += Application_Unloading;

        static void Application_Unloading(AssemblyLoadContext context)
        {
            Shared.Dispose();
        }
    }

    private readonly StreamWriter _writer;

    private FileLogger() => _writer = new(FileName, append: false);

    public void Dispose()
    {
        _writer.Flush();
        _writer.Dispose();
    }

    public void LogDebug(string message, Exception? exception = null) =>
        Log("Debug", message, exception);

    public void LogError(string message, Exception? exception = null) =>
        Log("Error", message, exception);

    public void LogFatal(string message, Exception? exception = null) =>
        Log("Fatal", message, exception);

    public void LogInformation(string message, Exception? exception = null) =>
        Log("Information", message, exception);

    public void LogTrace(string message, Exception? exception = null) =>
        Log("Trace", message, exception);

    public void LogWarning(string message, Exception? exception = null) =>
        Log("Warning", message, exception);

    private void Log(string type, string message, Exception? exception)
    {
        if (string.IsNullOrEmpty(message) && exception is null)
        {
            return;
        }

        lock (_writer)
        {
            _writer.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff} - {1}", DateTime.Now, type));

            if (exception is null)
            {
                _writer.WriteLine(message);
            }
            else
            {
                if (!string.IsNullOrEmpty(message))
                {
                    _writer.WriteLine(message);
                }

                _writer.WriteLine(exception.ToString());
            }

            _writer.WriteLine();
        }
    }
}
