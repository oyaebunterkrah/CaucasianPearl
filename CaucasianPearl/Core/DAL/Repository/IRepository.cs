﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Gets DbSet for specified model from database
        /// </summary>
        CaucasianPearlContext Context { get; }

        /// <summary>
        /// Gets DbSet for specified model from database
        /// </summary>
        DbSet<T> DbSet { get; }

        /// <summary>
        /// Gets all objects from database
        /// </summary>
        IQueryable<T> All();

        /// <summary>
        /// Gets objects from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets objects from database by filter and ordered by.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <param name="orderByPredicate">Specified a order</param>
        IQueryable<T> OrderByFilter<TOrderBy>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOrderBy>> orderByPredicate);

        /// <summary>
        /// Gets objects from database by filter and ordered by descending.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        /// <param name="orderByDescendingPredicate">Specified a order</param>
        IQueryable<T> OrderByDescendingFilter<TOrderBy>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOrderBy>> orderByDescendingPredicate);

        /// <summary>
        /// Gets single object from database by filter.
        /// </summary>
        /// <param name="predicate">Specified a filter</param>
        T Single(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets objects from database with filting and paging.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="filter">Specified a filter</param>
        /// <param name="total">Returns the total records count of the filter.</param>
        /// <param name="index">Specified the page index.</param>
        /// <param name="size">Specified the page size</param>
        IQueryable<T> Filter<TKey>(Expression<Func<T, bool>> filter,
            out int total, int index = 0, int size = 50);

        /// <summary>
        /// Gets the object(s) is exists in database by specified filter.
        /// </summary>
        /// <param name="predicate">Specified the filter expression</param>
        bool Contains(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Find object by keys.
        /// </summary>
        /// <param name="keys">Specified the search keys.</param>
        T Find(params object[] keys);

        /// <summary>
        /// Find object by specified expression.
        /// </summary>
        /// <param name="predicate"></param>
        T Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create a new object to database.
        /// </summary>
        /// <param name="entity">Specified a new object to create.</param>
        T Create(T entity);

        /// <summary>
        /// Delete the object from database.
        /// </summary>
        /// <param name="entity">Specified a existing object to delete.</param>        
        void Delete(T entity);

        /// <summary>
        /// Delete objects from database by specified filter expression.
        /// </summary>
        /// <param name="predicate"></param>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Update object changes and save to database.
        /// </summary>
        /// <param name="entity">Specified the object to save.</param>
        int Update(T entity);

        /// <summary>
        /// Get the total objects count.
        /// </summary>
        int Count { get; }
    }
}
