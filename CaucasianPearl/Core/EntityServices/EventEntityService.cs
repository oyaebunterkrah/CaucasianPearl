using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;
using Newtonsoft.Json;

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

        #region Overrided Methods

        public IEnumerable<EventItem> GetNeighborElements()
        {
            var eventItems = Get()
                .OrderBy(e => e.EventDate)
                .Take(Consts.Controllers.Event.EventCount).ToList()
                .Select(e => new EventItem(e));

            return eventItems;
        }

        public string GetNeighborElements(int id)
        {
            if (id <= 0)
                return string.Empty;

            var currentEvent = Get(id);
            var allEvents = Get().OrderBy(e => e.EventDate);
            var eventItems = new List<EventItem>();

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

            return JsonHelper.Serialize(eventItems.OrderBy(e => e.EventDate));
        }

        #endregion

        #region Helpers

        public string GetPrimaryPhoto(EventItem eventItem)
        {
            var eventMediaItem = eventItem.EventMedia.FirstOrDefault(em => em.IsPrimary ?? false);

            return eventMediaItem != null ? eventMediaItem.ThumbnailUrl : ImageHelper.GetDefaultImageUrl();
        }

        #endregion
    }
}