using ModelValidation.Models;
using System;
using System.Web.Mvc;

namespace ModelValidation.Controllers {
    public class HomeController : Controller {
        public ViewResult MakeBooking() => View(new Appointment { Date = DateTime.Now });

        public JsonResult ValidateDate(string Date) {
            DateTime parsedDate;

            if (!DateTime.TryParse(Date, out parsedDate))
                return Json("Please enter a valid date (mm/dd/yyyy)", JsonRequestBehavior.AllowGet);

            if (DateTime.Now > parsedDate)
                return Json("Please enter a date in the future", JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ViewResult MakeBooking(Appointment appt) {
            //if (string.IsNullOrEmpty(appt.ClientName))
            //    ModelState.AddModelError("ClientName", "Please enter your name");

            //if (ModelState.IsValidField("Date") && DateTime.Now > appt.Date)
            //    ModelState.AddModelError("Date", "Please enter a date in the future");

            //if (!appt.TermsAccepted)
            //    ModelState.AddModelError("TermsAccepted", "You must accept the terms");

            if (ModelState.IsValid)
                // Statements to store new Appointment...
                return View("Completed", appt);

            return View();
        }
    }
}