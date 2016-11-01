using System.Web.Mvc;
using System.Web.Routing;
using UrlAndRoutes.Infrastructure;

namespace UrlAndRoutes {
    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {

            //routes.Add(new Route("SayHello", new CustomRouteHandler()));

            //routes.RouteExistingFiles = true;

            routes.MapRoute("DiskFile", "Content/Static.html", new { controller = "Custom", action = "List" });

            routes.Add(new LegacyRoute(
                "~/articles/Windows_3.1_overview.html/",
                "~/old/.NET_1.0_Class_Library"));

            routes.MapRoute("MyRoute", "{controller}/{action}", null, new[] {"UrlAndRoutes.Controllers"});

            routes.MapRoute("MyOtherRoute", "App/{action}", new { controller = "Home" }, new[] { "UrlAndRoutes.Controllers" });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
