using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class EventEntityService : UrlFriendlyEntityService<Event>, IEventService<Event>
    {
        #region Properties

        // Количество объектов на одной странице.
        protected override int LinksPerPage
        {
            get { return Consts.PaginatorControl.EventItemsPerPage; }
        }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks
        {
            get { return Consts.PaginatorControl.EventNumberOfVisibleLinks; }
        }

        #endregion

        #region Methods

        // Возвращает список событий.
        // 2 события, которые были до и после
        // 1* 2* 3* 4*
        // если 1, то внизу отображается 2,3,4,5
        // если 2, то внизу отображается 1,3,4,5
        // если 4, то внизу отображается 2,3,5,6
        public IEnumerable<EventItem> GetLastEventItems(int count)
        {
            var eventItems = Get()
                .OrderBy(e => e.EventDate)
                .Take(count).ToList()
                .Select(e => new EventItem(e));

            return eventItems;
        }

        public IEnumerable<EventItemInfo> GetLastEventsInfo(int count)
        {
            var eventItems = Get()
                .OrderByDescending(e => e.EventDate)
                .Take(count).ToList()
                .Select(e => new EventItemInfo(e));

            return eventItems;
        }

        public IEnumerable<EventItem> GetNeighborEvents(int eventId)
        {
            var eventItems = new List<EventItem>();

            if (eventId <= 0)
                return eventItems;

            var currentEvent = Get(eventId);
            var allEvents = Get().OrderBy(e => e.EventDate);

            eventItems.AddRange(allEvents
                                     .Where(e => e.EventDate < currentEvent.EventDate)
                                     .OrderByDescending(e => e.EventDate)
                                     .Take(Consts.Controllers.Event.DifferenceCount).ToList()
                                     .Select(e => new EventItem(e)));
            eventItems.AddRange(allEvents
                                    .Where(e => e.EventDate > currentEvent.EventDate)
                                    .OrderBy(e => e.EventDate)
                                    .Take(Consts.Controllers.Event.DifferenceCount).ToList()
                                    .Select(e => new EventItem(e)));

            return eventItems.OrderBy(e => e.EventDate);
        }

        public IEnumerable<EventItemInfo> GetEventsForMonth()
        {
            return Get()
                .Where(e => e.EventDate.HasValue && e.EventDate.Value.Month == DateTime.Now.Month)
                .OrderBy(e => e.EventDate)
                .ToList()
                .Select(e => new EventItemInfo(e));
        }

        public IEnumerable<EventItem> GetEventsToDate(DateTime date)
        {
            return Get()
                .Where(e => e.EventDate == date.Date)
                .OrderBy(e => e.EventDate)
                .Take(Consts.Controllers.Event.EventCount)
                .ToList()
                .Select(e => new EventItem(e));
        }

        #endregion

        #region Helpers

        public string GetPrimaryPhotoUrl(EventItem eventItem)
        {
            var eventMediaItem = eventItem.EventMedia.FirstOrDefault(em => em.IsPrimary ?? false);

            return eventMediaItem != null
                ? eventMediaItem.ThumbnailUrl
                : ImageHelper.GetDefaultImageUrl(Consts.Paths.Img.SiteImgFolder);
        }

        #endregion
    }
}