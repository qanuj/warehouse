using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description; 
using Warehouse.Core;

namespace Warehouse.Controllers
{
    [RoutePrefix("api/v1/weather")]
    public class WeathersController : BasicApiController
    {
        private readonly IWarehouseService _service;

        public WeathersController(IWarehouseService service)
        {
            _service = service;
        }

        // GET: api/v1/weathers/5
        [Route("{id}"), HttpGet]
        [ResponseType(typeof(Weather))]
        public HttpResponseMessage GetWeather([FromUri]int id)
        {
            var weather = _service.FindWeather(id);
            if (weather == null)
            {
                return NotFound();
            }

            return Ok(weather);
        }
              
        // POST: api/Weathers
        [ResponseType(typeof(int))]
        [Route, HttpPost]
        public HttpResponseMessage PostWeather(Weather weather)
        {
            if (!ModelState.IsValid)
            {
                return Bad(ModelState);
            }
            var entityId = _service.AddWeather(weather);
            return Ok(entityId);
        }
    }
}
