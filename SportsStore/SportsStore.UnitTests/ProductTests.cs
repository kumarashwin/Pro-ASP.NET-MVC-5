using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void Generate_Category_Specific_Product_Count()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductId = 3, Name = "P3", Category = "Cat1" },
                new Product {ProductId = 4, Name = "P4", Category = "Cat2" },
                new Product {ProductId = 5, Name = "P5", Category = "Cat3" }});

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            int res1 = ((ProductListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3= ((ProductListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((ProductListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);


        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 4, Name = "P4", Category = "Oranges" }});

            var target = new NavController(mock.Object);
            string categoryToSelect = "Apples";

            //Act
            var result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Cant_Create_Categories()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 2, Name = "P2", Category = "Apples" },
                new Product {ProductId = 3, Name = "P3", Category = "Plums" },
                new Product {ProductId = 4, Name = "P4", Category = "Oranges" }});

            var target = new NavController(mock.Object);

            //Act
            var results = ((IEnumerable<string>)target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        [TestMethod]
        public void Can_Paginate_View_Model()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1" },
                new Product {ProductId = 2, Name = "P2" },
                new Product {ProductId = 3, Name = "P3" },
                new Product {ProductId = 4, Name = "P4" },
                new Product {ProductId = 5, Name = "P5" }});

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            var result = (ProductListViewModel)controller.List(null, 2).Model;

            //Assert
            var pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1" },
                new Product {ProductId = 2, Name = "P2" },
                new Product {ProductId = 3, Name = "P3" },
                new Product {ProductId = 4, Name = "P4" },
                new Product {ProductId = 5, Name = "P5" }});

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            var result = (ProductListViewModel)controller.List(null, 2).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Filter_Products()
        {
            //Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductId = 3, Name = "P3", Category = "Cat1" },
                new Product {ProductId = 4, Name = "P4", Category = "Cat2" },
                new Product {ProductId = 5, Name = "P5", Category = "Cat3" }});

            var controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Act
            var result = ((ProductListViewModel)controller.List("Cat2", 1).Model).Products.ToArray();

            //Assert
            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            HtmlHelper myHelper = null;

            var pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10};

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            //Assert
            Assert.AreEqual(
                @"<a class=""btn btn-default"" href=""Page1"">1</a>" +
                @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>" +
                @"<a class=""btn btn-default"" href=""Page3"">3</a>", result.ToString());
        }
    }
}
