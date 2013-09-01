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

        public IEnumerable<EventItem> GetLastEvents(int count)
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
        
        /// <summary>
        /// Возвращает соседние события.
        /// </summary>
        /// <param name="id">ID события</param>
        /// <returns></returns>
        public IEnumerable<EventItem> GetNeighborEvents(int id)
        {
            var eventItems = new List<EventItem>();

            if (id <= 0)
                return eventItems;

            var currentEvent = Get(id);
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

        /// <summary>
        /// Возвращает события на месяц.
        /// </summary>
        /// <returns>События в формате JSON</returns>
        public IEnumerable<EventItemInfo> GetEventsForMonth()
        {
            return Get()
                .Where(e => e.EventDate.HasValue && e.EventDate.Value.Month == DateTime.Now.Month)
                .OrderBy(e => e.EventDate)
                .ToList()
                .Select(e => new EventItemInfo(e));
        }

        /// <summary>
        /// Возвращает события на указанную дату.
        /// </summary>
        /// <param name="date">Дата события</param>
        /// <returns></returns>
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

        public string GetPrimaryPhoto(EventItem eventItem)
        {
            var eventMediaItem = eventItem.EventMedia.FirstOrDefault(em => em.IsPrimary ?? false);

            return eventMediaItem != null ? eventMediaItem.ThumbnailUrl : ImageHelper.GetDefaultImageUrl(Consts.FoldersPathes.CommonImagesFolder);
        }

        #endregion
    }
}