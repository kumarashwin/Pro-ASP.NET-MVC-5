using System.Web.Mvc;

namespace DebuggingDemo.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            int firstValue = 10;
            int secondValue = 5;
            int result = firstValue / secondValue;

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View(result);
        }
    }
}