using System;
using System.Collections.Generic;
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
    public class SponsorEntityService : BaseEntityService<Sponsor>, ISponsorService<Sponsor>
    {
        // Количество объектов на одной странице.
        protected override int LinksPerPage { get { return Consts.PaginatorControl.SponsorItemsPerPage; } }

        // Количество отображаемых страниц перед многоточием.
        protected override int NumberOfVisibleLinks { get { return Consts.PaginatorControl.SponsorNumberOfVisibleLinks; } }

        #region Methods

        public IEnumerable<SponsorItem> GetSponsors(int count, bool isBig)
        {
            Func<Sponsor, bool> condition = s => isBig
                ? !string.IsNullOrWhiteSpace(s.ImageExt)
                : string.IsNullOrWhiteSpace(s.ImageExt);

            var sponsorItems = Get()
                .Where(condition)
                .OrderBy(s => s.Created)
                .Take(count).ToList()
                .Select(s => new SponsorItem(s));

            return sponsorItems;
        }

        #endregion
    }
}