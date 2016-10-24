using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // GET: Admin
        public ViewResult Index() => View(repository.Products);

        public ViewResult Create() {
            ViewBag.Create = true;
            return View("Edit", new Product());
        }

        public ViewResult Edit(int id) => View(repository.Products.FirstOrDefault(p => p.ProductId == id));

        [HttpPost]
        public ActionResult Edit(Product product) {
            if (ModelState.IsValid) {
                repository.SaveProduct(product);
                TempData["message"] = string.Format($"{product.Name} has been saved");
                return RedirectToAction("Index");
            } else {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId) {
            var deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
                TempData["message"] = string.Format($"{deletedProduct.Name} was deleted");
            return RedirectToAction("Index");
        }
    }
}