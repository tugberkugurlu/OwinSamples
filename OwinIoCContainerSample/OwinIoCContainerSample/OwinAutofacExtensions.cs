using Autofac;
using Microsoft.Owin;
using Owin;
using OwinIoCContainerSample.Middlewares;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwinIoCContainerSample
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

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

        public static ILifetimeScope GetRequestDependencyScope(this IOwinContext owinContext) 
        {
            return owinContext.Environment.GetRequestDependencyScope();
        }
    }
}