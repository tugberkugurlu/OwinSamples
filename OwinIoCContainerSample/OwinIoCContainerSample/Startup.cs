using Autofac;
using Microsoft.Owin;
using Owin;
using OwinIoCContainerSample;
using OwinIoCContainerSample.Middlewares;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Startup))]
namespace OwinIoCContainerSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IContainer container = RegisterServices();

            app.UseAutofac(container)
               .Use<RandomTextMiddleware>();
        }

        public IContainer RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<Repository>().As<IRepository>().InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}