using System;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace Filters.Infrastructure {
    public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter {

        // The addition of the Authentication attribute will result in this method
        // being called first.
        // The initially null context.Result will be populated...

        // 2nd time:
        // This method will be hit again once the authentication from the GoogleAccountController
        // has been successful. This time around the context.Result remains null and upon failing
        // the 'if' condition, the control of the program is sent directly to the inner code of
        // the action initially requested : in our case - /Home/List/
        
        // There, the context's Result is populated with the ActionResult and then the control
        // move over to the OnAuthenticationChallenge for the second time...
        
        public void OnAuthentication(AuthenticationContext context) {
            IIdentity ident = context.Principal.Identity;
            if(!ident.IsAuthenticated || !ident.Name.EndsWith("@google.com")) {
                context.Result = new HttpUnauthorizedResult();
            }
        }

        // ...and MVC will continue the execution here where the redirect to the 
        // relevant page will be performed, with the returnUrl pointing to the action
        // that has the custom authentication attribute

        // 2nd time:
        // ...so when the control hits this method for the second time, the Result field
        // is not null, nor is it populated with the HttpUnauthorizedResult from the previous
        // OnAuthentication method. Essentially, at this point, this method does nothing
        // and the client receives whatever ActionResult was stipulated in the initially
        // requested action i.e. /Home/List/
        public void OnAuthenticationChallenge(AuthenticationChallengeContext context) {
            if (context.Result == null || context.Result is HttpUnauthorizedResult) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller", "GoogleAccount" },
                    {"action", "Login" },
                    {"returnUrl", context.HttpContext.Request.RawUrl }
                });
            } else {
                FormsAuthentication.SignOut();
            }
        }
    }
}