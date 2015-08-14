using SmartCalendar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartCalendar.Controllers
{
    public class TestController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            List<Event> result = new List<Event>();
            result.Add(GetDemoEvent());

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        Event GetDemoEvent()
        {
            return new Event()
            {
                Id = "1",
                Title = "test",
                Description = "test",
                Location = "test",
                Category = Category.Fun,
                DateAdd = DateTime.Now,
                DateEnd = DateTime.Now,
                DateStart = DateTime.Now
            };
        }
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}