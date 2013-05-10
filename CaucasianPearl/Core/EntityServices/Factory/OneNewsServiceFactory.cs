using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Factory
{
    public static class OneNewsServiceFactory
    {
        public static IUrlFriendlyService<OneNews> Create()
        {
            return new OneNewsEntityService();
        }
    }
}