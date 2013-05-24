using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(ContentBlockMetadata))]
    public partial class ContentBlock : IOrdered
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}