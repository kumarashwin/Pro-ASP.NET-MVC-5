using ControllerExtensibility.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ControllerExtensibility.Controllers {
    public class RemoteDataController : Controller
    {
        public async Task<ActionResult> Data() {
            string data = await Task<string>.Factory.StartNew(() => new RemoteService().GetRemoteData() );
            return View((object)data);
        }

        public async Task<ActionResult> DataAsync() =>
            View("Data", (object)(await new RemoteService().GetRemoteDataAsync()));
        
}