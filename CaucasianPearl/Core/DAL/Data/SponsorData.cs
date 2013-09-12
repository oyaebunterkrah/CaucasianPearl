using System;
using System.Collections.Generic;
using System.Linq;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.EntityHelpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.DAL.Data
{
    public class SponsorItem
    {
        public SponsorItem()
        {

        }

        public SponsorItem(Sponsor sponsor)
        {
            ID = sponsor.ID;
            Name = sponsor.Name;
            City = sponsor.City;
            Email = sponsor.Email;
            Phone = sponsor.Phone;
            Sum = sponsor.Sum;
            Comment = sponsor.Comment;
            Url = sponsor.Url;
            Created = sponsor.Created;
            Confirmed = sponsor.Confirmed;
            ImageExt = sponsor.ImageExt;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Sum { get; set; }
        public string Comment { get; set; }
        public string Url { get; set; }
        public DateTime? Created { get; set; }
        public bool Confirmed { get; set; }
        public string ImageExt { get; set; }
    }
}