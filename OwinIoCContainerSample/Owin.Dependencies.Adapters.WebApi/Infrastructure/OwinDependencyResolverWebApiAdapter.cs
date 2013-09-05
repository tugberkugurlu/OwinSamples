using System;
using System.Collections.Generic;
using System.Security;
using System.Web.Http.Dependencies;

namespace Owin.Dependencies.Adapters.WebApi.Infrastructure
{
    public class OwinDependencyResolverWebApiAdapter : IDependencyResolver
    {
        private readonly IOwinDependencyResolver _owinResolver;

        public OwinDependencyResolverWebApiAdapter(IOwinDependencyResolver owinResolver)
        {
            _owinResolver = owinResolver;
        }

        [SecuritySafeCritical]
        public IDependencyScope BeginScope()
        {
            IOwinDependencyScope owinScope = _owinResolver.BeginScope();
            return new OwinDependencyScopeWebApiAdapter(owinScope);
        }

        [SecuritySafeCritical]
        public object GetService(Type serviceType)
        {
            return _owinResolver.GetService(serviceType);
        }

        [SecuritySafeCritical]
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _owinResolver.GetServices(serviceType);
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            // TODO: Figure out if the IOwinDependencyResolver instance needs to be 
            // disposed by this instance.
        }
    }
}