using HelloMsftOwin;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[assembly: OwinStartup(typeof(Startup))]
namespace HelloMsftOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IEnumerable<string> randomTexts = new List<string> 
            {
                "Yes! This is my favorite sound in the whole world",
                "Excellent. Don't hold back!",
                "Great job! It smells like roses in here now",
                "Sometimes you just have to let 'em rip",
                "Ahhh, that felt right",
                "Ooops, I'm not sure about that one",
                "Wow, someone has been practicing",
                "You might want to be a little careful",
                "What a stinker. The paint is coming off the walls"
            };

            app.Use<RawMiddleware>(randomTexts);
        }
    }

    public class RawMiddleware : OwinMiddleware
    {
        private readonly IEnumerable<string> _randomTexts;

        public RawMiddleware(OwinMiddleware next, IEnumerable<string> randomTexts) : base(next)
        {
            _randomTexts = randomTexts;
        }

        public override async Task Invoke(IOwinContext context)
        {
            if (context.Request.Path == "/raw")
            {
                await context.Response.WriteAsync(_randomTexts.ElementAt(new Random().Next(_randomTexts.Count())));
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}
