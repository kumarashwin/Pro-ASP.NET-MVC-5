using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers.Tests
{
    [TestClass()]
    public class AdminTests
    {
        [TestMethod()]
        public void Index_Contains_All_Products()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductId = 3, Name = "P3", Category = "Cat1" }});

            var controller = new AdminController(mock.Object);

            //Act
            var result = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            //Assert
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(result[0].Name, "P1");
            Assert.AreEqual(result[1].Name, "P2");
            Assert.AreEqual(result[2].Name, "P3");
        }

        [TestMethod]
        public void Can_Edit_Product()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductId = 3, Name = "P3", Category = "Cat1" }});
            var controller = new AdminController(mock.Object);

            // Act
            var p1 = (Product)controller.Edit(1).ViewData.Model;
            var p2 = (Product)controller.Edit(2).ViewData.Model;
            var p3 = (Product)controller.Edit(3).ViewData.Model;

            // Assert
            Assert.AreEqual(1, p1.ProductId);
            Assert.AreEqual(2, p2.ProductId);
            Assert.AreEqual(3, p3.ProductId);
        }

        [TestMethod]
        public void Cannot_Edit_Nonexisting_Product()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductId = 3, Name = "P3", Category = "Cat1" }});
            var controller = new AdminController(mock.Object);

            // Act
            var result = (Product)controller.Edit(4).ViewData.Model;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Can_Save_Valid_Changes() {
            // Arrange
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);
            var product = new Product { Name = "Test"};

            // Act
            var result = controller.Edit(product);

            // Assert
            mock.Verify(m => m.SaveProduct(product));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Cannot_Save_Invalid_Changes() {
            // Arrange
            var mock = new Mock<IProductRepository>();
            var controller = new AdminController(mock.Object);
            var product = new Product { Name = "Test" };
            controller.ModelState.AddModelError("error", "error");

            // Act
            var result = controller.Edit(product);

            // Assert
            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Can_Delete_Valid_Products() {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductId = 3, Name = "P3", Category = "Cat1" }});

            var controller = new AdminController(mock.Object);

            // Act
            controller.Delete(2);

            // Assert
            mock.Verify(m => m.DeleteProduct(2));
        }
    }
}