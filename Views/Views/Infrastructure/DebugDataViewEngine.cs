using System.Web.Mvc;

namespace Views.Infrastructure {
    public class DebugDataViewEngine : IViewEngine {
        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache) =>
            new ViewEngineResult(new string[] { "No view (Debug Data View Engine)" });

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache) {
            if (viewName == "DebugData")
                return new ViewEngineResult(new DebugDataView(), this);
            else
                return new ViewEngineResult(new string[] { "No view (Debug Data View Engine)" });
        }

        public void ReleaseView(ControllerContext controllerContext, IView view) { }
    }
}