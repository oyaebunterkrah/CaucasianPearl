using System;
using System.IO;
using System.Web.Mvc;

using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.HtmlHelpers;
using CaucasianPearl.Models.EDM;
using Resources;

namespace CaucasianPearl.Controllers
{
    public class SponsorController : BaseController<Sponsor, ISponsorService<Sponsor>>
    {
        public SponsorController(ISponsorService<Sponsor> service) :
            base(service: service)
        {
        }

        #region Properties

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        #endregion

        #region Actions

        // Обработка загрузки картинки.
        [HttpPost]
        public ActionResult UploadImage(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(Consts.Actions.Edit);

            // получаем объект, для которого загружаем картинку
            var sponsor = _service.Get(id);
            if (sponsor == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null && (int.Equals(Request.Files.Count, 0) || (Request.Files.Count > 0 && int.Equals(httpPostedFileBase.ContentLength, 0))))
            {
                ViewBag.ErrorMessage = ErrorRes.YouDidNotSelectAFile;
                if (HttpContext.Request.UrlReferrer != null)
                    ViewBag.RedirectedUrl = HttpContext.Request.UrlReferrer.AbsolutePath;

                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            try
            {
                var imageFile = Request.Files[0];

                if (imageFile != null)
                {
                    // определяем название и полный путь полноразмерной картинки и миниатюры
                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = id + extension;
                    var fileSavePath = Path.Combine(Server.MapPath(Url.Content(Consts.Paths.Img.EntityImgFolder)),
                        Consts.Controllers.Sponsor.SponsorImagesFolder,
                        fileName);

                    // если файлы с такими названиями уже имеются, удаляем их
                    if (System.IO.File.Exists(fileSavePath))
                        System.IO.File.Delete(fileSavePath);

                    // сохраняем полную картинку и миниатюру
                    imageFile.ResizeAndSave(Consts.Controllers.Sponsor.SponsorImagesHeight, Consts.Controllers.Sponsor.SponsorImagesWidth, fileSavePath);

                    // расширение файла записываем в базу данных в поле ImageExt
                    sponsor.ImageExt = extension;
                    _service.Update(sponsor);
                }
            }
            catch (Exception exception)
            {
                var errorMessage = exception.Message;
                if (exception.InnerException != null)
                    errorMessage = string.Format("SponsorController.UploadImage() exception. Exception Message: {0}. Inner Exception Message{1}", errorMessage, exception.InnerException.Message);
                ViewBag.ErrorMessage = errorMessage;
                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            return ReturnToObject(sponsor);

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

        #endregion
    }
}