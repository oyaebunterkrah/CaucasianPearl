using System;
using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface ISponsorService<T> : IBaseService<T> where T : class, IBase, new()
    {
        /// <summary>
        /// Возвращает спонсоров.
        /// </summary>
        /// <param name="count">Количество</param>
        /// <param name="big">Флаг: крупные/мелкие</param>
        /// <returns></returns>
        IEnumerable<SponsorItem> GetSponsors(int count, bool isBig);
    }
}