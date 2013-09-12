using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class FeedbackEntityService : BaseEntityService<Feedback>, IFeedbackService<Feedback>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage
        {
            get { return Consts.PaginatorControl.FeedbackItemsPerPage; }
        }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks
        {
            get { return Consts.PaginatorControl.FeedbackNumberOfVisibleLinks; }
        }

        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<Feedback> Get()
        {
            return base.Get().OrderByDescending(feedback => feedback.ID);
        }

        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<Feedback> Get(bool isPageable)
        {
            return base.Get(isPageable)
                       .Where(f => HttpContext.Current.User.Identity.IsAuthenticated || (f.IsApproved ?? false));
        }

        // Возвращает последний отзыв.
        public FeedbackItem GetLastFeedback()
        {
            var lastFeedback =
                Get().Where(f => HttpContext.Current.User.Identity.IsAuthenticated || (f.IsApproved ?? false))
                     .OrderByDescending(f => f.Created)
                     .FirstOrDefault();

            return new FeedbackItem(lastFeedback ?? new Feedback(), true);
        }

        // Возвращает предыдущий отзыв.
        public FeedbackItem GetPreviousFeedback(int id)
        {
            var currentFeedback = Get(id);
            var previousFeedback = new Feedback();
            var isLast = false;

            if (currentFeedback != null)
            {
                var previousFeedbacks =
                    Get()
                        .Where(
                            f =>
                            (HttpContext.Current.User.Identity.IsAuthenticated || (f.IsApproved ?? false)) &&
                            f.Created < currentFeedback.Created)
                        .OrderByDescending(f => f.Created)
                        .ToList();

                previousFeedback = previousFeedbacks.FirstOrDefault();
                isLast = previousFeedbacks.Count == 1;
            }

            return new FeedbackItem(previousFeedback, isLast);
        }

        // Возвращает следующий отзыв.
        public FeedbackItem GetNextFeedback(int id)
        {
            var currentFeedback = Get(id);
            var nextFeedback = new Feedback();
            var isLast = false;

            if (currentFeedback != null)
            {
                var nextFeedbacks = Get()
                    .Where(
                        f =>
                        (HttpContext.Current.User.Identity.IsAuthenticated || (f.IsApproved ?? false)) &&
                        f.Created > currentFeedback.Created)
                    .OrderBy(f => f.Created)
                    .ToList();

                nextFeedback = nextFeedbacks.FirstOrDefault();
                isLast = nextFeedbacks.Count == 1;
            }

            return new FeedbackItem(nextFeedback, isLast);
        }

        // Получение количества выбранных объектов.
        public override int Count(NameValueCollection filter)
        {
            return Get(filter).Count(f => HttpContext.Current.User.Identity.IsAuthenticated || (f.IsApproved ?? false));
        }
    }
}