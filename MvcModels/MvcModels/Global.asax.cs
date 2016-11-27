using MvcModels.Infrastructure;
using MvcModels.Models;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcModels {
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            // ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());
            ModelBinders.Binders.Add(typeof(AddressSummary), new AddressSummaryBinder());
        }
    }
}
