using Microsoft.Extensions.Logging;

public class LoggerTest : ILogger
{
    private readonly string _name;
    private readonly LogLevel _minLogLevel;

    public LoggerTest(string name, LogLevel minLogLevel)
    {
        _name = name;
        _minLogLevel = minLogLevel;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLogLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;
        TestContext.Progress.WriteLine($"[{logLevel}] {_name}: {formatter(state, exception)}");
        if (exception != null)
        {
            TestContext.Progress.WriteLine(exception.ToString());
        }
    }
}

public class NUnitLoggerProvider : ILoggerProvider
{
    private readonly LogLevel _minLogLevel;

    public NUnitLoggerProvider(LogLevel minLogLevel)
    {
        _minLogLevel = minLogLevel;
    }

    public ILogger CreateLogger(string categoryName) => new LoggerTest(categoryName, _minLogLevel);

    public void Dispose() { }
}
