using System;
using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices.Interface
{
    public interface IProfileService<T> : IUrlFriendlyService<T> where T : class, IUrlFriendly, new()
    {
        /// <summary>
        /// Возвращает профили пользователей.
        /// </summary>
        /// <returns>IEnumerable of Profile для Index</returns>
        IEnumerable<Profile> GetProfiles(bool isPageble);

        /// <summary>
        /// Возвращает участников ансамбля.
        /// </summary>
        /// <returns>IEnumerable of ProfileItem для Members</returns>
        IEnumerable<ProfileItem> GetMembers();
    }
}