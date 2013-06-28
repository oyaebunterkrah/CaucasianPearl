using System;
using System.IO;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.HtmlHelpers;
using CaucasianPearl.Models.EDM;
using Resources.Shared;

namespace CaucasianPearl.Controllers
{
    public class OneNewsController : UrlFriendlyController<OneNews, IUrlFriendlyService<OneNews>>
    {
        public OneNewsController(IUrlFriendlyService<OneNews> service) :
            base(service: service)
        {
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        // Обработка загрузки картинки
        [HttpPost]
        public ActionResult UploadImage(int oneNewsId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(Consts.Actions.Edit);

            // Получаем объект, для которого загружаем картинку
            var oneNews = _service.Get(oneNewsId);
            if (oneNews == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null && (int.Equals(Request.Files.Count, 0) || (Request.Files.Count > 0 && int.Equals(httpPostedFileBase.ContentLength, 0))))
            {
                ViewBag.ErrorMessage = SharedErrorRes.YouDidNotSelectAFile;
                if (HttpContext.Request.UrlReferrer != null)
                    ViewBag.RedirectedUrl = HttpContext.Request.UrlReferrer.AbsolutePath;

                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            try
            {
                var imageFile = Request.Files[0];

                if (imageFile != null)
                {
                    // Определяем название и полный путь полноразмерной картинки и миниатюры
                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = oneNewsId + extension;
                    var fileSavePath = Path.Combine(
                        Server.MapPath(Url.Content(Consts.FoldersPathes.EntityImagesFolder)),
                        Consts.Controllers.OneNews.OneNewsImagesFolder,
                        "/",
                        fileName);

                    // Если файлы с такими названиями уже имеются, удаляем их
                    if (System.IO.File.Exists(fileSavePath))
                        System.IO.File.Delete(fileSavePath);

                    // Сохраняем полную картинку и миниатюру
                    imageFile.ResizeAndSave(Consts.Controllers.OneNews.OneNewsImagesHeight, Consts.Controllers.OneNews.OneNewsImagesWidth, fileSavePath);
                    
                    // Расширение файла записываем в базу данных в поле ImageExt
                    oneNews.ImageExt = extension;
                    _service.Update(oneNews);
                }
            }
            catch (Exception exception)
            {
                var errorMessage = exception.Message;
                if (exception.InnerException != null)
                    errorMessage = string.Format("OneNewsController.UploadImage() exception. Exception Message: {0}. Inner Exception Message{1}", errorMessage, exception.InnerException.Message);
                ViewBag.ErrorMessage = errorMessage;
                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            return ReturnToObject(oneNews);

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
    }
}