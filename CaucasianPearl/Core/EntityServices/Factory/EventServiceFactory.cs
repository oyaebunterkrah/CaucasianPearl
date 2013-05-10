using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Factory
{
    public static class EventServiceFactory
    {
        public static IUrlFriendlyService<Event> Create()
        {
            return new EventEntityService();
        }
    }
}