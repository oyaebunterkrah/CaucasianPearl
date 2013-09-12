using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;

namespace CaucasianPearl.Controllers
{
    public class FeedbackController : BaseController<Feedback, IFeedbackService<Feedback>>
    {
        public FeedbackController(IFeedbackService<Feedback> service) :
            base(service: service)
        {
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable
        {
            get { return true; }
        }

        // Получение списка объектов.
        public override ActionResult Index()
        {
            var feedbacks = _service.Get(IsPageable);

            foreach (var feedback in feedbacks)
            {
                feedback.Comment = feedback.Comment.Length < 100
                                       ? feedback.Comment
                                       : feedback.Comment.Substring(0, 150) + "...";
            }

            return View(feedbacks);
        }

        protected override void ModifyValuesOnCreate(Feedback feedback)
        {
            feedback.Created = DateTime.Now;

            base.ModifyValuesOnCreate(feedback);
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
                                       comment = feedback.Comment,
                                       id = feedback.ID
                                   }
                           }
                       : base.OnEdited(feedback);
        }

        // Возвращает последний отзыв.
        public FeedbackItem GetLastFeedback()
        {
            return _service.GetLastFeedback();
        }

        // Возвращает последний отзыв.
        public JsonResult GetPreviousFeedback(int id)
        {
            return Json(_service.GetPreviousFeedback(id), JsonRequestBehavior.AllowGet);
        }

        // Возвращает последний отзыв.
        public JsonResult GetNextFeedback(int id)
        {
            return Json(_service.GetNextFeedback(id), JsonRequestBehavior.AllowGet);
        }
    }
}