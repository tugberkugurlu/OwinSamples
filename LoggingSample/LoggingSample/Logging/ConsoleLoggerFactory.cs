using Microsoft.Owin.Logging;

namespace LoggingSample.Logging
{
    public class ConsoleLoggerFactory : ILoggerFactory
    {
        public ILogger Create(string name)
        {
            return new ConsoleLogger(name);
        }
    }
}