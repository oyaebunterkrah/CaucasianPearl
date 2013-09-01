using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.DAL.Interface;
using System.Collections.Specialized;
using CaucasianPearl.Core.EntityServices.Abstract;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IBaseService<T> where T : class, IBase, new()
    {
        /// <summary>
        /// Получение всех записей из таблицы БД.
        /// </summary>
        IQueryable<T> Get();

        /// <summary>
        /// Получение одной записи с заданным ID.
        /// </summary>
        T Get(int id);

        /// <summary>
        /// Получение списка записей по условию.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        List<T> Get(Func<T, bool> condition);

        /// <summary>
        /// Получение одной записи по условию.
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T GetFirstByCondition(Func<T, bool> condition);

        /// <summary>
        /// Получение записей по условию.
        /// </summary>
        IQueryable<T> Get(NameValueCollection filter);

        /// <summary>
        /// Получение выбранных записей Pageble.
        /// </summary>
        IQueryable<T> Get(bool isPageable);

        /// <summary>
        /// Получение количества всех записей.
        /// </summary>
        int Count();

        /// <summary>
        /// Получение количества выбранных записей по условию.
        /// </summary>
        int Count(NameValueCollection filter);

        /// <summary>
        /// Получение нескольких записей.
        /// Параметр skip - сколько первых записей пропустить, параметр take - сколько записей получить.
        /// </summary>
        IQueryable<T> Get(int skip, int take);

        /// <summary>
        /// Получение нескольких выбранных записей.
        /// </summary>
        IQueryable<T> Get(NameValueCollection filter, int skip, int take);

        /// <summary>
        /// Создание записи в таблице.
        /// </summary>
        void Create(T obj);

        /// <summary>
        /// Редактирование записи таблицы.
        /// </summary>
        void Update(T obj);

        /// <summary>
        /// Удаление записи из таблицы.
        /// </summary>
        void Delete(T obj);

        /// <summary>
        /// Удаление записи из таблицы по его ID.
        /// </summary>
        void Delete(int id);
    }
}