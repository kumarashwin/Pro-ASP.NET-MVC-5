using HelperMethods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelperMethods.Controllers {
    public class PeopleController : Controller {
        private Person[] personData = {
            new Person {FirstName = "Adam", LastName = "Freeman", Role = Role.Admin },
            new Person {FirstName = "Jacqui", LastName = "Griffith", Role = Role.User },
            new Person {FirstName = "John", LastName = "Smith", Role = Role.User },
            new Person {FirstName = "Anne", LastName = "Jones", Role = Role.Guest }
        };

        public ActionResult Index() => View();

        public IEnumerable<Person> GetData(string selectedRole) {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All")
                data = personData.Where(p => p.Role == (Role)Enum.Parse(typeof(Role), selectedRole));
            return data;
        }

        public JsonResult GetPeopleDataJson(string selectedRole = "All") =>
            Json(GetData(selectedRole)
                .Select(p => new {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Role = Enum.GetName(typeof(Role), p.Role)}), JsonRequestBehavior.AllowGet);

        public PartialViewResult GetPeopleData(string selectedRole = "All") =>
            PartialView(GetData(selectedRole));

        public ActionResult GetPeople(string selectedRole = "All") => View(model: selectedRole);
    }
}