using Autofac.Integration.WebApi;
using Autofac.Owin.WebApi;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http;

namespace Owin
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinExtensions
    {
        public static IAppBuilder UseWebApiWithAutofac(this IAppBuilder app, Autofac.IContainer container, HttpConfiguration configuration)
        {
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            HttpServer httpServer = new OwinDependencyScopeHttpServerAdapter(configuration);
            return app.UseWebApi(httpServer);
        }

        public static IAppBuilder UseWebApiWithAutofac(this IAppBuilder app, Autofac.IContainer container, HttpConfiguration configuration, HttpMessageHandler dispatcher)
        {
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            HttpServer httpServer = new OwinDependencyScopeHttpServerAdapter(configuration, dispatcher);
            return app.UseWebApi(httpServer);
        }
    }
}