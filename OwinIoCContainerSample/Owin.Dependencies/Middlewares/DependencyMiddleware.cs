using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin.Dependencies.Middlewares
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class DependencyMiddleware
    {
        private readonly AppFunc _nextFunc;
        private readonly IOwinDependencyResolver _resolver;

        public DependencyMiddleware(AppFunc nextFunc, IOwinDependencyResolver resolver)
        {
            _nextFunc = nextFunc;
            _resolver = resolver;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            using (IOwinDependencyScope scope = _resolver.BeginScope())
            {
                env.Add(Constants.OwinDependencyScopeEnvironmentKey, scope);
                await _nextFunc(env);
            }
        }
    }
}