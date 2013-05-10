using System;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;
using PagedList;

namespace CaucasianPearl.Controllers
{
    public class FeedbackController : BaseController<Feedback, IBaseService<Feedback>>
    {
        public FeedbackController(IBaseService<Feedback> service) :
            base(service: service)
        {
        }

        public FeedbackController() :
            this(DependencyResolverHelper<IBaseService<Feedback>>.GetService())
        {
        }

        // ¬ключаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        public override ActionResult Index()
        {
            var models = _service.Get(IsPageable);
            
            //var onePageOfProducts = models.ToPagedList(ControllerHelper.GetCurrentPageNumber(), 25);
            //ViewBag.OnePageOfProducts = onePageOfProducts;

            return View(models);
        }

        [AllowAnonymous]
        public override ActionResult Create(Feedback obj)
        {
            obj.FeedbackDateTime = DateTime.Now;

            return base.Create(obj);
        }
    }
}
