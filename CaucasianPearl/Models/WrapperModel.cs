using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Models
{
    public class WrapperModel
    {
        public IEnumerable<Event> Events;
        public IEnumerable<OneNews> OneNews;

        public WrapperModel()
        {
            var eventService = DependencyResolver.Current.GetService(typeof(IUrlFriendlyService<Event>)) as IUrlFriendlyService<Event>;
            if (eventService != null)
                Events = eventService.Get().Take(3);

            var oneNewsService = DependencyResolver.Current.GetService(typeof(IUrlFriendlyService<OneNews>)) as IUrlFriendlyService<OneNews>;
            if (oneNewsService != null)
                OneNews = oneNewsService.Get().Take(3);
        }
    }
}
