using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize);
            var pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = repository.Products.Count() };
            var currentCategory = category;

            return View(new ProductListViewModel() { Products = products, PagingInfo = pagingInfo, CurrentCategory = category });
        }
    }
}