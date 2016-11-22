using System.Web.Mvc;

namespace WorkingWithRazor.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() => View(new string[] { "Apple", "Orange", "Pear" });

        public ActionResult List() => View();
    }
}