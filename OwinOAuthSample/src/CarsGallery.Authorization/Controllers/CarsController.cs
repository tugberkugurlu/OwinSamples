using CarsGallery.Authorization.Filters;
using CarsGallery.Authorization.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarsGallery.Authorization.Controllers
{
    [InvalidModelStateFilter]
    public class CarsController : ApiController
    {
        private readonly CarsContext _carsCtx = new CarsContext();

        [Scope("Read")]
        public IEnumerable<Car> Get()
        {
            return _carsCtx.All;
        }

        [Scope("Read")]
        public Car GetCar(int id)
        {
            var carTuple = _carsCtx.GetSingle(id);

            if (!carTuple.Item1)
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return carTuple.Item2;
        }

        [Scope("Write")]
        public HttpResponseMessage PostCar(Car car)
        {
            var createdCar = _carsCtx.Add(car);
            var response = Request.CreateResponse(HttpStatusCode.Created, createdCar);
            response.Headers.Location = new Uri(
                Url.Link("DefaultHttpRoute", new { id = createdCar.Id }));

            return response;
        }

        [Scope("Write")]
        public Car PutCar(int id, Car car)
        {
            car.Id = id;

            if (!_carsCtx.TryUpdate(car))
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return car;
        }

        [Scope("Delete")]
        public HttpResponseMessage DeleteCar(int id)
        {
            if (!_carsCtx.TryRemove(id))
            {
                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
