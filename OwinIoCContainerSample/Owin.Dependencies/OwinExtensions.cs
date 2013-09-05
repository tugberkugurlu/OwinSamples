using Owin.Dependencies;
using Owin.Dependencies.Middlewares;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class OwinExtensions
    {
        public static IAppBuilder UseDependencyResolver(this IAppBuilder app, IOwinDependencyResolver resolver)
        {
            return app.Use(new Func<AppFunc, AppFunc>(nextApp => new DependencyMiddleware(nextApp, resolver).Invoke));
        }

        public static IOwinDependencyScope GetRequestDependencyScope(this IDictionary<string, object> environment)
        {
            object dependencyScope;
            if (environment.TryGetValue(Constants.OwinDependencyScopeEnvironmentKey, out dependencyScope))
            {
                return dependencyScope as IOwinDependencyScope;
            }

            return null;
        }
    }
}