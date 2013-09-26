using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Diagnostics;

[assembly: OwinStartup(typeof(OwinDiagnosticsSample.Startup))]
namespace OwinDiagnosticsSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // app.UseWelcomePage();
            app.UseErrorPage();
            app.Use((ctx, next) => 
            {
                int left = 1,
                    right = 0,
                    total = left / right;

                return next.Invoke();
            });
        }
    }
}