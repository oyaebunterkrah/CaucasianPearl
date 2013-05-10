using CaucasianPearl.Core.DAL.Interface;

namespace CaucasianPearl.Models.Interface
{
    public interface IOrdered : IBase
    {
        int? Sequence { get; set; }
    }
}