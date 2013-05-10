using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Factory
{
    public static class FeedbackServiceFactory
    {
        public static IBaseService<Feedback> Create()
        {
            return new FeedbackEntityService();
        }
    }
}