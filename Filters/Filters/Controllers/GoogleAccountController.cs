using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Filters.Controllers {

    public class GoogleAccountController : Controller {

        // The client gets redirected to this action from the OnAuthenticationChallenge method
        public ActionResult Login() => View();
        
        // Upon authentication, the client is routed back to the originally requested action
        // through returnUrl
        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl) {
            if(username.EndsWith("@google.com") && password == "secret") {
                FormsAuthentication.SetAuthCookie(username, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            } else {
                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }
        }
    }
}