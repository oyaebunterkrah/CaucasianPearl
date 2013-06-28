using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class EventEntityService : UrlFriendlyEntityService<Event>
    {
        #region Properties

        // Количество объектов на одной странице.
        protected override int LinksPerPage { get { return Consts.PaginatorControl.EventItemsPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks { get { return Consts.PaginatorControl.EventNumberOfVisibleLinks; } }

        #endregion

        #region Overrided Methods

        //public override System.Linq.IQueryable<Event> Get(bool isPageable)
        //{


        //    return base.Get(isPageable).;
        //}

        #endregion
    }
}