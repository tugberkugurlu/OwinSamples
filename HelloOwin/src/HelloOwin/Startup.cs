using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace HelloOwin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var logger = app.CreateLogger(GetType());

            app.SetDataProtectionProvider(new SharedSecretDataProtectionProvider(
                "af98j3pf98ja3fdopa32hr !!!! DO NOT USE THIS STRING IN YOUR APP !!!!",
                "AES",
                "HMACSHA256"));

            // example of a filter - writeline each request
            app.UseFilter(req => logger.WriteInformation(string.Format(
                "{0} {1}{2} {3}",
                req.Method,
                req.PathBase,
                req.Path,
                req.QueryString)));

            // example of a handler - all paths reply Hello, Owin!
            app.UseHandler(async (req, res) =>
            {
                res.ContentType = "text/plain";
                await res.WriteAsync("Hello, OWIN!");
            });
        }
    }
}