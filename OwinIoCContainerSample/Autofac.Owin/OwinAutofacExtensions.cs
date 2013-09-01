using Autofac;
using Autofac.Owin;
using Autofac.Owin.Middlewares;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class OwinAutofacExtensions
    {
        public static IAppBuilder UseAutofac(this IAppBuilder app, IContainer container)
        {
            return app.Use(new Func<AppFunc, AppFunc>(nextApp => new AutofacMiddleware(nextApp, container).Invoke));
        }

        public static ILifetimeScope GetRequestDependencyScope(this IDictionary<string, object> environment)
        {
            object dependencyScope;
            if (environment.TryGetValue(Constants.AutofacDependencyScopeEnvironmentKey, out dependencyScope))
            {
                return dependencyScope as ILifetimeScope;
            }

            return null;
        }
    }
}