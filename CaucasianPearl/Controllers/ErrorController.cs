using System.Net;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Services.LoggingService;
using Ninject;

namespace CaucasianPearl.Controllers
{
    public class ErrorController : Controller
    {
        [Inject]
        public ILogService LogService { get; set; }

        public ActionResult Index(string aspxerrorpath)
        {
            LogService.Error(aspxerrorpath);

            Response.TrySkipIisCustomErrors = true;

            return NotFound(string.Empty);
        }

        // Доступ запрещён.
        public ActionResult AccessDenied(string aspxerrorpath)
        {
            LogService.Error(aspxerrorpath);

            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.TrySkipIisCustomErrors = true;

            return View(Consts.Controllers.Error.Views.AccessDenied);
        }

        // Пользователь не авторизован.
        public ActionResult NotAuthorized(string aspxerrorpath)
        {
            LogService.Error(aspxerrorpath);

            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            Response.TrySkipIisCustomErrors = true;

            return View(Consts.Controllers.Error.Views.NotAuthorized);
        }

        // Объект не найден.
        public ActionResult NotFound(string aspxerrorpath)
        {
            LogService.Error(aspxerrorpath);

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Response.TrySkipIisCustomErrors = true;

            return View(Consts.Controllers.Error.Views.NotFound);
        }

        // Объект не найден.
        public ActionResult Unexpected(string aspxerrorpath)
        {
            LogService.Error(aspxerrorpath);

            Response.StatusCode = (int)HttpStatusCode.NotFound;
            Response.TrySkipIisCustomErrors = true;
            
            return View(Consts.Controllers.Error.Views.Unexpected);
        }
    }
}