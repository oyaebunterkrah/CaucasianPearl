using System;
using System.IO;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Extensions;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Models.EDM;
using Resources.Shared;

namespace CaucasianPearl.Controllers
{
    public class EventController : UrlFriendlyController<Event, IUrlFriendlyService<Event>>
    {
        public EventController(IUrlFriendlyService<Event> service) :
            base(service: service)
        {
        }

        public EventController() :
            this(DependencyResolverHelper<IUrlFriendlyService<Event>>.GetService())
        {
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        // Обработка загрузки картинки
        [HttpPost]
        public ActionResult UploadImage(int eventId)
        {
            // Получаем объект, для которого загружаем картинку
            var @event = _service.Get(eventId);
            if (@event == null)
                return View(viewName: Consts.Views.NotFound);

            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null &&
                (Equals(Request.Files.Count, 0) ||
                 (Request.Files.Count > 0 && Equals(httpPostedFileBase.ContentLength, 0))))
            {
                ViewBag.ErrorMessage = SharedErrorRes.YouDidNotSelectAFile;
                if (HttpContext.Request.UrlReferrer != null)
                    ViewBag.RedirectedUrl = HttpContext.Request.UrlReferrer.AbsolutePath;

                return View(viewName: Consts.Views.Error);
            }

            try
            {
                var imageFile = Request.Files[0];

                if (imageFile != null)
                {
                    // Определяем название и полный путь полноразмерной картинки и миниатюры
                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = eventId + extension;
                    var fileSavePath = Path.Combine(Server.MapPath(Url.Content(Consts.EntityImagesFolder)),
                                                       Consts.Controllers.Event.EventImagesFolder, fileName);

                    // Если файлы с такими названиями уже имеются, удаляем их
                    if (System.IO.File.Exists(fileSavePath))
                        System.IO.File.Delete(fileSavePath);

                    // Сохраняем полную картинку и миниатюру
                    imageFile.ResizeAndSave(maxHeight: Consts.Controllers.Event.EventImagesHeight,
                                            maxWidth: Consts.Controllers.Event.EventImagesWidth, strSavePath: fileSavePath);

                    // Расширение файла записываем в базу данных в поле ImageExt
                    @event.ImageExt = extension;
                    _service.Update(dataObject: @event);
                }
            }
            catch (Exception exception)
            {
                var errorMessage = exception.Message;
                if (exception.InnerException != null)
                    errorMessage =
                        string.Format(
                            "EventController.UploadImage() exception. Exception Message: {0}. Inner Exception Message{1}",
                            errorMessage, exception.InnerException.Message);
                ViewBag.ErrorMessage = errorMessage;
                return View(viewName: Consts.Views.Error);
            }

            return ReturnToObject(@event);

            /*загрузка chunk'ами {
            var fileUpload = Request.Files[0];
            var uploadPath = Server.MapPath("~/App_Data");
            chunk = chunk ?? 0;
            using (var fs = new FileStream(Path.Combine(uploadPath, name), chunk == 0 ? FileMode.Create : FileMode.Append))
            {
                var buffer = new byte[fileUpload.InputStream.Length];
                fileUpload.InputStream.Read(buffer, 0, buffer.Length);
                fs.Write(buffer, 0, buffer.Length);
            }
            return Json(new { message = "chunk uploaded", name = name }); }*/
        }

        public override ActionResult Create(Event obj)
        {
            AddValuesOnCreate(obj);

            return base.Create(obj);
        }
    }
}