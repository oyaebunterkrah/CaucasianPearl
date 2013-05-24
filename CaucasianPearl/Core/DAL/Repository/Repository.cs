using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Repository
{
    public class Repository<T> : IRepository<T>
        where T : class, IBase, new()
    {
        private CaucasianPearlContext _context;

        public CaucasianPearlContext Context
        {
            get { return _context ?? new CaucasianPearlContext(); }
        }

        private readonly bool _shareContext;

        public Repository()
        {
            _context = new CaucasianPearlContext();
            _shareContext = true;
        }

        public Repository(CaucasianPearlContext context)
        {
            _context = context;
            _shareContext = true;
        }

        public DbSet<T> DbSet
        {
            get { return _context.Set<T>(); }
        }

        public void Dispose()
        {
            if (_shareContext && (_context != null))
                _context.Dispose();
        }

        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<T> OrderByFilter<TOrderBy>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOrderBy>> orderPredicate)
        {
            return DbSet.Where(predicate)
                        .OrderBy(orderPredicate)
                        .AsQueryable();
        }

        public virtual IQueryable<T> OrderByDescendingFilter<TOrderBy>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOrderBy>> orderPredicate)
        {
            return DbSet.Where(predicate)
                        .OrderByDescending(orderPredicate)
                        .AsQueryable();
        }

        public virtual T Single(Expression<Func<T, bool>> predicate)
        {
            return DbSet.SingleOrDefault(predicate);
        }

        public IQueryable<T> Filter<TKey>(Expression<Func<T, bool>> predicate, out int total, int index = 0,
                                          int size = 50)
        {
            total = 0;
            return null;
        }

        public virtual IQueryable<T> Filter(Expression<Func<T, bool>> predicate, out int total, int index = 0,
                                            int size = 50)
        {
            var skipCount = index*size;
            var _resetSet = predicate != null
                                ? DbSet.Where(predicate).AsQueryable()
                                : DbSet.AsQueryable();
            _resetSet = skipCount == 0
                            ? _resetSet.Take(size)
                            : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();

            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public virtual T Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public virtual T Create(T obj)
        {
            var newEntry = DbSet.Add(obj);
            if (!_shareContext)
                _context.SaveChanges();

            return newEntry;
        }

        void IRepository<T>.Delete(T obj)
        {

        }

        public virtual int Count
        {
            get { return DbSet.Count(); }
        }

        public virtual int Delete(T obj)
        {
            DbSet.Remove(obj);

            return !_shareContext ? _context.SaveChanges() : 0;
        }

        public virtual int Update(T obj)
        {
            var entry = _context.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;

            return !_shareContext ? _context.SaveChanges() : 0;
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);

            return !_shareContext ? _context.SaveChanges() : 0;
        }
    }
}