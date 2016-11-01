using System.Web.Mvc;

namespace UrlAndRoutes.Controllers {

    public class HomeController : Controller {

        public ActionResult Index() {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        public ActionResult CustomVariable(object id) {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            return View();
        }

        public ViewResult MyActionMethod() {
            string myActionUrl = Url.Action("Index", new { id = "MyId" });
            string myRouteUrl = Url.RouteUrl(new { controller = "Home", action = "Index" });

            //... do something with URLs...
            return View();
        }
    }
}