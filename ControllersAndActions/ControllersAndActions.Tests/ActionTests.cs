using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControllersAndActions.Controllers;
using System;

namespace ControllersAndActions.Tests {

    [TestClass]
    public class ActionTests {

        [TestMethod]
        public void ControllerTest() {

            // Arrange
            var controller = new ExampleController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("Hello", result.ViewBag.Message);
        }

        [TestMethod]
        public void RedirectTest() {

            // Arrange
            var controller = new ExampleController();

            // Act
            var result = controller.Redirect();

            // Assert
            Assert.IsFalse(result.Permanent);

            Action<Action<string, string>, string, string> test = (func, a, b) => func(a,b);
            test(Assert.AreEqual, "Example", (string)result.RouteValues["controller"]);
            test(Assert.AreEqual, "Index", (string)result.RouteValues["action"]);
        }

        [TestMethod]
        public void ViewSelectionTest() {
            // Arrange
            var controller = new ExampleController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(System.DateTime));
        }
    }
}