using Microsoft.Owin.Logging;
using System;
using System.Diagnostics;

namespace LoggingSample.Logging
{
    public class ConsoleLogger : ILogger
    {
        private readonly string _loggerName;

        public ConsoleLogger(string loggerName)
        {
            _loggerName = loggerName;
        }

        public bool WriteCore(TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            string baseMessage = string.Format("{0} (1) | {2}, {3}: {4}", _loggerName, eventType, DateTimeOffset.Now, eventId, formatter(state, exception));
            Console.WriteLine(baseMessage);
            return true;
        }
    }
}