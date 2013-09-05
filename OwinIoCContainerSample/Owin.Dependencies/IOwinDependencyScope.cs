using System;
using System.Collections.Generic;

namespace Owin.Dependencies
{
    public interface IOwinDependencyScope : IDisposable
    {
        object GetService(Type serviceType);
        IEnumerable<object> GetServices(Type serviceType);
    }
}