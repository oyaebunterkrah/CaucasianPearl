using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(SponsorMetadata))]
    public partial class Sponsor : IBase
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}
