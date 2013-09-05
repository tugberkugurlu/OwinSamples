using Autofac;
using Microsoft.Owin;
using Owin;
using OwinIoCContainerSample;
using OwinIoCContainerSample.Middlewares;
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

            app.UseAutofac(container)
               .Use<RandomTextMiddleware>()
               .UseWebApiWithAutofac(container, config);
        }

        public IContainer RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}