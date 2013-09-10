using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Logging;
using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Owin;
using Owin.Logging.Adapters.WebApi.Sample;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Startup))]
namespace Owin.Logging.Adapters.WebApi.Sample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ILogger webApiLogger = app.CreateLogger("System.Web.Http");
            OwinWebApiTracer tracer = new OwinWebApiTracer(webApiLogger);
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultHttpRoute", "api/{controller}");
            config.Services.Replace(typeof(ITraceWriter), tracer);

            app.Use<MyMiddleware>(app)
               .UseWebApi(config);
        }
    }

    public class MyMiddleware : OwinMiddleware
    {
        private readonly ILogger _logger;

        public MyMiddleware(OwinMiddleware next, IAppBuilder app) : base(next)
        {
            _logger = app.CreateLogger<MyMiddleware>();
        }

        public override async Task Invoke(IOwinContext context)
        {
            _logger.WriteVerbose(string.Format("{0} {1}: {2}", context.Request.Scheme, context.Request.Method, context.Request.Path));
            await Next.Invoke(context);
            _logger.WriteCritical("request is going out. so critical!");
        }
    }
}