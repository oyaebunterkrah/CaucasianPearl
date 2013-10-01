using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.HtmlHelpers;
using CaucasianPearl.Models.EDM;
using CaucasianPearl.Resources;

namespace CaucasianPearl.Controllers
{
    public class SiteSettingsController : BaseController<SiteSetting, IBaseService<SiteSetting>>
    {
        public SiteSettingsController(IBaseService<SiteSetting> service) :
            base(service: service)
        {
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        public ActionResult GetCoverImages()
        {
            var path = Server.MapPath(Consts.Paths.Img.CoversFolder);
            ViewBag.Images = Directory.GetFiles(path, "*.*")
                                     .Select(Path.GetFileName)
                                     .ToArray();

            ViewBag.CoverImageName = SiteSettingsHelper.GetSiteSettingValueAsString(Consts.SiteSettings.CoverImageName);

            return View();
        }

        // Обработка загрузки обложки.
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public ActionResult UploadCoverImage()
        {
            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null &&
                (Request.Files.Count == 0 ||
                 (Request.Files.Count > 0 && httpPostedFileBase.ContentLength == 0)))
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
                    // Определяем название и полный путь полноразмерной картинки и миниатюры
                    var extension = Path.GetExtension(imageFile.FileName);
                    var fileName = DateTime.Now.Ticks + extension;
                    var fileSavePath = Path.Combine(Server.MapPath(Url.Content(Consts.Paths.Img.CoversFolder)), fileName);

                    // Если файлы с такими названиями уже имеются, удаляем их
                    if (System.IO.File.Exists(fileSavePath))
                        System.IO.File.Delete(fileSavePath);

                    // Сохраняем полную картинку и миниатюру
                    imageFile.ResizeAndSave(maxHeight: 600,
                                            maxWidth: 0, strSavePath: fileSavePath);

                    var path = Server.MapPath(Consts.Paths.Img.CoversFolder);
                    if (Directory.GetFiles(path, "*.*").Length > 4)
                    {
                        var fileInfo = new DirectoryInfo(path)
                            .GetFileSystemInfos()
                            .OrderBy(fi => fi.CreationTime).FirstOrDefault();
                        if (fileInfo != null && fileInfo.Exists)
                            fileInfo.Delete();
                    }
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
                return View(Consts.Controllers.Error.Views.Unexpected);
            }

            return RedirectToAction(Consts.Controllers.SiteSettings.Actions.GetCoverImages);
        }

        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public void DeleteCoverImage(string imageName)
        {
            var path = Server.MapPath(Consts.Paths.Img.CoversFolder);
            var fileInfo = new DirectoryInfo(path).GetFileSystemInfos().FirstOrDefault(fi => fi.Name == imageName);
            
            if (fileInfo != null && fileInfo.Exists)
                fileInfo.Delete();
        }

        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public void SetCoverImage(string imageName)
        {
            var coverImageName = SiteSettingsHelper.GetSiteSetting(Consts.SiteSettings.CoverImageName);

            if (coverImageName != null && coverImageName.Value != imageName)
            {
                coverImageName.Value = imageName;
                SiteSettingsHelper.SiteSettingsService.Update(coverImageName);
            }
        }
    }
}