namespace CaucasianPearl.Core.DAL.Interface
{
    public interface IOrdered : IBase
    {
        int? Sequence { get; set; }
    }
}