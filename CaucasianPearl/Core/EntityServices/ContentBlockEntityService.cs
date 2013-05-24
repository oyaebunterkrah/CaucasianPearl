using System.Collections.Specialized;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class ContentBlockEntityService : OrderedEntityService<ContentBlock>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage { get { return Consts.PaginatorControl.ContentBlockItemsPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks { get { return Consts.PaginatorControl.ContentBlockNumberOfVisibleLinks; } }

        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<ContentBlock> Get()
        {
            return base.Get().OrderByDescending(contentBlock => contentBlock.ID);
        }

        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<ContentBlock> Get(bool isPageable)
        {
            return base.Get(isPageable)
                       .Where(m => HttpContext.Current.User.Identity.IsAuthenticated || m.IsPublished);
        }

        // Получение количества выбранных объектов.
        public override int Count(NameValueCollection filter)
        {
            return Get(filter).Count(m => HttpContext.Current.User.Identity.IsAuthenticated || m.IsPublished);
        }
    }
}