using System;
using System.Globalization;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;

namespace CaucasianPearl.Core.EntityServices.Abstract
{
    public abstract class UrlFriendlyEntityService<T> : OrderedEntityService<T>, IUrlFriendlyService<T>
        where T : class, IUrlFriendly, new()
    {
        // Получение объекта по его краткому имени
        public virtual T Get(string shortName)
        {
            return (from obj in _repository.DbSet where obj.ShortName == shortName select obj).FirstOrDefault();
        }

        // Проверка краткого имени на уникальность и приведение его к уникальному виду.
        public virtual string CreateUniqueShortName(int id, string shortName)
        {
            var strResult = shortName.ToLower();

            int? maxId = Get().OrderByDescending(item => item.ID).Select(item => item.ID).FirstOrDefault();
            var strAffix = maxId.Value.ToString(CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(shortName)) strResult = strAffix;

            var count = Get().Count(item => item.ShortName.ToLower() == strResult && item.ID != id);
            if (count > 0) strResult = string.Format("{0}{1}", shortName, strAffix);
            if (Consts.ReservedWords.Contains(strResult))
                strResult = string.Format("{0}{1}", strResult, strAffix);

            return strResult;
        }
    }
}