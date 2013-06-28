using System.ComponentModel.DataAnnotations;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Models.Metadata;

namespace CaucasianPearl.Models.EDM
{
    public partial class EventMedia : IOrdered
    {
        bool IBase.CanBeDeleted()
        {
            return true;
        }
    }
}