using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldOwin {
    
    public partial class Startup {

        public void Configuration(IAppBuilder app) {

            app.UseHandler(async (request, response) => {

                response.ContentType = "text/html";
                await response.WriteAsync("OWIN Hello World!!");
            });
        }
    }
}