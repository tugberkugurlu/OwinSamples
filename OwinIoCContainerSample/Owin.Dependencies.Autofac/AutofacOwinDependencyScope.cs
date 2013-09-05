using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Owin.Dependencies.Autofac
{
    public class AutofacOwinDependencyScope : IOwinDependencyScope
    {
        private bool _disposed;
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacOwinDependencyScope(ILifetimeScope lifetimeScope)
        {
            if (lifetimeScope == null)
            {
                throw new ArgumentNullException("lifetimeScope");
            }

            _lifetimeScope = lifetimeScope;
        }

        ~AutofacOwinDependencyScope()
        {
            Dispose(false);
        }

        public object GetService(Type serviceType)
        {
            return _lifetimeScope.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_lifetimeScope.IsRegistered(serviceType))
            {
                return Enumerable.Empty<object>();
            }

            Type enumerableServiceType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            object instance = _lifetimeScope.Resolve(enumerableServiceType);
            return (IEnumerable<object>)instance;
        }

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
                    if (_lifetimeScope != null)
                    {
                        _lifetimeScope.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}