namespace new_diary
{
    public class FileLogger : ILogger, IDisposable
    {
        string filePath;
        public object _lock = new object();
        public FileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose() { }

        public bool IsEnabled(LogLevel level)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
                TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
            }
        }

        }

    public class FileLoggerProvider : ILoggerProvider
    {
        string path;
        public FileLoggerProvider(string path)
        {
            this.path = path;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(path);
        }

        public void Dispose() { }
    }

    public static class FileLoggerExtension
    {
        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path)
        {
            builder.AddProvider(new FileLoggerProvider(path));
            return builder;
        }
    }
}
