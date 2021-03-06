﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

using Ninject;
using WebMatrix.WebData;

using CaucasianPearl.Controllers.Interface;
using CaucasianPearl.Core;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Properties;
using CaucasianPearl.Resources;

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
        // Параметризованный конструктор
        protected BaseController(S service)
        {
            _service = service;
        }

        // Сервис для для работы с данными
        protected readonly S _service;

        // Свойство, определяющее, работает ли в списке объектов постраничный вывод.
        // Если постраничный вывод нужен, то в классе-потомке этому свойству устанавливается значение TRUE.
        protected virtual bool IsPageable { get { return false; } }

        [Inject]
        public ILogService LogService { get; set; }
        
        #region Actions

        // Получение списка объектов
        public virtual ActionResult Index()
        {
            var models = _service.Get(IsPageable);

            foreach (var model in models)
                ModifyValuesOnDetails(model);

            return View(models);
        }

        // Получение объекта по его ID
        public virtual ActionResult Details(int id)
        {
            var model = _service.Get(id);

            if (model == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            ModifyValuesOnDetails(model);

            return View(model);
        }

        // Открытие формы создания объекта
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Create()
        {
            return View();
        }

        // Обработка формы создания объекта
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Create(T model)
        {
            if (ModelState.IsValid)
            {
                ModifyValuesOnCreate(model);

                // после ModifyValuesOnCreate ещё раз проверяем на валидность
                if (ModelState.IsValid)
                {
                    _service.Create(model);

                    return OnCreated(model);
                }
            }

            return View();
        }

        // Обработка формы создания объекта без редиректа
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual void CreateExpress(T model)
        {
            if (ModelState.IsValid)
            {
                ModifyValuesOnCreate(model);

                _service.Create(model);
            }
        }

        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult CreatePartial()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult CreatePartial(T model)
        {
            try
            {
                CreateExpress(model);

                return Json(true);
            }
            catch (Exception exception)
            {
                LogService.Error(exception);

                return Json(false);
            }
        }

        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult CreatePartialWithCaptcha(T model, bool captchaValid, string captchaErrorMessage)
        {
            if (!WebSecurity.IsAuthenticated)
                if (!captchaValid)
                    ModelState.AddModelError("captcha", ErrorRes.WrongCaptchaCode);

            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new CaptchaAjaxResultData
                    {
                        success = false,
                        errorMessage = string.Join("<br />", ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))),
                        captchaIsValid = captchaValid
                    });
                }

                CreateExpress(model);

                return Json(new AjaxResultData { success = true });
            }
            catch (Exception exception)
            {
                LogService.Error(exception);

                return Json(new AjaxResultData { success = false, errorMessage = ErrorRes.UnexpectedError });
            }
        }

        // Открытие формы редактирование объекта
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Edit(int id)
        {
            var model = _service.Get(id);

            return model == null ? View(Consts.Controllers.Error.Views.NotFound) : View(model);
        }

        // Обработка формы редактирования объекта
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Edit(int id, T model)
        {
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(model))
                {
                    var old = _service.Get(id);
                    UpdateModel(old);

                    ModifyValuesOnEdit(old);

                    // после ModifyValuesOnEdit ещё раз проверяем на валидность
                    if (ModelState.IsValid)
                    {
                        _service.Update(old);

                        return OnEdited(model);
                    }
                }
            }

            return View(model);
        }

        // Обработка формы редактирования объекта без редиректа
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual void EditExpress(int id, T model)
        {
            if (ModelState.IsValid)
                if (TryUpdateModel(model))
                {
                    var old = _service.Get(id);

                    if (old != null)
                    {
                        UpdateModel(old);
                        ModifyValuesOnEdit(old);

                        _service.Update(old);
                    }
                }
        }

        // Открытие страницы с подтверждением удаления объекта
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Delete(int id)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            return View(model);
        }

        // Удаление объекта после подтверждения
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult Delete(int id, FormCollection formCollection)
        {
            var model = _service.Get(id);
            if (model == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            if (!model.CanBeDeleted())
                return View(Consts.Controllers.Error.Views.DeleteFail, model);

            var onDeleted = OnDeleted(model);

            OnDelete(model);

            _service.Delete(model);

            return onDeleted;
        }

        // Удаление объекта после подтверждения без редиректа.
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual void DeleteExpress(int id, FormCollection formCollection)
        {
            var model = _service.Get(id);

            if (model != null)
                _service.Delete(model);
        }

        // Удаление объекта без перенаправления на страницу подтверждения
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public virtual ActionResult DeleteExpress(int id)
        {
            var model = _service.Get(id);

            if (model == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            var onDeleted = OnDeleted(model);

            OnDelete(model);

            _service.Delete(model);

            return onDeleted;
        }

        // our custom server validator's validation method
        public JsonResult RecaptchaValidate(string challengeValue, string responseValue)
        {
            return Json(ReCaptchaValidation.Validate(challengeValue, responseValue));
        }

        #endregion

        #region Virtual methods

        #region On...

        // Перенаправление к странице списка объектов
        protected virtual ActionResult ReturnToList(T model)
        {
            return RedirectToAction(Consts.Actions.Index, new { page = Request.QueryString[Consts.QueryStringParameters.Page] });
        }

        // Перенаправление на страницу создания объекта
        protected virtual ActionResult ReturnToCreate(T model)
        {
            return RedirectToAction(Consts.Actions.Create, new { id = model.ID });
        }

        // Перенаправление на страницу редактирования объекта
        protected virtual ActionResult ReturnToEdit(T model)
        {
            return RedirectToAction(Consts.Actions.Edit, new { id = model.ID });
        }

        // Перенаправление на страницу деталей объекта
        protected virtual ActionResult ReturnToDetails(T model)
        {
            return RedirectToAction(Consts.Actions.Details, new { id = model.ID });
        }

        #endregion

        #region On...

        // Перенаправление после создания объекта
        protected virtual ActionResult OnCreated(T model)
        {
            return ReturnToDetails(model);
        }

        // Перенаправление после редактирования объекта
        protected virtual ActionResult OnEdited(T model)
        {
            return ReturnToDetails(model);
        }

        // Перенаправление после удаления объекта
        protected virtual ActionResult OnDeleted(T model)
        {
            return ReturnToList(model);
        }

        // Действия перед удалением объекта
        protected virtual void OnDelete(T model) { }

        #endregion

        #region ModifyValuesOn...

        // Автоматическое добавление свойств объекта, не получаемых с формы создания объекта
        protected virtual void ModifyValuesOnCreate(T model) { }

        // Изменение значений, полученных с формы создания/редактирования
        protected virtual void ModifyValuesOnEdit(T model) { }

        // Изменение значений перед отображением
        protected virtual void ModifyValuesOnDetails(T model) { }

        #endregion
        
        #endregion
    }
}