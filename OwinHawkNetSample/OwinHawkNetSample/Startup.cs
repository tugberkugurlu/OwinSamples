using HawkNet;
using HawkNet.Owin;
using Owin;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinHawkNetSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute("Default", "api/{controller}");

            app.SetLoggerFactory(new ConsoleLoggerFactory());

            app.UseHawkAuthentication(new HawkAuthenticationOptions
            {
                Credentials = (id) => 
                {
                    return Task.FromResult(new HawkCredential
                    {
                        Id = "dh37fgj492je",
                        Key = "werxhqb98rpaxn39848xrunpaw3489ruxnpa98w4rxn",
                        Algorithm = "hmacsha256",
                        User = "steve"
                    });
                }
            });

            app.UseWebApi(config);
        }
    }

    [Authorize]
    public class CarsController : ApiController
    {
        public string[] Get() 
        {
            var user = User;
            var request = Request;

            return new[] 
            { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}
