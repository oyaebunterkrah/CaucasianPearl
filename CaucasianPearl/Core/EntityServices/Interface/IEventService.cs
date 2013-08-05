using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IEventService<T> : IUrlFriendlyService<T> where T : class, IUrlFriendly, new()
    {
        IEnumerable<EventItem> GetNeighborElements();

        string GetNeighborElements(int id);

        string GetPrimaryPhoto(EventItem eventItem);
    }
}