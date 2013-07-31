using Owin;

namespace CarsGallery.ResourceOwner.Web
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
