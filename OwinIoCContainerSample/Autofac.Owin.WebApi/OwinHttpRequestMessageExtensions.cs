using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using System.Web.Http.Dependencies;

namespace System.Net.Http
{
    internal static class OwinHttpRequestMessageExtensions
    {
        public static IDependencyScope GetOwinDependencyScope(this HttpRequestMessage request)
        {
            ILifetimeScope lifetimeScope = request.GetOwinContext().GetRequestDependencyScope();
            return new AutofacWebApiDependencyScope(lifetimeScope);
        }
    }
}