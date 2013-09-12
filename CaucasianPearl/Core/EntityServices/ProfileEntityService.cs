using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;
using System.Linq;

namespace CaucasianPearl.Core.EntityServices
{
    public class ProfileEntityService : UrlFriendlyEntityService<Profile>, IProfileService<Profile>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage { get { return Consts.PaginatorControl.ProfileItemsPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks { get { return Consts.PaginatorControl.ProfileNumberOfVisibleLinks; } }

        #region Methods

        public IEnumerable<Profile> GetProfiles()
        {
            var profiles = Get()
                .OrderBy(p => p.LastName);

            foreach (var profile in profiles)
                profile.ImageUrl = GetProfileImage(profile);

            return profiles;
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

        public static string GetProfileImage(object profile)
        {
            return string.Format("{0}/{1}/{2}",
                VirtualPathUtility.ToAppRelative(Consts.Paths.Img.EntityImgFolder),
                Consts.Controllers.Profile.ProfileImagesFolder,
                ImageHelper.GetImageName(profile));
        }
    }
}