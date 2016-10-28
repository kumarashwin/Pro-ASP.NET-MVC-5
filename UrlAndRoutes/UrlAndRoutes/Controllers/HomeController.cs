using System.Web.Mvc;

namespace UrlAndRoutes.Controllers {

    [RoutePrefix("Home")]
    public class HomeController : Controller {

        [Route("~/Index")]
        public ActionResult Index() {
            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            return View("ActionName");
        }

        [Route("CustomVariable/{id:alpha:minlength(6)}")]
        public ActionResult CustomVariable(string id) {
            ViewBag.Controller = "Home";
            ViewBag.Action = "CustomVariable";
            ViewBag.CustomVariable = id;
            return View();
        }
    }
}