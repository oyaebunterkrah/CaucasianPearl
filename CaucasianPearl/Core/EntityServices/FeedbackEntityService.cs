using System.Collections.Specialized;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class FeedbackEntityService : BaseEntityService<Feedback>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage { get { return Consts.PaginatorControl.FeedbackItemsPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks { get { return Consts.PaginatorControl.FeedbackNumberOfVisibleLinks; } }

        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<Feedback> Get()
        {
            return base.Get().OrderByDescending(feedback => feedback.ID);
        }

        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<Feedback> Get(bool isPageable)
        {
            return base.Get(isPageable)
                       .Where(m => HttpContext.Current.User.Identity.IsAuthenticated || m.IsApproved);
        }

        // Получение количества выбранных объектов.
        public override int Count(NameValueCollection filter)
        {
            return Get(filter).Count(m => HttpContext.Current.User.Identity.IsAuthenticated || m.IsApproved);
        }
    }
}