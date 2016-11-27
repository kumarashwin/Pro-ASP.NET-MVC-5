using MvcModels.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcModels.Controllers {
    public class HomeController : Controller {
        private Person[] personData = {
            new Person {PersonId = 1, FirstName = "Adam", LastName = "Freeman", Role = Role.Admin },
            new Person {PersonId = 2, FirstName = "Jacqui", LastName = "Griffith", Role = Role.User },
            new Person {PersonId = 3, FirstName = "John", LastName = "Smith", Role = Role.User },
            new Person {PersonId = 4, FirstName = "Anne", LastName = "Jones", Role = Role.Guest }
        };

        public ActionResult Index(int id) => View(personData.Where(p => p.PersonId == id).First());

        public ActionResult CreatePerson() => View(new Person());

        [HttpPost]
        public ActionResult CreatePerson(Person model) => View("Index", model);

        public ActionResult DisplaySummary([Bind(Prefix = "HomeAddress")] AddressSummary summary) => View(summary);

        public ActionResult Names(IList<string> names) => View(names ?? new string[0]);

        //public ActionResult Address(IList<AddressSummary> addresses) => View(addresses ?? new List<AddressSummary>());

        public ActionResult Address() {
            var addresses = new List<AddressSummary>();
            UpdateModel(addresses);
            return View(addresses);
        }
    }
}