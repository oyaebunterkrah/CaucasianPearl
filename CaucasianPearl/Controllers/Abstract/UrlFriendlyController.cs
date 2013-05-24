using System;
using System.Linq;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Interface;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;

namespace CaucasianPearl.Controllers.Abstract
{
    /// <summary>
    /// UrlFriendlyController
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    /// <typeparam name="S">service</typeparam>
    public abstract class UrlFriendlyController<T, S> : OrderedController<T, S>, IUrlFriendlyController<T>
        where T : class, IUrlFriendly, new()
        where S : IUrlFriendlyService<T>
    {
        protected UrlFriendlyController(S service) : base(service) { }

        // Количество объектов на одной странице по умолчанию.
        public virtual int PagerLinksPerPage { get { return Consts.PaginatorControl.DefaultItemsPerPage; } }
        // Количество отображаемых страниц перед многоточием по умолчанию.
        public virtual int NumberOfVisibleLinks  { get { return Consts.PaginatorControl.DefaultNumberOfVisibleLinks; } }

        // Отображение объекта с указанным ShortName
        public ActionResult GetByShortName(string shortName)
        {
            if (string.IsNullOrWhiteSpace(shortName))
            {
                var objs = _service.Get(IsPageable);

                return View(Consts.Actions.Index, objs);
            }

            var obj = _service.Get(shortName);

            return obj == null
                ? View(Consts.Controllers.Error.Views.NotFound)
                : View(Consts.Views.Details, obj);
        }

        #region Overridden virtual methods

        // При создании или сохранении объекта:
        // если свойство ShortName пустое, формируем его из значения shortNameSource
        // затем проверяем его свойство ShortName и приводим его к уникальному виду
        protected override void ChangeValuesOnEdit(T model)
        {
            base.ChangeValuesOnEdit(model);

            if (string.IsNullOrWhiteSpace(model.ShortName))
            {
                var shortNameSource = GetShortNameSource(model);
                if (!string.IsNullOrWhiteSpace(shortNameSource))
                    model.ShortName = Transliterate(shortNameSource);
            }

            model.ShortName = _service.CreateUniqueShortName(model.ID, model.ShortName);
        }

        #endregion

        #region Virtual methods

        // В классе-потомке можно переопределить этот метод, чтобы он возвращал источник формирования ShortName
        protected virtual string GetShortNameSource(T obj)
        {
            return string.Empty;
        }

        #endregion

        #region Methods

        // Перевод источника формирования ShortName в маленькие латинские буквы без пробелов
        protected string Transliterate(string param)
        {
            var strResult = param
                .Where(ch => Consts.Translit.Keys.Contains(ch))
                .Aggregate(string.Empty, (current, ch) => current + Consts.Translit[ch]);

            if (String.IsNullOrWhiteSpace(strResult)) strResult += "_";

            return strResult;
        }

        #endregion
    }
}
