using System.Text;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Models;
using Contact = CaucasianPearl.Models.Contact;

namespace CaucasianPearl.Controllers
{
    public class HomeController : Controller
    {
        //[LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult Index()
        {
            var wrapperModel = new WrapperModel();

            return View(wrapperModel);
        }

        //[LocalizedCacheAttribute(Duration = Consts.OutputCacheDuration)]
        public ActionResult About()
        {
            return View();
        }

        //[LocalizedCacheAttribute(Duration = Consts.OutputCacheDuration)]
        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            var msg = "Имеется несколько ошибок!";

            if (ModelState.IsValid)
                msg = "Спасибо! Мы скоро Вам ответим.";

            if (Request.IsAjaxRequest())
                return new JsonResult {ContentEncoding = Encoding.UTF8, Data = new {success = true, message = msg}};

            TempData["Message"] = msg;

            return View();
        }

        //[LocalizedCacheAttribute(Duration = Consts.OutputCacheDuration)]
        public ActionResult Contacts()
        {
            return View();
        }
    }
}