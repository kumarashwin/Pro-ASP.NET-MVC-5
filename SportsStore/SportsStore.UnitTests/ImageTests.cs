using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using System.Linq;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTests {
    [TestClass]
    public class ImageTests {

        [TestMethod]
        public void Can_Retrieve_Image_Data() {
            // Arrange

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1" },
                new Product {ProductId = 2, Name = "Test", ImageData = new byte[] { }, ImageMimeType = "image/png" },
                new Product {ProductId = 3, Name = "P3" }
            }.AsQueryable());

            var controller = new ProductController(mock.Object);

            // Act
            var result = controller.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual("image/png", result.ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_Id() {
            // Arrange

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1" },
                new Product {ProductId = 2, Name = "P2" }
            }.AsQueryable());

            var controller = new ProductController(mock.Object);

            // Act
            var result = controller.GetImage(100);

            // Assert
            Assert.IsNull(result);
        }
    }
}
