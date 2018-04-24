using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Host
{
    public class Log4NetFactory : ILoggerFactory
    {
        private log4net.Repository.ILoggerRepository repository;
        public Log4NetFactory(log4net.Repository.ILoggerRepository _repository)
        {
            repository = _repository;
        }
        public void AddProvider(ILoggerProvider provider)
        {
            //throw new NotImplementedException();
            
        }

        public ILogger CreateLogger(string categoryName)
        {
            var loger = log4net.LogManager.GetLogger(repository.Name, categoryName);
            return new Log4NetLoger(loger);
        }

        public void Dispose()
        {
            repository.Shutdown();
        }
    }
    public class Log4NetLoger : ILogger, IDisposable
    {
        private log4net.ILog log;
        public Log4NetLoger(log4net.ILog _log)
        {
            log = _log;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {               
                case LogLevel.Debug:
                    return log.IsDebugEnabled;
                case LogLevel.Trace:
                case LogLevel.Information:
                    return log.IsInfoEnabled;
                case LogLevel.Warning:
                    return log.IsWarnEnabled;
                case LogLevel.Error:
                    return log.IsErrorEnabled;
                case LogLevel.Critical:
                    return log.IsFatalEnabled;
            }
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var msg = formatter.Invoke(state, exception);
            switch (logLevel)
            {
                case LogLevel.Trace:
                    log.Info(msg);
                    break;
                case LogLevel.Debug:
                    log.Debug(msg);
                    break;
                case LogLevel.Information:
                    log.Info(msg);
                    break;
                case LogLevel.Warning:
                    log.Warn(msg);
                    break;
                case LogLevel.Error:
                    log.Error(msg,exception);
                    break;
            }
        }
        private log4net.Core.Level Convert(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return log4net.Core.Level.Trace;
                case LogLevel.Debug:
                    return log4net.Core.Level.Debug;
                case LogLevel.Information:
                    return log4net.Core.Level.Info;
                case LogLevel.Warning:
                    return log4net.Core.Level.Warn;
                case LogLevel.Error:
                    return log4net.Core.Level.Error;
                case LogLevel.Critical:
                    return log4net.Core.Level.Critical;
                case LogLevel.None:
                    return log4net.Core.Level.Off;
            }
            return log4net.Core.Level.Off;
        }
    }
}
