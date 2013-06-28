using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Core.EntityServices
{
    public class EventMediaEntityService : OrderedEntityService<EventMedia>
    {
        public override void Create(EventMedia obj)
        {
            AddValuesOnCreate(obj);

            base.Create(obj);
        }
    }
}