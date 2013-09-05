using Autofac;
using Owin.Dependencies.Autofac;

namespace Owin
{
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class OwinExtensions
    {
        public static IAppBuilder UseAutofacDependencyResolver(this IAppBuilder app, IContainer container) 
        {
            AutofacOwinDependencyResolver resolver = new AutofacOwinDependencyResolver(container);
            return app.UseDependencyResolver(resolver);
        }
    }
}