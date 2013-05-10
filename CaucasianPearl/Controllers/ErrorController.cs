using System.Net;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;

namespace CaucasianPearl.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            Response.TrySkipIisCustomErrors = true;

            return NotFound(string.Empty);
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }

        public ActionResult AccessDenied(string action)
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.TrySkipIisCustomErrors = true;

            return View();
        }
    }
}
