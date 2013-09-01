using Autofac;
using Owin;
using System.ComponentModel;

namespace Microsoft.Owin
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinContextExtensions
    {
        public static ILifetimeScope GetRequestDependencyScope(this IOwinContext owinContext)
        {
            return owinContext.Environment.GetRequestDependencyScope();
        }
    }
}