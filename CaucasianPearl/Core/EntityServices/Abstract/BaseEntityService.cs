using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.DAL.Repository;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using Ninject;

namespace CaucasianPearl.Core.EntityServices.Abstract
{
    public abstract class BaseEntityService<T> : IBaseService<T> where T : class, IBase, new()
    {
        [Inject]
        public IRepository<T> _repository { get; set; }

        // Количество объектов на одной странице.
        protected virtual int LinksPerPage
        {
            get { return Consts.PaginatorControl.DefaultItemsPerPage; }
        }

        // Количество отображаемых страниц перед многоточием.
        protected virtual int NumberOfVisibleLinks
        {
            get { return Consts.PaginatorControl.DefaultNumberOfVisibleLinks; }
        }

        // Получение полного списка объектов.
        public virtual IQueryable<T> Get()
        {
            return _repository.DbSet.Select(obj => obj).OrderByDescending(obj => obj.ID);
        }

        // Получение объекта по его ID.
        public virtual T Get(int id)
        {
            return _repository.DbSet.FirstOrDefault(obj => obj.ID == id);
        }

        // Получение списка объектов по условию.
        public virtual List<T> Get(Func<T, bool> condition)
        {
            return Get().Where(condition).ToList();
        }

        /// Получение объекта по условию.
        public virtual T GetFirstByCondition(Func<T, bool> condition)
        {
            return Get().FirstOrDefault(condition);
        }

        // Получение списка выбранных объектов.
        public virtual IQueryable<T> Get(NameValueCollection filter)
        {
            return Get();
        }

        // Получение списка выбранных объектов Pageble.
        public virtual IQueryable<T> Get(bool isPageable)
        {
            var context = HttpContext.Current;
            if (context == null)
                throw new Exception("context");

            return isPageable
                       ? Get(context.Request.QueryString, (ControllerHelper.GetCurrentPageNumber() - 1) * LinksPerPage, LinksPerPage)
                       : Get(context.Request.QueryString);
        }

        // Получение количества всех объектов.
        public virtual int Count()
        {
            return Get().Count();
        }

        // Получение количества выбранных объектов.
        public virtual int Count(NameValueCollection filter)
        {
            return Get(filter).Count();
        }

        // Получение неполного списка объекта
        // skip - сколько первых записей пропустить, take - сколько записей получить.
        public virtual IQueryable<T> Get(int skip, int take)
        {
            return Get().Skip(skip).Take(take);
        }

        // Получение неполного списка выбранных объектов.
        public virtual IQueryable<T> Get(NameValueCollection filter, int skip, int take)
        {
            return Get(filter).Skip(skip).Take(take);
        }

        // Добавление объекта.
        public virtual void Create(T obj)
        {
            _repository.DbSet.Add(obj);
            _repository.Context.SaveChanges();
        }

        // Сохранение изменений в объекте.
        public virtual void Update(T obj)
        {
            _repository.Context.Entry(obj).State = EntityState.Modified;
            _repository.Context.SaveChanges();
        }

        // Удаление объекта.
        public virtual void Delete(T obj)
        {
            _repository.DbSet.Remove(obj);
            _repository.Context.SaveChanges();
        }

        // Удаление объекта по ID.
        public virtual void Delete(int id)
        {
            var obj = _repository.DbSet.FirstOrDefault(i => i.ID == id);

            if (obj != null)
            {
                _repository.DbSet.Remove(obj);
                _repository.Context.SaveChanges();
            }
        }
    }
}