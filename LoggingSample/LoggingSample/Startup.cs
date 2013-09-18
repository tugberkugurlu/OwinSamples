using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LoggingSample
{
    using LoggingSample.Logging;
    using TraceFactoryDelegate = Func<string, Func<TraceEventType, int, object, Exception, Func<object, Exception, string>, bool>>;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // app.SetLoggerFactory(new ConsoleLoggerFactory());

            Log1(app);
            Log2(app);

            app.Use<MyCustomMiddleware>(app);
        }

        private void ReplaceTheDefaultLogger(IAppBuilder app)
        {
            // NOTE: This still sees Microsof.Owin as your parent switch. 
            // So, Microsoft.Owin listeners on switches are still enabled.

            const string LoggerFactoryAppKey = "server.LoggerFactory";
            SourceSwitch sSwitch = new SourceSwitch("Tugberk.Owin");
            ConsoleTraceListener cListener = new ConsoleTraceListener();
            ILoggerFactory loggerFactory = new DiagnosticsLoggerFactory(sSwitch, cListener);

            // NOTE: One of the following ways will set the logger factory
            app.Properties[LoggerFactoryAppKey] = new TraceFactoryDelegate(name => loggerFactory.Create(name).WriteCore);
            // app.SetLoggerFactory(loggerFactory);
        }

        private void Log1(IAppBuilder app) 
        {
            ILogger logger = app.CreateLogger<Startup>();
            logger.WriteError("App is starting up");
            logger.WriteCritical("App is starting up");
            logger.WriteWarning("App is starting up");
            logger.WriteVerbose("App is starting up");
            logger.WriteInformation("App is starting up");

            int foo = 1;
            int bar = 0;

            try
            {
                int fb = foo / bar;
            }
            catch (Exception ex)
            {
                logger.WriteError("Error on calculation", ex);
            }
        }

        private void Log2(IAppBuilder app) 
        {
            ILogger logger = app.CreateLogger("MyCustomComponent");
            logger.WriteError("App is starting up");
            logger.WriteCritical("App is starting up");
            logger.WriteWarning("App is starting up");
            logger.WriteVerbose("App is starting up");
            logger.WriteInformation("App is starting up");

            int foo = 1;
            int bar = 0;

            try
            {
                int fb = foo / bar;
            }
            catch (Exception ex)
            {
                logger.WriteError("Error on calculation", ex);
            }
        }
    }

    public class MyCustomMiddleware : OwinMiddleware
    {
        private readonly ILogger _logger;

        public MyCustomMiddleware(OwinMiddleware next, IAppBuilder app) : base(next)
        {
            _logger = app.CreateLogger<MyCustomMiddleware>();
        }

        public override Task Invoke(IOwinContext context)
        {
            _logger.WriteVerbose(string.Format("{0} {1}: {2}", 
                context.Request.Scheme, context.Request.Method, context.Request.Path));

            context.Response.Headers.Add("Content-Type", new[] { "text/plain" });
            return context.Response.WriteAsync("Logging sample is runnig!");
        }
    }
}