using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
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
