using CaucasianPearl.Core.DAL.Interface;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IUrlFriendlyService<T> : IOrderedService<T> where T : class, IUrlFriendly, new()
    {
        // Получение объекта по краткому наименованию	
        T Get(string shortName);

        // Проверка краткого имени на уникальность и приведение его к уникальному виду
        string CreateUniqueShortName(int id, string shortName);
    }
}