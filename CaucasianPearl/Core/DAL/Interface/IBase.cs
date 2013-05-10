namespace CaucasianPearl.Core.DAL.Interface
{
    public interface IBase
    {
        int ID { get; set; }
        bool CanBeDeleted();
    }
}
