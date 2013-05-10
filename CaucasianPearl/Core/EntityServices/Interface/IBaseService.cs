using System.Linq;
using CaucasianPearl.Core.DAL.Interface;
using System.Collections.Specialized;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IBaseService<T> where T : class, IBase, new()
    {
        // Получение всех записей из таблицы БД.
        IQueryable<T> Get();

        // Получение выбранных записей.
        IQueryable<T> Get(NameValueCollection filter);

        // Получение выбранных записей Pageble.
        IQueryable<T> Get(bool isPageable);

        // Получение количества всех записей.
        int Count();

        // Получение количества выбранных записей.
        int Count(NameValueCollection filter);

        // Получение одной записи с заданным ID.
        T Get(int id);

        // Получение нескольких записей.
        // Параметр skip - сколько первых записей пропустить, параметр take - сколько записей получить.
        IQueryable<T> Get(int skip, int take);

        // Получение нескольких выбранных записей.
        IQueryable<T> Get(NameValueCollection filter, int skip, int take);

        // Добавление записи в таблицу.
        void Create(T dataObject);

        // Редактирование записи таблицы.
        void Update(T dataObject);

        // Удаление записи из таблицы.
        void Delete(T dataObject);

    }
}