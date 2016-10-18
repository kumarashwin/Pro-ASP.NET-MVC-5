using SportsStore.Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // GET: Nav
        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {
            ViewBag.SelectedCategory = category;
            return PartialView(
                viewName: horizontalLayout ? "MenuHorizontal" : "Menu",
                model: repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}