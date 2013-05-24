using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(ProfileMetadata))]
    public partial class Profile : IUrlFriendly
    {
        public string ImageUrl { get; set; }

        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}