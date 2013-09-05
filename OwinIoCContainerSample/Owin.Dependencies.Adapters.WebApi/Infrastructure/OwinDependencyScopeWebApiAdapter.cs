using System;
using System.Collections.Generic;
using System.Security;
using System.Web.Http.Dependencies;

namespace Owin.Dependencies.Adapters.WebApi.Infrastructure
{
    public class OwinDependencyScopeWebApiAdapter : IDependencyScope
    {
        private bool _disposed;
        private readonly IOwinDependencyScope _owinDependencyScope;

        [SecuritySafeCritical]
        ~OwinDependencyScopeWebApiAdapter()
        {
            Dispose(false);
        }

        public OwinDependencyScopeWebApiAdapter(IOwinDependencyScope owinDependencyScope)
        {
            _owinDependencyScope = owinDependencyScope;
        }

        [SecuritySafeCritical]
        public object GetService(Type serviceType)
        {
            return _owinDependencyScope.GetService(serviceType);
        }

        [SecuritySafeCritical]
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _owinDependencyScope.GetServices(serviceType);
        }

        [SecuritySafeCritical]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_owinDependencyScope != null)
                    {
                        _owinDependencyScope.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}