using ControllerExtensibility.Infrastructure;
using ControllerExtensibility.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() => View("Result", new Result { ControllerName = "Home", ActionName = "Index" });

        [Local]
        [ActionName("Index")]
        public ActionResult LocalIndex() => View("Result", new Result { ControllerName = "Home", ActionName = "LocalIndex" });
    }
}