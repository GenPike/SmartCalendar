using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartCalendar.Models;
using SmartCalendar.Controllers;
using System.Web.Http;
using System.Net;
using System.Web.Http.Results;
using Moq;
using SmartCalendar.Models.Abstracts;
using System.Threading.Tasks;
using System.Net.Http;
using SmartCalendar.Models.EFRepositories;
using System.Collections.Generic;

namespace SmartCalendar.Tests.Controllers
{
    [TestClass]
    public partial class EventControllerTest
    {

        [TestMethod]
        public async Task UpdateEventTest_ShouldReturn_BadRequestStatusCode_and_NotNullContent()
        {
            //arrange
            var controller = new EventController(new EventRepository());
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            
            
            //act
            controller.ModelState.AddModelError("","");
            var response = await controller.UpdateEvent(GetDemoEvent());   
         
            //assert
            Assert.IsNotNull(response);            
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public async Task UpdateEventTest_ShouldReturn_NotFoundStatusCode_And_NullContent()
        {
            //arrange            
            var repository = new EventRepository(new TestAppContext());
            var controller = new EventController(repository);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            
            //act
            var response = await controller.UpdateEvent(new Event { Id="agfgfgf"});

            //assert
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public async Task UpdateEventTest_ShouldReturnSameProduct_and_OkStatusCode()
        {
            //arrange
            var context = new TestAppContext();
            context.Events.Add(GetDemoEvent());
            var repository = new EventRepository(context);            
            var controller = new EventController(repository);
            controller.Request = new HttpRequestMessage();
            controller.Request.SetConfiguration(new HttpConfiguration());
            var item = GetDemoEvent();

            //act
            var response = await controller.UpdateEvent(item);

            //assert
            Event resultEvent;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.IsTrue(response.TryGetContentValue<Event>(out resultEvent));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(item.Id, resultEvent.Id);
        }

        Event GetDemoEvent()
        {
            return new Event() {
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
    }
}
