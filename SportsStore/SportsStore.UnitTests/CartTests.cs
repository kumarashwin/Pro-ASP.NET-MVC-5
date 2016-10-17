using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Linq;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange
            var mock = new Mock<IOrderProcessor>();
            var cart = new Cart();
            var shippingDetails = new ShippingDetails();
            var controller = new CartController(null, mock.Object);

            //Act
            var result = controller.Checkout(cart, shippingDetails);

            //Assert
            mock.Verify(
                expression: m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                times: Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Arrange
            var mock = new Mock<IOrderProcessor>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var controller = new CartController(null, mock.Object);
            controller.ModelState.AddModelError("error", "error");

            //Act
            var result = controller.Checkout(cart, new ShippingDetails());

            //Assert
            mock.Verify(
                expression: m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                times: Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            //Arrange
            var mock = new Mock<IOrderProcessor>();

            var cart = new Cart();
            cart.AddItem(new Product(), 1);

            var controller = new CartController(null, mock.Object);

            //Act
            var result = controller.Checkout(cart, new ShippingDetails());

            //Assert
            mock.Verify(
                expression: m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()),
                times: Times.Once());

            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductId = 1, Name = "P1", Category = "Apples" } }.AsQueryable());

            var cart = new Cart();

            var controller = new CartController(mock.Object, null);

            //Act
            controller.AddToCart(cart, 1, null);

            //Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductId, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductId = 1, Name = "P1", Category = "Apples" } }.AsQueryable());

            var cart = new Cart();

            var controller = new CartController(mock.Object, null);

            //Act
            var result = controller.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            //Arrange
            var cart = new Cart();
            var controller = new CartController(null, null);

            //Act
            var result = (CartIndexViewModel)controller.Index(cart, "myUrl").ViewData.Model;

            //Assert
            Assert.AreSame(cart, result.Cart);
            Assert.AreEqual("myUrl", result.ReturnUrl);
        }

        [TestMethod]
        public void Can_Add_New_Lines()
        {
            //Arrange
            var p1 = new Product { ProductId = 1, Name = "P1", Category = "Cat1" };
            var p2 = new Product { ProductId = 2, Name = "P2", Category = "Cat2" };

            var cart = new Cart();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            var results = cart.Lines.ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange
            var p1 = new Product { ProductId = 1, Name = "P1", Category = "Cat1" };
            var p2 = new Product { ProductId = 2, Name = "P2", Category = "Cat2" };

            var cart = new Cart();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);
            var results = cart.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange
            var p1 = new Product { ProductId = 1, Name = "P1", Category = "Cat1" };
            var p2 = new Product { ProductId = 2, Name = "P2", Category = "Cat2" };
            var p3 = new Product { ProductId = 3, Name = "P3", Category = "Cat3" };

            var cart = new Cart();

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p3, 5);
            cart.AddItem(p2, 1);

            //Act
            cart.RemoveLine(p2);

            //Assert
            Assert.AreEqual(0, cart.Lines.Where(c => c.Product == p2).Count());
            Assert.AreEqual(2, cart.Lines.Count());
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            //Arrange
            var p1 = new Product { ProductId = 1, Name = "P1", Category = "Cat1", Price = 100M };
            var p2 = new Product { ProductId = 2, Name = "P2", Category = "Cat2", Price = 50M };

            var cart = new Cart();



            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 3);
            var result = cart.ComputeTotalValue();

            //Assert
            Assert.AreEqual(450M, result);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            //Arrange
            var p1 = new Product { ProductId = 1, Name = "P1", Category = "Cat1", Price = 100M };
            var p2 = new Product { ProductId = 2, Name = "P2", Category = "Cat2", Price = 50M };

            var cart = new Cart();

            //Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 3);
            cart.Clear();

            //Assert
            Assert.AreEqual(0, cart.Lines.Count());
        }
    }
}
