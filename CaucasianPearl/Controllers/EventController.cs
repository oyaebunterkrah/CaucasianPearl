﻿using CaucasianPearl.Core.Services.Logging;
using FlickrNet;
using Newtonsoft.Json;
using Resources.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.HtmlHelpers;
using CaucasianPearl.Core.Services.FlickrNet;
using CaucasianPearl.Models.EDM;
using CaucasianPearl.Properties;

namespace CaucasianPearl.Controllers
{
    public class EventController : UrlFriendlyController<Event, IUrlFriendlyService<Event>>
    {
        public EventController(IUrlFriendlyService<Event> service) :
            base(service: service)
        {
        }

        #region Properties

        private static ILogService LogService
        {
            get { return DependencyResolverHelper<ILogService>.GetService(); }
        }

        private static IFlickrService FlickrService
        {
            get
            {
                var flickrService = DependencyResolverHelper<IFlickrService>.GetService();
                flickrService.IsPageable = true;
                flickrService.PerPage = Consts.PaginatorControl.FlickrItemsPerPage;

                return flickrService;
            }
        }

        // Включаем постраничный вывод.
        protected override bool IsPageable { get { return true; } }

        #endregion

        #region Constants

        private const string MediaJsonKey = "mediaJson";

        #endregion

        #region Actions

        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public override ActionResult Create(Event @event)
        {
            //if (ModelState.IsValid)
            //{
            //    _service.Create(@event);

            //    return base.OnCreated(@event);
            //}

            if (!HttpContext.Request.Form.AllKeys.Contains(MediaJsonKey))
                RedirectToAction(Consts.Controllers.Error.Actions.Unexpected, Consts.Controllers.Error.Name);

            var mediaJson = HttpContext.Request.Form[MediaJsonKey];
            var photo = JsonHelper.Deserialize<List<FlickrObject>>(mediaJson);

            return View();
        }

        // Возвращает фотоальбомы.
        public string GetPhotoSets()
        {
            var photosets = FlickrService.PhotosetsGetList();

            return JsonConvert.SerializeObject(photosets);
        }

        // Возвращает фотографии из фотоальбома.
        public string GetPhotos(string photosetId)
        {
            var photos = FlickrService.GetPhotosetPhotos(photosetId);

            return JsonConvert.SerializeObject(photos);
        }

        // Обработка загрузки картинки
        [HttpPost]
        public ActionResult UploadImage(int eventId)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(Consts.Actions.Edit);

            // Получаем объект, для которого загружаем картинку
            var @event = _service.Get(eventId);
            if (@event == null)
                return View(Consts.Controllers.Error.Views.NotFound);

            var httpPostedFileBase = Request.Files[0];
            if (httpPostedFileBase != null &&
                (Equals(Request.Files.Count, 0) ||
                 (Request.Files.Count > 0 && Equals(httpPostedFileBase.ContentLength, 0))))
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
                    var fileName = eventId + extension;
                    var fileSavePath = Path.Combine(
                        Server.MapPath(Url.Content(Consts.EntityImagesFolder)),
                        Consts.Controllers.Event.EventImagesFolder,
                        "/",
                        fileName);

                    // Если файлы с такими названиями уже имеются, удаляем их
                    if (System.IO.File.Exists(fileSavePath))
                        System.IO.File.Delete(fileSavePath);

                    // Сохраняем полную картинку и миниатюру
                    imageFile.ResizeAndSave(maxHeight: Consts.Controllers.Event.EventImagesHeight,
                                            maxWidth: Consts.Controllers.Event.EventImagesWidth, strSavePath: fileSavePath);

                    // Расширение файла записываем в базу данных в поле ImageExt
                    //@event.ImageExt = extension;
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
                return View(Consts.Controllers.Error.Views.Unexpected);
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
        
        #endregion
    }
}