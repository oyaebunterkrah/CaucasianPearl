using System;
using System.Collections.Specialized;
using System.Linq;
using CaucasianPearl.Core.DAL;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.Interface;

namespace CaucasianPearl.Core.EntityServices.Abstract
{
    public abstract class OrderedEntityService<T> : BaseEntityService<T>, IOrderedService<T>
        where T : class, IOrdered, new()
    {
        // В списке объекты должны располагаться в порядке уменьшения Sequence
        public override IQueryable<T> Get()
        {
            return base.Get().OrderBy(obj => obj.Sequence);
        }

        // Получение неполного списка объекта
        // skip - сколько первых записей пропустить, take - сколько записей получить.
        public override IQueryable<T> Get(int skip, int take)
        {
            return Get().OrderBy(obj => obj.Sequence).Skip(skip).Take(take);
        }

        // Получение неполного списка выбранных объектов.
        public override IQueryable<T> Get(NameValueCollection filter, int skip, int take)
        {
            return Get(filter).OrderByDescending(obj => obj.Sequence).Skip(skip).Take(take);
        }

        // Получение ближайшего объекта с меньшим Sequence
        public virtual T GetPrevious(T dataObject)
        {
            return Repository.DbSet.OrderByDescending(obj => obj.Sequence)
                                    .FirstOrDefault(obj => obj.Sequence < dataObject.Sequence);
        }

        // Получение ближайшего объекта с большим Sequence
        public virtual T GetNext(T dataObject)
        {
            return Repository.DbSet.OrderBy(obj => obj.Sequence)
                                    .FirstOrDefault(obj => obj.Sequence > dataObject.Sequence);
        }
        
        // Получение максимального значения Sequence из всех объектов
        public virtual int? GetMaxSequence()
        {
            return Get().OrderByDescending(obj => obj.Sequence)
                        .Select(obj => obj.Sequence)
                        .FirstOrDefault();
        }
    }
}
