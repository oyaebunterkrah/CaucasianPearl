using System;
using System.Text;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Controllers
{
    public class FeedbackController : BaseController<Feedback, IBaseService<Feedback>>
    {
        public FeedbackController(IBaseService<Feedback> service) :
            base(service: service)
        {
        }

        [AllowAnonymous]
        public override ActionResult Create(Feedback obj)
        {
            obj.FeedbackDateTime = DateTime.Now;

            return base.Create(obj);
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        // Получение списка объектов.
        public override ActionResult Index()
        {
            var feedbacks = _service.Get(IsPageable);

            foreach (var feedback in feedbacks)
            {
                feedback.Comment = feedback.Comment.Length < 100
                                      ? feedback.Comment
                                      : feedback.Comment.Substring(0, 150) + "...";
                feedback.Suggestion = feedback.Suggestion.Length < 100
                                      ? feedback.Suggestion
                                      : feedback.Suggestion.Substring(0, 150) + "...";
            }

            return View(feedbacks);
        }

        // Перенаправление после редактирования объекта.
        protected override ActionResult OnEdited(Feedback feedback)
        {
            return Request.IsAjaxRequest()
                       ? new JsonResult
                           {
                               ContentEncoding = Encoding.UTF8,
                               Data = new
                                   {
                                       success = true,
                                       message = "Элемент успешно обновлён.",
                                       suggestion = feedback.Suggestion,
                                       comment = feedback.Comment,
                                       id = feedback.ID
                                   }
                           }
                       : base.OnEdited(feedback);
        }
    }
}
