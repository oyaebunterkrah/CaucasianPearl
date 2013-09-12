using System.Text;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Controllers
{
    public class HomeController : Controller
    {
        //[LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult Index()
        {
            ViewBag.FooterVisible = true;

            ViewBag.HomePageMode = SiteSettingsHelper.GetSiteSettingTyped(Consts.SiteSettings.HomePageMode, SiteSettingsHelper.HomePageMode.Events);

            var sponsorEntityService = ServiceHelper<ISponsorService<Sponsor>>.GetService();
            ViewBag.BigSponsors = sponsorEntityService.GetSponsors(Consts.Controllers.Sponsor.DefaultBigSponsorsCount, true);

            var eventEntityService = ServiceHelper<IEventService<Event>>.GetService();
            ViewBag.Events = eventEntityService.GetLastEventsInfo(Consts.Controllers.Event.EventCount);

            if (ViewBag.HomePageMode == SiteSettingsHelper.HomePageMode.Events)
            {
                var events = ServiceHelper<IEventService<Event>>.GetService().GetLastEventsInfo(Consts.Controllers.Event.EventCount);

                return View(events);
            }

            ViewBag.CoverImagePath = Url.Content(Consts.Paths.Img.CoversFolder + SiteSettingsHelper.GetSiteSettingValueAsString(Consts.SiteSettings.CoverImageName));
            
            return View();
        }
        
        //[LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult About()
        {
            var profileEntityService = ServiceHelper<IProfileService<Profile>>.GetService();
            ViewBag.Members = profileEntityService.GetMembers();

            var feedbackEntityService = ServiceHelper<IFeedbackService<Feedback>>.GetService();
            ViewBag.Feedbacks = feedbackEntityService.GetLastFeedback();

            var sponsorEntityService = ServiceHelper<ISponsorService<Sponsor>>.GetService();
            ViewBag.BigSponsors = sponsorEntityService.GetSponsors(Consts.Controllers.Sponsor.DefaultBigSponsorsCount, true);
            ViewBag.SmallSponsors = sponsorEntityService.GetSponsors(Consts.Controllers.Sponsor.DefaultSmallSponsorsCount, false);

            return View();
        }

        //[LocalizedCacheAttribute(Duration = Consts.OutputCacheDuration)]
        public ActionResult Affiche()
        {
            var eventEntityService = ServiceHelper<IEventService<Event>>.GetService();
            if (eventEntityService != null)
            {
                var eventsForMonth = eventEntityService.GetEventsForMonth();
                ViewBag.EventsForMonth = JsonHelper.Serialize(eventsForMonth);
            }

            return View();
        }

        ////[LocalizedCacheAttribute(Duration = Consts.OutputCacheDuration)]
        //public ActionResult Contact()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Contact(Contact model)
        //{
        //    var msg = "Имеется несколько ошибок!";

        //    if (ModelState.IsValid)
        //        msg = "Спасибо! Мы скоро Вам ответим.";

        //    if (Request.IsAjaxRequest())
        //        return new JsonResult {ContentEncoding = Encoding.UTF8, Data = new {success = true, message = msg}};

        //    TempData["Message"] = msg;

        //    return View();
        //}

        ////[LocalizedCacheAttribute(Duration = Consts.OutputCacheDuration)]
        //public ActionResult Contacts()
        //{
        //    return View();
        //}
    }
}