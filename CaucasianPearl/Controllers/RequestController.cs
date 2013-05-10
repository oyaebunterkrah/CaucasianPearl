using System;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Controllers
{
    public class RequestController : BaseController<Request, IBaseService<Request>>
    {
        public RequestController(IBaseService<Request> service) :
            base(service: service)
        {
        }

        public RequestController() :
            this(DependencyResolverHelper<IBaseService<Request>>.GetService())
        {
        }

        // ¬ключаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        public override ActionResult Index()
        {
            var models = _service.Get(IsPageable);

            return View(models);
        }

        [AllowAnonymous]
        public override ActionResult Create(Request obj)
        {
            obj.RequestDateTime = DateTime.Now;

            return base.Create(obj);
        }

        public override ActionResult Details(int id)
        {
            return base.Details(id);
        }
    }
}