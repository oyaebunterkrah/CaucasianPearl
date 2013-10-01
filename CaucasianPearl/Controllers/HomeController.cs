using System.Linq;
using System.Text;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.EntityServices;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models;
using CaucasianPearl.Models.EDM;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Controllers
{
    public class HomeController : Controller
    {
        //[LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult Index()
        {
            var sponsorEntityService = ServiceHelper<ISponsorService<Sponsor>>.GetService();
            var eventEntityService = ServiceHelper<IEventService<Event>>.GetService();

            ViewBag.FooterVisible = true;
            ViewBag.HomePageMode = SiteSettingsHelper.GetSiteSettingTyped(Consts.SiteSettings.HomePageMode, SiteSettingsHelper.HomePageMode.Events);
            ViewBag.BigSponsors = sponsorEntityService.GetSponsors(Consts.Controllers.Sponsor.DefaultBigSponsorsCount, true);
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
            var feedbackEntityService = ServiceHelper<IFeedbackService<Feedback>>.GetService();
            var sponsorEntityService = ServiceHelper<ISponsorService<Sponsor>>.GetService();

            ViewBag.Members = profileEntityService.GetMembers();
            ViewBag.Feedbacks = feedbackEntityService.GetLastFeedback();
            ViewBag.BigSponsors = sponsorEntityService.GetSponsors(Consts.Controllers.Sponsor.DefaultBigSponsorsCount, true);
            ViewBag.SmallSponsors = sponsorEntityService.GetSponsors(Consts.Controllers.Sponsor.DefaultSmallSponsorsCount, false);

            return View();
        }

        public ActionResult Events(string @event, string eventMedia)
        {
            // устанавливаем текущее событие - eventItem {
            int eventId;
            int.TryParse(@event, out eventId);

            var eventEntityService = ServiceHelper<IEventService<Event>>.GetService();
            var eventItems = eventEntityService.GetLastEventItems(Consts.Controllers.Event.EventCount);
            var currentEventItem = eventItems.FirstOrDefault();

            if (eventId > 0)
            {
                var currentEvent = eventEntityService.Get(eventId);

                if (currentEvent != null)
                    currentEventItem = new EventItem(currentEvent);
            }

            ViewBag.CurrentEventItem = currentEventItem; // }

            // устанавливаем текущий медиа файл - mediaItem {
            var currentEventMediaItem = currentEventItem.EventMedia.FirstOrDefault();

            int eventMediaId;
            int.TryParse(eventMedia, out eventMediaId);

            if (eventMediaId > 0)
                if (currentEventItem.EventMedia.Any(em => em.ID == eventMediaId))
                    currentEventMediaItem = currentEventItem.EventMedia.FirstOrDefault(em => em.ID == eventMediaId); // }

            ViewBag.CurrentEventMediaItem = currentEventMediaItem;

            return View(eventItems);
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
    }
}