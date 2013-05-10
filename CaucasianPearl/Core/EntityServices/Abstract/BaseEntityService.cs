﻿using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using Ninject;

namespace CaucasianPearl.Core.EntityServices.Abstract
{
    public abstract class BaseEntityService<T> : IBaseService<T> where T : class, IBase, new()
    {
        //protected IRepository<T> _repository;
        [Inject]
        public IRepository<T> Repository { get; set; }


        //protected BaseEntityService() : this(repository: new Repository<T>())
        //{
        //}

        //protected BaseEntityService(IRepository<T> repository)
        //{
        //    if (repository == null)
        //        throw new ArgumentNullException("repository");

        //    _repository = repository;
        //}

        // Количество объектов на одной странице.
        protected virtual int LinksPerPage { get { return Consts.Paginator.DefaultLinksPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected virtual int NumberOfVisibleLinks { get { return Consts.Paginator.DefaultNumberOfVisibleLinks; } }

        // Получение полного списка объектов.
        public virtual IQueryable<T> Get()
        {
            return Repository.DbSet.Select(obj => obj);
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

        // Получение списка выбранных объектов.
        public virtual IQueryable<T> Get(NameValueCollection filter)
        {
            return Get();
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

        // Получение объекта по его ID.
        public virtual T Get(int id)
        {
            return Repository.DbSet.FirstOrDefault(obj => obj.ID == id);
        }

        // Получение неполного списка объекта
        // skip - сколько первых записей пропустить, take - сколько записей получить.
        public virtual IQueryable<T> Get(int skip, int take)
        {
            return Get().OrderBy(obj => obj.ID).Skip(skip).Take(take);
        }

        // Получение неполного списка выбранных объектов.
        public virtual IQueryable<T> Get(NameValueCollection filter, int skip, int take)
        {
            return Get(filter).OrderBy(obj => obj.ID).Skip(skip).Take(take);
        }

        // Добавление объекта.
        public virtual void Create(T obj)
        {
            Repository.DbSet.Add(obj);
            Repository.Context.SaveChanges();
        }

        // Сохранение изменений в объекте.
        public virtual void Update(T obj)
        {
            Repository.Context.Entry(obj).State = EntityState.Modified;
            Repository.Context.SaveChanges();
        }

        // Удаление объекта.
        public virtual void Delete(T obj)
        {
            Repository.DbSet.Remove(obj);
            Repository.Context.SaveChanges();
        }
    }
}
