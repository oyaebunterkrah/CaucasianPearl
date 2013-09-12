using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Models
{
    public class WrapperModel
    {
        public IEnumerable<Event> Events;
        public IEnumerable<Sponsor> Sponsor;

        public WrapperModel()
        {
            var eventService = ServiceHelper<IEventService<Event>>.GetService();
            Events = eventService.Get().Take(3);

            var Service = ServiceHelper<IBaseService<Sponsor>>.GetService();
            Sponsor = Service.Get().Take(3);
        }
    }
}