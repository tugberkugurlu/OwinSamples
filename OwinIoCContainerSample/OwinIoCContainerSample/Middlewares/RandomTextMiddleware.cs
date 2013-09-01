using Autofac;
using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;

namespace OwinIoCContainerSample.Middlewares
{
    public class RandomTextMiddleware : OwinMiddleware
    {
        public RandomTextMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.Request.Path == new PathString("/random"))
            {
                ILifetimeScope dependencyScope = context.GetRequestDependencyScope();
                IRepository repository = dependencyScope.Resolve<IRepository>();
                await context.Response.WriteAsync(repository.GetRandomText());
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}