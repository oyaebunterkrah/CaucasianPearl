using System;
using System.Linq;
using System.Web.Mvc;
using CaucasianPearl.Core.Helpers;
using Ninject;
using Recaptcha;

using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.EntityServices.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Models.EDM;

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
                request.Content = StringHelper.Substring(request.Content, 100);
                request.Comment = StringHelper.Substring(request.Comment, 100);
            }

            return View(requests);
        }

        [AllowAnonymous]
        public override ActionResult Create()
        {
            return base.Create();
        }
        
        [AllowAnonymous]
        public override ActionResult Create(Request obj)
        {
            return base.Create(obj);
        }

        [AllowAnonymous]
        public override ActionResult CreatePartial()
        {
            return base.CreatePartial();
        }

        [AllowAnonymous]
        [HttpPost, RecaptchaControlMvc.CaptchaValidator]
        public override ActionResult CreatePartialWithCaptcha(Request obj, bool captchaValid, string captchaErrorMessage)
        {
            return base.CreatePartialWithCaptcha(obj, captchaValid, captchaErrorMessage);
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

        protected override void ModifyValuesOnCreate(Request request)
        {
            request.Created = DateTime.Now;

            base.ModifyValuesOnCreate(request);
        }

        protected override void ModifyValuesOnDetails(Request request)
        {
            request.Content = StringHelper.Substring(request.Content, 100);
            request.Comment = StringHelper.Substring(request.Comment, 100);

            base.ModifyValuesOnDetails(request);
        }
    }
}