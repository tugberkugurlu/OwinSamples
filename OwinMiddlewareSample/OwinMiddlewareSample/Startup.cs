using Owin;
using Owin.Types;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppFunc = System.Func
                <
                    System.Collections.Generic.IDictionary<string, object>,
                    System.Threading.Tasks.Task
                >;

namespace OwinMiddlewareSample {
    
    public partial class Startup {

        public void Configuration(IAppBuilder app) {

            app.UseType<MyMiddleware>();
        }

        public class MyMiddleware {

            // This will called once when the app starts up.
            // This takes a parameter for the next pointer.
            // These are put together kind of like a link list

            private readonly AppFunc _nextFunc;

            public MyMiddleware(AppFunc nextFunc) {
                _nextFunc = nextFunc;
            }

            public async Task Invoke(IDictionary<string, object> env) {

                OwinRequest request = new OwinRequest(env);
                if (request.Path == "/raw") {

                    OwinResponse response = new OwinResponse(request);
                    response.ContentType = "text/plain";
                    await response.WriteAsync("Hello from OWIN!");
                }
                else {

                    await _nextFunc(env);
                }
            }
        }
    }
}