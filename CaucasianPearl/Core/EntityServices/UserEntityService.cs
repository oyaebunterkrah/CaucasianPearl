using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class RequestEntityService : BaseEntityService<Request>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage { get { return Consts.PaginatorControl.RequestItemsPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks { get { return Consts.PaginatorControl.RequestNumberOfVisibleLinks; } }
    }
}