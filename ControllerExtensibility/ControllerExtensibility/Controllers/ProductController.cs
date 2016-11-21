using ControllerExtensibility.Models;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class ProductController : Controller {
        public ActionResult Index() => View("Result", new Result {
            ControllerName = "Product",
            ActionName = "Index"
        });

        public ActionResult List() => View("Result", new Result {
            ControllerName = "Product",
            ActionName = "List"
        });
    }
}