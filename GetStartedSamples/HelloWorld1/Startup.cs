using HelloWorld1;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFunc = System.Func
                <
                    System.Collections.Generic.IDictionary<string, object>,
                    System.Threading.Tasks.Task
                >;

[assembly: OwinStartup(typeof(Startup))]
namespace HelloWorld1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(new Func<AppFunc, AppFunc>(nextApp => new TimerMiddleware(nextApp).Invoke));
            app.Use(new Func<AppFunc, AppFunc>(nextApp => new MyMiddleware(nextApp).Invoke));
        }
    }

    public class TimerMiddleware
    {
        private readonly AppFunc _nextFunc;

        public TimerMiddleware(AppFunc nextFunc)
        {
            _nextFunc = nextFunc;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            Console.WriteLine("Request: " + env["owin.RequestPath"].ToString());
            await _nextFunc(env);
            Console.WriteLine("Response: " + env["owin.ResponseStatusCode"].ToString());
        }
    }

    public class MyMiddleware
    {
        private readonly AppFunc _nextFunc;

        public MyMiddleware(AppFunc nextFunc)
        {
            _nextFunc = nextFunc;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            string path = env["owin.RequestPath"] as string;
            if (path == "/raw")
            {
                Stream responseStream = (Stream)env["owin.ResponseBody"];
                IDictionary<string, string[]> headers = (IDictionary<string, string[]>)env["owin.ResponseHeaders"];

                string bodyText = string.Format("Serviced request on {0} at {1}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                byte[] responseBytes = ASCIIEncoding.UTF8.GetBytes(bodyText);

                headers["Content-Length"] = new string[] { responseBytes.Length.ToString(CultureInfo.InvariantCulture) };
                headers["Content-Type"] = new[] { "text/plain" };

                await responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                await _nextFunc(env);
            }
        }
    }
}