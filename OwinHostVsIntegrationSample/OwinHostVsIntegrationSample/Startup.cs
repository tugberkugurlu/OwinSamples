using Microsoft.Owin;
using Owin;
using OwinHostVsIntegrationSample;
using System.IO;

[assembly: OwinStartup(typeof(Startup))]
namespace OwinHostVsIntegrationSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app) 
        {
            app.Use(async (ctx, next) => 
            {
                TextWriter output = ctx.Get<TextWriter>("host.TraceOutput");
                output.WriteLine("{0} {1}: {2}", 
                    ctx.Request.Scheme, ctx.Request.Method, ctx.Request.Path);

                await ctx.Response.WriteAsync("Hello world!");
            });
        }
    }
}