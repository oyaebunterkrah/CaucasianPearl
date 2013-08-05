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
        public IEnumerable<OneNews> OneNews;

        public WrapperModel()
        {
            var eventService = DependencyResolverHelper<IEventService<Event>>.GetService();
            Events = eventService.Get().Take(3);

            var oneNewsService = DependencyResolverHelper<IUrlFriendlyService<OneNews>>.GetService();
            OneNews = oneNewsService.Get().Take(3);
        }
    }
}
