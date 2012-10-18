using ControllerTestDemo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ControllerTestDemo.Domain;
using System.Web.Mvc;
using Moq;

namespace ControllerTestDemo.Tests.Controllers
{
    
    
    /// <summary>
    ///This is a test class for UserControllerTest and is intended
    ///to contain all UserControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void IndexActionReturnsListOfUsers()
        {
            // Arrange

            // create the mock
            var mockRepository = new Mock<IUserRepository>();

            // create a list of users to return
            var users = new[] { 
                new User { username = "u1", firstname = "fn1", lastname = "ln1", password = "p1", address = "ad1", datejoined = DateTime.Now },
                new User { username = "u2", firstname = "fn2", lastname = "ln2", password = "p2", address = "ad2", datejoined = DateTime.Now },
                new User { username = "u3", firstname = "fn3", lastname = "ln3", password = "p3", address = "ad3", datejoined = DateTime.Now }  
            };

            // tell the mock that when GetAll is called,
            // return the list of users
            mockRepository.Setup(r => r.GetAll()).Returns(users);

            // pass the mocked instance, not the mock itself, to the category
            // controller using the Object property
            var controller = new UserController(mockRepository.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsInstanceOfType(
                result.ViewData.Model, 
                typeof(IEnumerable<User>), 
                "View model is wrong type");
            var model = result.ViewData.Model as IEnumerable<User>;
            Assert.AreEqual(model.Count(), 3, "Incorrect number of results");
            Assert.AreEqual(model.AsQueryable<User>().FirstOrDefault<User>()
                .username, "u1", "Incorrect data in result");
        }

        [TestMethod]
        public void IndexActionReturnsErrorViewIfNoData()
        {
            // Arrange

            // create the mock
            var mockRepository = new Mock<IUserRepository>();

            // create an empty list of users to return
            var users = new List<User>();

            // tell the mock that when GetAll is called,
            // return the list of users
            mockRepository.Setup(r => r.GetAll()).Returns(users);

            // pass the mocked instance, not the mock itself, to the category
            // controller using the Object property
            var controller = new UserController(mockRepository.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.AreEqual("Error", result.ViewName, "Incorrect view");
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CreatActionRedirectsToIndexOnValid()
        {
            // Arrange
            // create the mock
            var mockRepository = new Mock<IUserRepository>();  // don't actually need repository in this test

            User user = new User
            {
                username = "u1",
                firstname = "fn1",
                lastname = "ln1",
                password = "p1",
                address = "ad1",
                datejoined = DateTime.Now
            };

            // Save is void - do not specify any behaviour  for now
            mockRepository.Setup(r => r.Save(It.IsAny<User>()));

            // pass the mocked instance, not the mock itself, to the category
            // controller using the Object property
            var controller = new UserController(mockRepository.Object);

            // Act
            var result = controller.Create(user) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result, "Incorrect result type");
            Assert.AreEqual("Index", result.RouteValues["action"], 
                "Incorrect action in redirect");

        }

        [TestMethod]
        public void CreatActionRedisplaysViewOnInvalid()
        {
            // Arrange
            // create the mock
            var mockRepository = new Mock<IUserRepository>();  // don't actually need repository in this test

            User user = new User();
      
            // Save is void - do not specify any behaviour  for now
            mockRepository.Setup(r => r.Save(It.IsAny<User>()));

            // pass the mocked instance, not the mock itself, to the 
            // controller using the Object property
            var controller = new UserController(mockRepository.Object);
            
            // introduce a model state error
            controller.ModelState.AddModelError("key", "model is invalid");

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.IsNotNull(result, "Incorrect result type");
            Assert.AreEqual("", result.ViewName, "Incorrect view");

        }
    }
}
