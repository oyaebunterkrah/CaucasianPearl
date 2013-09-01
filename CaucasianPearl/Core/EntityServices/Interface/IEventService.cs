using System;
using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IEventService<T> : IUrlFriendlyService<T> where T : class, IUrlFriendly, new()
    {
        IEnumerable<EventItem> GetLastEvents(int count);
        
        IEnumerable<EventItemInfo> GetLastEventsInfo(int count);

        IEnumerable<EventItem> GetNeighborEvents(int id);

        IEnumerable<EventItemInfo> GetEventsForMonth();

        IEnumerable<EventItem> GetEventsToDate(DateTime date);

        string GetPrimaryPhoto(EventItem eventItem);
    }
}