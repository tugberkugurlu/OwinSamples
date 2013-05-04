using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApi50OwinSample {

    public partial class Startup {

        public void Configuration(IAppBuilder app) {

            var httpConfig = new HttpConfiguration();
            httpConfig.Routes.MapHttpRoute("DefaultRoute", "{controller}");
            app.UseWebApi(httpConfig);
        }
    }
}