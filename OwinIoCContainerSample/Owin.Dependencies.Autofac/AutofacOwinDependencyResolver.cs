using Autofac;
using System;
using System.Collections.Generic;

namespace Owin.Dependencies.Autofac
{
    public class AutofacOwinDependencyResolver : IOwinDependencyResolver
    {
        /// <summary>
        /// Tag used to identify registrations that are scoped to the OWIN request level.
        /// </summary>
        private const string OwinRequestTag = "AutofacOwinRequest";
        private bool _disposed;
        private readonly ILifetimeScope _container;
        private readonly IOwinDependencyScope _rootDependencyScope;

        public AutofacOwinDependencyResolver(ILifetimeScope container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
            _rootDependencyScope = new AutofacOwinDependencyScope(container);
        }

        ~AutofacOwinDependencyResolver()
        {
            Dispose(false);
        }

        public IOwinDependencyScope BeginScope()
        {
            ILifetimeScope lifetimeScope = _container.BeginLifetimeScope(OwinRequestTag);
            return new AutofacOwinDependencyScope(lifetimeScope);
        }

        public object GetService(Type serviceType)
        {
            return _rootDependencyScope.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _rootDependencyScope.GetServices(serviceType);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_rootDependencyScope != null)
                    {
                        _rootDependencyScope.Dispose();
                    }
                }
                this._disposed = true;
            }
        }
    }
}
