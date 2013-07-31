using Owin;

namespace CarsGallery.ResourceServer.Web
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
