using System.Web.Mvc;

namespace CaucasianPearl.Controllers.Interface
{
    public interface IOrderedController
    {
        // Перемещение объекта вверх.
        ActionResult Up(int id);

        // Перемещение объекта вниз.
        ActionResult Down(int id);
    }
}
