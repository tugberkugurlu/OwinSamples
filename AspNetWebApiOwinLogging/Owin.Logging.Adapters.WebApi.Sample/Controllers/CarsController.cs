using System.Web.Http;
using System.Web.Http.Tracing;

namespace Owin.Logging.Adapters.WebApi.Sample.Controllers
{
    public class CarsController : ApiController
    {
        public string[] GetCars() 
        {
            ITraceWriter traceWriter = Configuration.Services.GetTraceWriter();

            traceWriter.Debug(Request, "Owin.Logging.Adapters.WebApi.Sample.Controllers.CarsController", "Special log message");
            traceWriter.Info(Request, "Owin.Logging.Adapters.WebApi.Sample.Controllers.CarsController", "Special log message");
            traceWriter.Warn(Request, "Owin.Logging.Adapters.WebApi.Sample.Controllers.CarsController", "Special log message");
            traceWriter.Error(Request, "Owin.Logging.Adapters.WebApi.Sample.Controllers.CarsController", "Special log message");
            traceWriter.Fatal(Request, "Owin.Logging.Adapters.WebApi.Sample.Controllers.CarsController", "Special log message");

            return new[] 
            {
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}
