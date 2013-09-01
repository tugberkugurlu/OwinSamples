using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autofac.Owin.Middlewares
{
    using Autofac;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class AutofacMiddleware
    {
        /// <summary>
        /// Tag used to identify registrations that are scoped to the OWIN request level.
        /// </summary>
        private const string OwinRequestTag = "AutofacOwinRequest";
        private readonly AppFunc _nextFunc;
        private readonly IContainer _container;

        public AutofacMiddleware(AppFunc nextFunc, IContainer container)
        {
            _nextFunc = nextFunc;
            _container = container;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope(OwinRequestTag))
            {
                env.Add(Constants.AutofacDependencyScopeEnvironmentKey, scope);
                await _nextFunc(env);
            }
        }
    }
}