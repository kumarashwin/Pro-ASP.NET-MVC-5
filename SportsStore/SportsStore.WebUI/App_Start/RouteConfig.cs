using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Lists the first page of products from all categories
            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Product", action = "List", category = (string)null, page = 1}
                );

            // Lists the specified page of products from all categories
            routes.MapRoute(
                name: null,
                url: "Page/{page}",
                defaults: new { controller = "Product", action = "List", category = (string)null },
                constraints: new {page = @"\d+"}
                );

            // Lists the first page of products from the specified categories
            routes.MapRoute(
                name: null,
                url: "{category}",
                defaults: new { controller = "Product", action = "List", page = 1 }
                );

            // Lists the specified page from the specified category
            routes.MapRoute(
                name: null,
                url: "{category}/Page/{page}",
                defaults: new { controller = "Product", action = "List" },
                constraints: new { page = @"\d+" }
            );

            // Default
            routes.MapRoute(
                name: "default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
                );
        }
    }
}