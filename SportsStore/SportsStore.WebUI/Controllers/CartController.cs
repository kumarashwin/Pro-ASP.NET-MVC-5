using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private Cart Cart { get{
                var cart = (Cart)Session["Cart"];
                if (cart == null)
                {
                    cart = new Cart();
                    Session["Cart"] = cart;
                }
                return cart; } }

        private RedirectToRouteResult DoSomethingToCart(int productId, string returnUrl, Action<Product> func){
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null) func(product);

            return RedirectToAction("Index", new { returnUrl });}

        public CartController(IProductRepository repository)
        {
            this.repository = repository;
        }
      
        public ViewResult Index(string returnUrl) => View(new CartIndexViewModel { Cart = Cart, ReturnUrl = returnUrl });
        public RedirectToRouteResult AddToCart(int productId, string returnUrl) => DoSomethingToCart(productId, returnUrl, (p => Cart.AddItem(p)));
        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl) => DoSomethingToCart(productId, returnUrl, (p => Cart.RemoveLine(p)));
    }
}