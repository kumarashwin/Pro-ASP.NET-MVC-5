using System.Web.Mvc;
using WebServices.Models;

namespace WebServices.Controllers {
    public class HomeController : Controller {
        private ReservationRepository repo = ReservationRepository.Current;

        public ActionResult Index() => View(repo.GetAll());

        public ActionResult Add(Reservation item) {
            if (ModelState.IsValid) {
                repo.Add(item);
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public ActionResult Remove(int id) {
            repo.Remove(id);
            return RedirectToAction("Index");
        }

        public ActionResult Update(Reservation item) {
            if (ModelState.IsValid && repo.Update(item))
                return RedirectToAction("Index");
            return View("Index");
        }
    }
}