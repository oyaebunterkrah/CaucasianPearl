using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Factory
{
    public static class SponsorServiceFactory
    {
        public static IBaseService<Sponsor> Create()
        {
            return new SponsorEntityService();
        }
    }
}