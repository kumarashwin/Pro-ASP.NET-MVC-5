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
            var currentCategory = category;

            // Returns all products if category is null, else returns those from the specified category
            var productsByCategory = repository.Products.Where(p => category == null || p.Category == category);

            // Counts either all the products in the repository (if category == null) or just those from the given category
            var pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = productsByCategory.Count() };

            // Fetched categorized products by page limited to PageSize
            var products = productsByCategory.OrderBy(p => p.ProductId).Skip((page - 1) * PageSize).Take(PageSize);
            
            return View(new ProductListViewModel() { Products = products, PagingInfo = pagingInfo, CurrentCategory = category });
        }
    }
}