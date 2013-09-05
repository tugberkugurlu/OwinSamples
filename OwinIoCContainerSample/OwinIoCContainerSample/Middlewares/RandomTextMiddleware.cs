using Microsoft.Owin;
using Owin.Dependencies;
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
            if (context.Request.Path == "/random")
            {
                IOwinDependencyScope dependencyScope = context.GetRequestDependencyScope();
                IRepository repository = dependencyScope.GetService(typeof(IRepository)) as IRepository;
                await context.Response.WriteAsync(repository.GetRandomText());
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}