using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    [Authorize]
    public class AdminController : Controller {

        public ViewResult Index() {

            return View();
        }
    }
}