using Owin;
using Owin.Dependencies;
using System.ComponentModel;

namespace Microsoft.Owin
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinContextExtensions
    {
        public static IOwinDependencyScope GetRequestDependencyScope(this IOwinContext owinContext)
        {
            return owinContext.Environment.GetRequestDependencyScope();
        }
    }
}