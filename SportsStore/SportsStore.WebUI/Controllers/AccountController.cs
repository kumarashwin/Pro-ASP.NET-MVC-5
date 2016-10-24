using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Models;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers {
    public class AccountController : Controller
    {
        private IAuthProvider authProvider;

        public AccountController(IAuthProvider authProvider) {
            this.authProvider = authProvider;
        }

        public ViewResult Login() => View();

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl) {
            if (ModelState.IsValid) {
                if (authProvider.Authenticate(model.UserName, model.Password))
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                else
                    ModelState.AddModelError("", "Incorrect username or password");
            }
            return View();
        }
    }
}