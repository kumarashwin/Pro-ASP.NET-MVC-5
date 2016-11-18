using Filters.Infrastructure;
using System;
using System.Web.Mvc;

namespace Filters.Controllers {
    public class HomeController : Controller
    {
        [Authorize(Users = "admin")]
        public string Index() => "This is the Index action on the Home controller";

        [GoogleAuth]
        [Authorize(Users = "bob@google.com")]
        public string List() => "This is the List action method on the Home controller";

        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public string RangeTest(int id) {
            if (id > 100)
                return $"The id value is: {id}";
            else
                throw new ArgumentOutOfRangeException("id", id, "");
        }

        [CustomAction]
        public string FilterTest() => "This is the FilterTest action";
    }
}