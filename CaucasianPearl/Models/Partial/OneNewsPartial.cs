using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(OneNewsMetadata))]
    public partial class OneNews : IUrlFriendly
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}
