using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UrlAndRoutes.Controllers
{
    public class LegacyController : Controller
    {
        // GET: Legacy
        public ActionResult GetLegacyUrl(string legacyUrl) => View(model: legacyUrl);
    }
}