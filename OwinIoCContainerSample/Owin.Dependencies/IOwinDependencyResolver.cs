using System.Collections.Generic;

namespace Owin.Dependencies
{
    public interface IOwinDependencyResolver : IOwinDependencyScope
    {
        IOwinDependencyScope BeginScope();
    }
}