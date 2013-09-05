using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using Owin.Dependencies.Autofac;
using OwinIoCContainerSample;
using OwinIoCContainerSample.Middlewares;
using System.Reflection;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace OwinIoCContainerSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IContainer container = RegisterServices();
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultHttpRoute", "api/{controller}");

            AutofacOwinDependencyResolver resolver = new AutofacOwinDependencyResolver(container);

            app.UseDependencyResolver(resolver)
               .Use<RandomTextMiddleware>()
               .UseWebApiWithOwinDependencyResolver(resolver, config);
        }

        public IContainer RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}