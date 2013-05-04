using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApiHelloOwin {

    public class CarsController : ApiController {

        public string[] Get() { 

            return new[] {
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}