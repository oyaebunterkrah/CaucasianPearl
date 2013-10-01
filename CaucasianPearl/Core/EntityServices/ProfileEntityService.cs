using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;
using System.Linq;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Core.EntityServices
{
    public class ProfileEntityService : UrlFriendlyEntityService<Profile>, IProfileService<Profile>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage
        {
            get { return Consts.PaginatorControl.ProfileItemsPerPage; }
        }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks
        {
            get { return Consts.PaginatorControl.ProfileNumberOfVisibleLinks; }
        }

        #region Methods

        public IEnumerable<Profile> GetProfiles(bool isPageable)
        {
            return Get(isPageable).OrderBy(p => p.LastName);
        }

        public IEnumerable<ProfileItem> GetMembers()
        {
            var profileItems = Get()
                .OrderBy(p => p.LastName)
                .ToList()
                .Select(p => new ProfileItem(p));

            return profileItems;
        }

        #endregion

        #region Helpers

        public static Dictionary<string, string> GetAllRoles()
        {
            return Roles.GetAllRoles()
                        .Where(role => role != Consts.Roles.Admin)
                        .ToDictionary(role => role, role => Consts.Roles.RolesDict[role]);
        }

        public static string GetProfileRoleTitle(Profile profile)
        {
            return Consts.Roles.RolesDict[profile.webpages_Roles.RoleName];
        }

        #endregion
    }
}