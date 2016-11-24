using HelperMethods.Models;
using System.Web.Mvc;

namespace HelperMethods.Controllers {
    public class HomeController : Controller {

        public ActionResult Index() {
            ViewBag.Fruits = new string[] { "Apple", "Orange", "Pear" };
            ViewBag.Cities = new string[] { "New York", "London", "Paris" };

            return View(model: "This is an HTML element: <input>");
        }

        public ActionResult CreatePerson() => View(new Person());

        [HttpPost]
        public ActionResult CreatePerson(Person person) => View("DisplayPerson", person);
    }
}