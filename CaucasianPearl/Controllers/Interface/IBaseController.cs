using System.Web.Mvc;
using CaucasianPearl.Core.DAL.Interface;

namespace CaucasianPearl.Controllers.Interface
{
    public interface IBaseController<in T> where T : class, IBase, new()
    {
        // Получение списка объектов.
        ActionResult Index();

        // Открытие формы создания объекта.
        ActionResult Create();

        // Обработка формы создания объекта.
        ActionResult Create(T model);

        // Открытие формы редактирование объекта.
        ActionResult Edit(int id);

        // Обработка формы редактирования объекта
        ActionResult Edit(int id, T model);

        // Открытие страницы с подтверждением удаления объекта.
        ActionResult Delete(int id);

        // Удаление объекта после подтверждения.
        ActionResult Delete(int id, FormCollection formCollection);

        // Получение объекта по его ID.
        ActionResult Details(int id);
    }
}