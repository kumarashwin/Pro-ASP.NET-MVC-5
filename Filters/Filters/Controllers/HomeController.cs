using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Users = "admin")]
        public string Index() => "This is the Index action on the Home controller";

        [GoogleAuth]
        [Authorize(Users = "bob@google.com")]
        public string List() => "This is the List action method on the Home controller";
    }
}