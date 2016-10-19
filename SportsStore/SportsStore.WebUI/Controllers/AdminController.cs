using SportsStore.Domain.Abstract;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        // GET: Admin
        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int id) => View(repository.Products.FirstOrDefault(p => p.ProductId == id));
    }
}