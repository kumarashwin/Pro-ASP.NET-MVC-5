using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper getTestObject() => new MinimumDiscountHelper();

        [TestMethod]
        public void Discount_Above_100()
        {
            //Arrange
            IDiscountHelper target = getTestObject();
            decimal total = 200;

            //Act
            var discountedTotal = target.ApplyDiscount(total);

            //Assert
            Assert.AreEqual(total * 0.9M, discountedTotal);
            
        }

        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            //Arrange
            IDiscountHelper target = getTestObject();

            //Act
            decimal TenDollarDiscount = target.ApplyDiscount(10);
            decimal HundredDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);

            //Assert
            Assert.AreEqual(5, TenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            //Arrange
            IDiscountHelper target = getTestObject();

            //Act
            decimal discount5 = target.ApplyDiscount(5);
            decimal discount0 = target.ApplyDiscount(0);

            //Assert
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            //Arrange
            IDiscountHelper target = getTestObject();

            //Act
            target.ApplyDiscount(-1);

            //Assert
            
        }
    }
}
