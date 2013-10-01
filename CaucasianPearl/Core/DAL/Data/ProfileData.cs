using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.EntityHelpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Data
{
    public class ProfileItem
    {
        public ProfileItem()
        {

        }

        public ProfileItem(Profile profile)
        {
            ID = profile.ID;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
            Email = profile.Email;
            Position = profile.Position;
            ImageUrl = ImageHelper.GetImageUrl(profile, Consts.Controllers.Profile.ProfileImagesFolder);
            Description = profile.Description;
            ShortName = profile.ShortName;
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
    }
}