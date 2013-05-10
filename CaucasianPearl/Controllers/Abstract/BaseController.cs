﻿using System.Web.Mvc;
using CaucasianPearl.Controllers.Interface;
using CaucasianPearl.Core;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Models.Interface;

namespace CaucasianPearl.Controllers.Abstract
{
    /// <summary>
    /// BaseController
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    /// <typeparam name="S">service</typeparam>
    public abstract class BaseController<T, S> : Controller, IBaseController<T>
        where T : class, IBase, new()
        where S : IBaseService<T>
    {
        #region QueryString parameters

        private const string PageQs = "page";

        #endregion

        // Объект для для работы с данными.
        protected readonly S _service;

        // Свойство, определяющее, работает ли в списке объектов постраничный вывод.
        // Если постраничный вывод нужен, то в классе-потомке этому свойству устанавливается значение TRUE.
        protected virtual bool IsPageable { get { return false; } }

        // Параметризованный конструктор.
        protected BaseController(S service)
        {
            _service = service;
        }

        #region Actions

        // Получение списка объектов.
        public virtual ActionResult Index()
        {
            var models = _service.Get(IsPageable);

            return View(models);
        }

        // Получение объекта по его ID.
        public virtual ActionResult Details(int id)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Views.NotFound);
            return View(model);
        }

        // Открытие формы создания объекта.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Create()
        {
            return View();
        }

        // Обработка формы создания объекта.
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Create(T obj)
        {
            if (ModelState.IsValid)
            {
                _service.Create(obj);

                return OnCreated(obj);
            }

            return View();
        }

        // Открытие формы редактирование объекта.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Edit(int id)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Views.NotFound);
            return View(model);
        }

        // Обработка формы редактирования объекта
        [HttpPost]
        public virtual ActionResult Edit(int id, T obj)
        {
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(obj))
                {
                    ChangeFormCollectionValues(obj);
                    var old = _service.Get(id);
                    UpdateModel(old);
                    _service.Update(old);

                    return OnEdited(obj);
                }
            }

            return View(obj);
        }

        // Открытие страницы с подтверждением удаления объекта.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Delete(int id)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Views.NotFound);
            return View(model);
        }

        // Удаление объекта после подтверждения.
        [HttpPost]
        public virtual ActionResult Delete(int id, FormCollection collection)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Views.NotFound);

            if (!model.CanBeDeleted())
                return View(Consts.Views.DeleteFail, model);

            var onDeleted = OnDeleted(model);

            OnDelete(model);
            _service.Delete(model);
            return onDeleted;
        }

        // Удаление объекта без перенаправления на страницу подтверждения.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult DeleteExpress(int id)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Views.NotFound);

            var onDeleted = OnDeleted(model);

            OnDelete(model);
            _service.Delete(model);
            return onDeleted;
        }

        #endregion

        #region Virtual methods

        // Перенаправление к странице списка объектов.
        protected virtual ActionResult ReturnToList(T model)
        {
            return RedirectToAction(Consts.Actions.Index, new { page = Request.QueryString[PageQs] });
        }

        // Перенаправление к странице объекта.
        protected virtual ActionResult ReturnToObject(T model)
        {
            return RedirectToAction(Consts.Actions.Details, new { id = model.ID, page = Request.QueryString[PageQs] });
        }

        // Перенаправление после создания объекта.
        protected virtual ActionResult OnCreated(T model)
        {
            return ReturnToObject(model);
        }

        // Перенаправление после редактирования объекта.
        protected virtual ActionResult OnEdited(T model)
        {
            return ReturnToObject(model);
        }

        // Перенаправление после удаления объекта.
        protected virtual ActionResult OnDeleted(T model)
        {
            return ReturnToList(model);
        }

        // Изменение значений, полученных с формы создания/редактирования.
        protected virtual void ChangeFormCollectionValues(dynamic obj) { }

        // Автоматическое добавление свойств объекта, не получаемых с формы создания объекта.
        protected virtual void AddValuesOnCreate(T model) { }

        // Действия при удалении объекта
        protected virtual void OnDelete(T model) { }

        #endregion
    }
}
