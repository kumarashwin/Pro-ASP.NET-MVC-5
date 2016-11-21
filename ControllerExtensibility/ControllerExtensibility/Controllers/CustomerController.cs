using ControllerExtensibility.Models;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class CustomerController : Controller {
        public ActionResult Index() => View("Result", new Result {
            ControllerName = "Customer",
            ActionName = "Index"
        });

        public ActionResult List() => View("Result", new Result {
            ControllerName = "Customer",
            ActionName = "List"
        });
    }
}