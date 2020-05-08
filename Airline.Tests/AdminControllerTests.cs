using System;
using Moq;
using Airline.Models.Models;
using Airline.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Airline.BLL.Interfaces;
using System.Web.Mvc;
using Airline.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Airline.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void AllCrewMemberModelResultNotNull()
        {
            //Arrange
            var mockCrew = new Mock<ICrewMemberMethods>();
            mockCrew.Setup(a => a.GetAll());
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            AdminController controller = new AdminController(mockCrew.Object, mockFlight.Object, mockRequest.Object);

            //Act
            ViewResult result = controller.AllCrewMembers() as ViewResult;

            //Assert 
            Assert.IsNotNull(result.Model);
        }
        [TestMethod]
        public void AllCrewMemberViewEqualAllCrewMemberCshtml()
        {
            //Arrange
            var mockCrew = new Mock<ICrewMemberMethods>();
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            AdminController controller = new AdminController(mockCrew.Object, mockFlight.Object, mockRequest.Object);
            //Act
            ViewResult result = controller.AllCrewMembers() as ViewResult;
            //Assert 
            Assert.AreEqual("AllCrewMembers", result.ViewName);
        }
        [TestMethod]
        public void RequestStatusDoneRedirection()
        {
            //Arrange
            string expected = "AllRequests";
            var mockCrew = new Mock<ICrewMemberMethods>();
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            AdminController controller = new AdminController(mockCrew.Object, mockFlight.Object, mockRequest.Object);
            //Act
            RedirectToRouteResult result = controller.RequestStatusDone(0) as RedirectToRouteResult;
            //Assert
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }
        [TestMethod]
        public void EditFlightIsNotNull()
        {
            //Arrange
            var mockCrew = new Mock<ICrewMemberMethods>();
            var mockFlight = new Mock<IFlightMethods>();
            var mockRequest = new Mock<IRequestMethods>();
            AdminController controller = new AdminController(mockCrew.Object, mockFlight.Object, mockRequest.Object);
            //Act 
            ViewResult result = controller.EditFlight() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
