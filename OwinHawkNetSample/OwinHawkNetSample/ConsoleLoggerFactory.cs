using Microsoft.Owin.Logging;
using System;

namespace OwinHawkNetSample
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public ILogger Create(string name)
        {
            return new ConsoleLogger();
        }
    }

    public class ConsoleLogger : ILogger
    {
        public bool WriteCore(System.Diagnostics.TraceEventType eventType, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            string message = null;
            if (state != null)
            {
                message += state.ToString();
            }

            if (exception != null)
            {
                message += ". Exception: " + exception.ToString();
            }

            Console.WriteLine("Event {0}. Message {1}", eventType, message);

            return true;
        }
    }
}
