using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NowinSample
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(new Func<AppFunc, AppFunc>(ignoreNext => Invoke));
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            // retrieve the Request Data from the environment
            string path = env["owin.RequestPath"] as string;

            if (path.Equals("/", StringComparison.OrdinalIgnoreCase))
            {
                // Prepare the message
                const string Message = "Hello World!";
                byte[] bytes = Encoding.UTF8.GetBytes(Message);

                // retrieve the Response Data from the environment
                Stream responseBody = env["owin.ResponseBody"] as Stream;
                IDictionary<string, string[]> responseHeaders = env["owin.ResponseHeaders"] as IDictionary<string, string[]>;

                // write the headers, response body
                responseHeaders["Content-Type"] = new[] { "text/plain" };
                await responseBody.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}