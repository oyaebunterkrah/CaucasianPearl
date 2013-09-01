using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(SiteSettingsMetadata))]
    public partial class SiteSetting : IBase
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}