using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(EventMetadata))]
    public partial class Event : IUrlFriendly
    {
        bool IBase.CanBeDeleted()
        {
            return EventMedia.Count == 0;
        }
    }
}
