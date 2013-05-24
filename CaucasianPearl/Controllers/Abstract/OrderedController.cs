using System.Web.Mvc;
using CaucasianPearl.Controllers.Interface;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.EntityServices.Interface;

namespace CaucasianPearl.Controllers.Abstract
{
    /// <summary>
    /// OrderedController
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    /// <typeparam name="S">service</typeparam>
    public abstract class OrderedController<T, S> : BaseController<T, S>,  IOrderedController
        where T : class, IOrdered, new()
        where S : IOrderedService<T>
    {
        protected OrderedController(S service) : base(service) { }

        #region Actions

        public override ActionResult Create(T obj)
        {
            AddValuesOnCreate(obj);

            return base.Create(obj);
        }

        // Перемещение объекта вверх.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public ActionResult Up(int id)
        {
            var obj = _service.Get(id);

            if (obj == null)
                return View(Consts.Controllers.Error.Views.NotFound);
            
            if (obj.Sequence.HasValue)
            {
                var next = _service.GetNext(obj);

                if (next != null)
                {
                    var curSeq = obj.Sequence;
                    var nextSeq = next.Sequence;

                    obj.Sequence = nextSeq;
                    _service.Update(obj);

                    next.Sequence = curSeq;
                    _service.Update(next);
                }
            }

            return OnUp(obj);
        }

        // Перемещение объекта вниз.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public ActionResult Down(int id)
        {
            var obj = _service.Get(id);

            if (obj == null) return View(Consts.Controllers.Error.Views.NotFound);
            
            if (obj.Sequence.HasValue)
            {
                var previous = _service.GetPrevious(obj);

                if (previous != null)
                {
                    var curSeq = obj.Sequence;
                    var prevSeq = previous.Sequence;

                    obj.Sequence = prevSeq;
                    _service.Update(obj);

                    previous.Sequence = curSeq;
                    _service.Update(previous);
                }
            }

            return OnDown(obj);
        }

        #endregion

        #region Virtual methods

        // Перенаправление после перемещения объекта вверх.
        protected virtual ActionResult OnUp(T obj)
        {
            return ReturnToList(obj);
        }

        // Перенаправление после перемещения объекта вниз.
        protected virtual ActionResult OnDown(T obj)
        {
            return ReturnToList(obj);
        }

        #endregion

        #region Overridden virtual methods

        // При создании объекта автоматически задаём ему значение Sequence, равное следующему после наибольшего.
        protected override void AddValuesOnCreate(T obj)
        {
            base.AddValuesOnCreate(obj);

            var maxSequence = _service.GetMaxSequence();
            obj.Sequence = maxSequence.HasValue ? maxSequence.Value + 1 : 1;
        }

        #endregion
    }
}