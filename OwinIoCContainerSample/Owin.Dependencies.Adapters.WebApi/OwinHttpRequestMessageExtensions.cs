using Microsoft.Owin;
using Owin.Dependencies;
using Owin.Dependencies.Adapters.WebApi.Infrastructure;
using System.Web.Http.Dependencies;

namespace System.Net.Http
{
    internal static class OwinHttpRequestMessageExtensions
    {
        public static IDependencyScope GetOwinDependencyScope(this HttpRequestMessage request)
        {
            IOwinDependencyScope owinDependencyScope = request.GetOwinContext().GetRequestDependencyScope();
            return new OwinDependencyScopeWebApiAdapter(owinDependencyScope);
        }
    }
}