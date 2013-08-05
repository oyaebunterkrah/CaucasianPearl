using System.Web.Mvc;
using CaucasianPearl.Core.DAL.Interface;

namespace CaucasianPearl.Controllers.Interface
{
    public interface IBaseController<in T> where T : class, IBase, new()
    {
        // Открытие формы создания объекта.
        ActionResult Create();

        // Обработка формы создания объекта.
        ActionResult Create(T obj);

        // Открытие страницы с подтверждением удаления объекта.
        ActionResult Delete(int id);

        // Удаление объекта после подтверждения.
        ActionResult Delete(int id, FormCollection formCollection);

        // Получение объекта по его ID.
        ActionResult Details(int id);

        // Открытие формы редактирование объекта.
        ActionResult Edit(int id);

        // Обработка формы редактирования объекта
        ActionResult Edit(int id, T obj);

        // Получение списка объектов.
        ActionResult Index();
    }
}
