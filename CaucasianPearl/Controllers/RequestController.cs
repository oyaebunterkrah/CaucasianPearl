using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;
using Resources.Request;

namespace CaucasianPearl.Controllers
{
    public class RequestController : BaseController<Request, IBaseService<Request>>
    {
        public RequestController(IBaseService<Request> service) :
            base(service: service)
        {
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        // Получение списка объектов.
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public override ActionResult Index()
        {
            var requests = _service.Get(IsPageable);

            foreach (var request in requests)
            {
                request.Content = request.Content.Length < 100
                                      ? request.Content
                                      : request.Content.Substring(0, 150) + "...";
                request.Comment = request.Comment.Length < 100
                                      ? request.Comment
                                      : request.Comment.Substring(0, 150) + "...";
            }

            return View(requests);
        }

        [AllowAnonymous]
        public override ActionResult Create()
        {
            return View();
        }
        
        [AllowAnonymous]
        public override ActionResult Create(Request obj)
        {
            obj.RequestDateTime = DateTime.Now;

            return base.Create(obj);
        }

        [AllowAnonymous]
        public ActionResult Thanks()
        {
            return View();
        }

        // Перенаправление после создания объекта.
        [AllowAnonymous]
        protected override ActionResult OnCreated(Request model)
        {
            return RedirectToAction(Consts.Controllers.Request.Actions.Thanks);
        }
    }
}