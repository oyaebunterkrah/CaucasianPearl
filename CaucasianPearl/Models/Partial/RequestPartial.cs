using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    [MetadataType(typeof(RequestMetadata))]
    public partial class Request : IBase
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}
