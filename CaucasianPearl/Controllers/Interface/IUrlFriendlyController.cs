using System.Web.Mvc;
using CaucasianPearl.Core.DAL.Interface;

namespace CaucasianPearl.Controllers.Interface
{
    public interface IUrlFriendlyController<in T> where T : class, IUrlFriendly, new()
    {
        // Отображение объекта с указанным ShortName.
        ActionResult GetByShortName(string shortName);
    }
}
