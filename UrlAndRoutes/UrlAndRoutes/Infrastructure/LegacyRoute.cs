using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlAndRoutes.Infrastructure {
    public class LegacyRoute : RouteBase {
        private string[] urls;

        public LegacyRoute(params string[] targetUrls) {
            this.urls = targetUrls;
        }

        public override RouteData GetRouteData(HttpContextBase httpContext) {
            RouteData result = null;

            string requestedUrl = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            if(urls.Contains(requestedUrl, StringComparer.OrdinalIgnoreCase)) {
                result = new RouteData(this, new MvcRouteHandler());
                result.Values.Add("controller", "Legacy");
                result.Values.Add("action", "GetLegacyUrl");
                result.Values.Add("legacyUrl", requestedUrl);
            }

            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
            VirtualPathData result = null;

            if(values.ContainsKey("legacyUrl") && urls.Contains((string)values["legacyUrl"], StringComparer.OrdinalIgnoreCase))
                result = new VirtualPathData(this, new UrlHelper(requestContext).Content((string)values["legacyUrl"]).Substring(1));

            return result;
        }
    }
}