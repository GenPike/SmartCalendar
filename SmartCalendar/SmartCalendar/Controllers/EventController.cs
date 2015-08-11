using Microsoft.AspNet.Identity;
using SmartCalendar.Models;
using SmartCalendar.Models.Abstracts;
using SmartCalendar.Models.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartCalendar.Controllers
{
    public class EventController : ApiController
    {
        private IRepository repository;

        public EventController(IRepository repos)
        {
            repository = repos;
        }   

        [HttpPost]
        public async Task<HttpResponseMessage> CreateEvent([FromBody]Event item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            IdentityResult result = await repository.Create(item);
            HttpResponseMessage errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateEvent([FromBody]Event item)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            IdentityResult result = await repository.Update(item);
            HttpResponseMessage errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpDelete]
        public async Task<HttpResponseMessage> RemoveEvent([FromBody] string id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            IdentityResult result = await repository.Remove(id);
            HttpResponseMessage errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetEvent(string id)
        {

            Event result = await repository.TakeEvent(id);

            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        public HttpResponseMessage GetAll([FromBody] string userId, double start, double end)
        {
            var result = repository.TakeAllFromTo(userId, start, end);
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
        #region Helpers
        private HttpResponseMessage GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return null;
        }
        #endregion
    }
}