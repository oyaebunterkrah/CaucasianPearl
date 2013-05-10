using System.Web.Mvc;
using CaucasianPearl.Models.Interface;

namespace CaucasianPearl.Controllers.Interface
{
    public interface IOrderedController<in T> where T : class, IOrdered, new()
    {
        // Перемещение объекта вверх.
        ActionResult Up(int id);

        // Перемещение объекта вниз.
        ActionResult Down(int id);
    }
}
