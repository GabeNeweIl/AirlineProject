using System;
using System.Web.Mvc;
using Airline.BLL.Interfaces;
using Airline.Web.Controllers;
using Airline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Airline.Tests
{
    [TestClass]
    public class DispatcherControllerTests
    {
        [TestMethod]
        public void SendRequestIsNotNull()
        {
            //Arrange
            var mockCrew = new Mock<ICrewMemberMethods>();
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            DispatcherController controller = new DispatcherController(mockCrew.Object, mockFlight.Object, mockRequest.Object);
            //Act 
            ViewResult result = controller.SendRequest() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void SendRequestAreEqual()
        {
            //Arrange
            var mockCrew = new Mock<ICrewMemberMethods>();
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            DispatcherController controller = new DispatcherController(mockCrew.Object, mockFlight.Object, mockRequest.Object);
            //Act 
            ViewResult result = controller.SendRequest() as ViewResult;
            //Assert
            Assert.AreEqual("SendRequest",result.ViewName);
        }
        [TestMethod]
        public void SendRequesRedirection()
        {
            //Arrange
            var mockCrew = new Mock<ICrewMemberMethods>();
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            DispatcherController controller = new DispatcherController(mockCrew.Object, mockFlight.Object, mockRequest.Object);
            SendRequestViewModel request = new SendRequestViewModel()
            {
                Text = ""
            };
            //Act 
            RedirectToRouteResult result = controller.SendRequest(request) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual("DispatcherPanel", result.RouteValues["action"]);
        }
    }
}
