using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller {
        public ViewResult Index() {
            ViewBag.Title = "Index";
            ViewBag.Message = TempData["Message"] ?? "Hello";
            return View(model: DateTime.Now);
        }

        public RedirectToRouteResult Redirect() => RedirectToRoute(new { controller = "Example", action = "Index"});

        public RedirectToRouteResult RedirectWithData(string message = "Default") {
            TempData["Message"] = message;
            return RedirectToAction("Index");
        }
    }
}