using ClientFeatures.Models;
using System.Web.Mvc;

namespace ClientFeatures.Controllers {
    public class HomeController : Controller {
        public ViewResult MakeBooking() => View(new Appointment { ClientName = "Adam", TermsAccepted = true });

        [HttpPost]
        public JsonResult MakeBooking(Appointment appt) {
            // Store new appointment in a repo/database
            return Json(appt, JsonRequestBehavior.AllowGet);
        }
    }
}