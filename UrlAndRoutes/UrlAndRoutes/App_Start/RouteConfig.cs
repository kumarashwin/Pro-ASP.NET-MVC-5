using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Routing;

namespace UrlAndRoutes
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("ShopSchema2", "Shop/OldAction", new { controller = "Home", action = "Index" });

            //routes.MapRoute("ShopSchema", "Shop/{action}", new { controller = "Home" });

            //routes.MapRoute(
            //    name: "StaticRoute",
            //    url: "Public/{controller}/{action}",
            //    defaults: new { controller = "Home", action = "Index" });

            //routes.MapRoute(
            //    name: "MyRoute",
            //    url: "{controller}/{action}",
            //    defaults: new { controller = "Home", action = "Index"});

            //routes.MapRoute(
            //    name: "CatchAll",
            //    url: "{controller}/{action}/{id}/{*catchall}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    constraints: new {
            //        controller = "^H.*", action = "^Index$|^About$",
            //        httpMethod = new HttpMethodConstraint("GET"),
            //        id = new CompoundRouteConstraint(new IRouteConstraint[]{
            //            new AlphaRouteConstraint(),
            //            new MinLengthRouteConstraint(6)
            //        })
            //    });
            //    namespaces: new[] { "UrlAndRoutes.AdditionalControllers" });
            //    .DataTokens["UseNamespaceFallback"] = false;

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
