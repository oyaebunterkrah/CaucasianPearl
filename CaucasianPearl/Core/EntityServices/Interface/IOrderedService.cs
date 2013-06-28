using CaucasianPearl.Core.DAL.Interface;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IOrderedService<T> : IBaseService<T> where T : class, IOrdered, new()
    {
        // Получение ближайшего объекта с меньшим Sequence
        T GetPrevious(T dataObject);

        // Получение ближайшего объекта с большим Sequence
        T GetNext(T dataObject);

        // Получение максимального значения Sequence из всех объектов
        int? GetMaxSequence();

        // Добавление дополнительных значение при создании объекта.
        void AddValuesOnCreate(T obj);
    }
}