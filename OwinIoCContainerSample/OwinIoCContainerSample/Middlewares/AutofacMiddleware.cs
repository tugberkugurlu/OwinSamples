using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwinIoCContainerSample.Middlewares
{
    using Autofac;
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class AutofacMiddleware
    {
        private readonly AppFunc _nextFunc;
        private readonly IContainer _container;

        public AutofacMiddleware(AppFunc nextFunc, IContainer container)
        {
            _nextFunc = nextFunc;
            _container = container;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                env.Add(Constants.AutofacDependencyScopeEnvironmentKey, scope);
                await _nextFunc(env);
            }
        }
    }
}