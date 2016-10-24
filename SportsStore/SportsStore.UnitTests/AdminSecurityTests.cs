using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using Moq;

namespace SportsStore.UnitTests {
    /// <summary>
    /// Summary description for AdminSecurityTests
    /// </summary>
    [TestClass]
    public class AdminSecurityTests {

        [TestMethod]
        public void Can_Login_With_Valid_Credentials() {
            // Arrange
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("admin", "secret")).Returns(true);

            var viewModel = new LoginViewModel() { UserName = "admin", Password = "secret" };
            var controller = new AccountController(mock.Object);

            // Act
            var result = controller.Login(viewModel, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyUrl", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cant_Login_With_Invalid_Credentials() {
            // Arrange
            var mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("wrongUser", "wrongPassword")).Returns(false);

            var viewModel = new LoginViewModel() { UserName = "wrongUser", Password = "wrongPassword" };
            var controller = new AccountController(mock.Object);

            // Act
            var result = controller.Login(viewModel, "/MyUrl");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
