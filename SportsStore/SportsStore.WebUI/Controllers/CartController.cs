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
        private IOrderProcessor orderProcessor;

        private RedirectToRouteResult DoSomethingToCart(int productId, string returnUrl, Action<Product> func){
            var product = repository.Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null) func(product);
            return RedirectToAction("Index", new { returnUrl });}

        public CartController(IProductRepository repository, IOrderProcessor orderProcessor) {
            this.repository = repository;
            this.orderProcessor = orderProcessor;}
      
        public ViewResult Index(Cart cart, string returnUrl) => View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl) => DoSomethingToCart(productId, returnUrl, (p => cart.AddItem(p)));
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl) => DoSomethingToCart(productId, returnUrl, (p => cart.RemoveLine(p)));

        public PartialViewResult Summary(Cart cart) => PartialView(cart);

        public ViewResult Checkout() => View(new ShippingDetails());

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Sorry, your cart is empty!");

            if (ModelState.IsValid){
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            } else {
                return View(shippingDetails);
            }
        }
    }
}