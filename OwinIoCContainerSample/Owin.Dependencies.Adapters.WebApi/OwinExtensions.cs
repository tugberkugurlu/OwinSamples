using Owin.Dependencies;
using Owin.Dependencies.Adapters.WebApi;
using Owin.Dependencies.Adapters.WebApi.Infrastructure;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http;

namespace Owin
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinExtensions
    {
        public static IAppBuilder UseWebApiWithOwinDependencyResolver(this IAppBuilder app, IOwinDependencyResolver resolver, HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new OwinDependencyResolverWebApiAdapter(resolver);
            HttpServer httpServer = new OwinDependencyScopeHttpServerAdapter(configuration);
            return app.UseWebApi(httpServer);
        }

        public static IAppBuilder UseWebApiWithOwinDependencyResolver(this IAppBuilder app, IOwinDependencyResolver resolver, HttpConfiguration configuration, HttpMessageHandler dispatcher)
        {
            configuration.DependencyResolver = new OwinDependencyResolverWebApiAdapter(resolver);
            HttpServer httpServer = new OwinDependencyScopeHttpServerAdapter(configuration, dispatcher);
            return app.UseWebApi(httpServer);
        }
    }
}