using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(FeedbackMetadata))]
    public partial class Feedback : IBase
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}
