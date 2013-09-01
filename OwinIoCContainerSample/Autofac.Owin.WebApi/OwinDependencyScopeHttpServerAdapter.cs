using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace Autofac.Owin.WebApi
{
    public class OwinDependencyScopeHttpServerAdapter : HttpServer
    {
        public OwinDependencyScopeHttpServerAdapter(HttpConfiguration configuration)
            : base(configuration)
        {
        }

        public OwinDependencyScopeHttpServerAdapter(HttpConfiguration configuration, HttpMessageHandler dispatcher)
            : base(configuration, dispatcher)
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Note: no need to call request.RegisterForDispose as AutofacMiddleware will dispose the ILifetimeScope instance.

            request.Properties[HttpPropertyKeys.DependencyScope] = request.GetOwinDependencyScope();
            return base.SendAsync(request, cancellationToken);
        }
    }
}