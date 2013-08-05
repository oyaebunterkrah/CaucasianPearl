using System.Collections.ObjectModel;
using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.Services.LoggingService;
using Newtonsoft.Json;
using Resources.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CaucasianPearl.Controllers.Abstract;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Helpers.HtmlHelpers;
using CaucasianPearl.Core.Services.FlickrNetService;
using CaucasianPearl.Models.EDM;
using WebMatrix.WebData;

namespace CaucasianPearl.Controllers
{
    public class EventController : UrlFriendlyController<Event, IEventService<Event>>
    {
        public EventController(IEventService<Event> service) :
            base(service: service)
        {
            WebSecurity.Login("admin", "Eekoogh4", true);
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

        private const string MediaItemsForDeleteJsonKey = "mediaItemsForDelete";
        private const string MediaItemsForEditJsonKey = "mediaItemsForEdit";
        private const string MediaItemsForCreateJsonKey = "mediaItemsForCreate";

        #endregion

        #region Actions

        public override ActionResult Index()
        {
            var models = _service.Get(IsPageable).OrderByDescending(e => e.EventDate);

            foreach (var model in models)
                ModifyValuesOnDetails(model);

            return View(models);
        }

        // Обработка формы создания события.
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public override ActionResult Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _service.Create(@event);

                var createdEvent = _service.Get(@event.ID);

                CreateEventMedia(createdEvent.ID);

                return base.OnCreated(createdEvent);
            }

            return View();
        }

        // Обработка формы удаления события.
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public override ActionResult Delete(int id, FormCollection formCollection)
        {
            var @event = _service.Get(id);
            var eventMediaIdList = @event.EventMedia.Select(em => em.ID).ToList();

            var onDeleted = OnDeleted(@event);
            base.Delete(id, null);

            // удаляем медиа файлы из базы
            if (eventMediaIdList.Count > 0)
            {
                var orderedService = DependencyResolverHelper<IOrderedService<EventMedia>>.GetService();
                foreach (var eventMediaId in eventMediaIdList)
                    orderedService.Delete(eventMediaId);
            }

            return onDeleted;
        }

        // Обработка формы редактирования события.
        [HttpPost]
        [Authorize(Roles = Consts.Roles.AdminContentManager)]
        public override ActionResult Edit(int id, Event @event)
        {
            DeleteEventMedia(id);
            EditEventMedia(id);
            CreateEventMedia(id);

            return base.Edit(id, @event);
        }

        // Возвращает фотографии из фотоальбома.
        public ActionResult SelectMediaItemsOnCreate()
        {
            return View();
        }

        // Возвращает фотографии из фотоальбома. Ппри редактировании.
        public ActionResult SelectMediaItemsOnEdit(Event @event)
        {
            return View(@event);
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
                        Server.MapPath(Url.Content(Consts.FoldersPathes.EntityImagesFolder)),
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
                    _service.Update(obj: @event);
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

        // Возвращает список событий.
        // 2 события, которые были до и после
        // 1* 2* 3* 4*
        // если 1, то внизу отображается 2,3,4,5
        // если 2, то внизу отображается 1,3,4,5
        // если 4, то внизу отображается 2,3,5,6
        public ActionResult GetEvents()
        {
            var events = GetNeighborElements();

            return View(events);
        }
        
        #endregion

        #region Helpers

        private void DeleteEventMedia(int eventId)
        {
            try
            {
                var mediaItemsForDelete = GetEventMediaItemsByKey(MediaItemsForDeleteJsonKey).Select(d => d.PhotoId);
                if (!mediaItemsForDelete.Any())
                    return;

                var currentEvent = _service.Get(eventId);
                if (!currentEvent.EventMedia.Any())
                    return;

                // список ID медиа файлов для удаления
                var eventMediaItemsForDeleteList = new List<int>();

                foreach (var mediaItemForDelete in mediaItemsForDelete)
                {
                    var eventMediaItemForDelete =
                        currentEvent.EventMedia.FirstOrDefault(em => mediaItemForDelete.Contains(em.PhotoId));

                    if (eventMediaItemForDelete != null)
                    {
                        eventMediaItemsForDeleteList.Add(eventMediaItemForDelete.ID);

                        currentEvent.EventMedia.Remove(eventMediaItemForDelete);
                    }
                }

                // устанавливаем главное фото, если не выбрано ни одного
                if (currentEvent.EventMedia.Count != 0)
                    if (currentEvent.EventMedia.All(em => !(em.IsPrimary ?? false)))
                    {
                        var firstEventMedia = currentEvent.EventMedia.FirstOrDefault();
                        if (firstEventMedia != null)
                            firstEventMedia.IsPrimary = true;
                    }

                _service.Update(currentEvent);

                // удаляем медиа файлы из базы
                if (eventMediaItemsForDeleteList.Count > 0)
                {
                    var orderedService = DependencyResolverHelper<IOrderedService<EventMedia>>.GetService();
                    foreach (var eventMediaItemId in eventMediaItemsForDeleteList)
                        orderedService.Delete(eventMediaItemId);
                }
            }
            catch (Exception exeption)
            {
                LogService.Error(exeption);
                RedirectToAction(Consts.Controllers.Error.Actions.Unexpected, Consts.Controllers.Error.Name);
            }
        }

        private void EditEventMedia(int eventId)
        {
            try
            {
                var mediaItemsForEdit = GetEventMediaItemsByKey(MediaItemsForEditJsonKey);
                if (!mediaItemsForEdit.Any())
                    return;

                var oldEvent = _service.Get(eventId);
                if (!oldEvent.EventMedia.Any())
                    return;

                // делаем все медиа элементы не главными
                foreach (var eventMedia in oldEvent.EventMedia)
                    eventMedia.IsPrimary = false;

                foreach (var mediaItemForEdit in mediaItemsForEdit)
                {
                    // получаем соответствующий медиа элемент
                    var oldEventMediaItemForEdit =
                        oldEvent.EventMedia.FirstOrDefault(em => em.PhotoId == mediaItemForEdit.PhotoId);

                    if (oldEventMediaItemForEdit != null)
                    {
                        oldEventMediaItemForEdit.IsPrimary = mediaItemForEdit.IsPrimary;
                        oldEventMediaItemForEdit.Description = mediaItemForEdit.Description;
                        oldEventMediaItemForEdit.Content = mediaItemForEdit.Content;
                    }
                }

                _service.Update(oldEvent);
            }
            catch (Exception exeption)
            {
                LogService.Error(exeption);
                RedirectToAction(Consts.Controllers.Error.Actions.Unexpected, Consts.Controllers.Error.Name);
            }
        }

        private void CreateEventMedia(int eventId)
        {
            var selectedMediaItems = GetEventMediaItemsByKey(MediaItemsForCreateJsonKey);
            if (selectedMediaItems.Count == 0)
                return;

            var eventMedia = selectedMediaItems.Select(smi => new EventMedia
            {
                EventId = eventId,
                PhotoId = smi.PhotoId,
                PhotosetId = smi.PhotosetId,
                Description = smi.Description,
                Content = smi.Content,
                MediaType = smi.MediaType,
                FlickrUrl = smi.FlickrUrl,
                ThumbnailUrl = smi.ThumbnailUrl,
                SmallUrl = smi.SmallUrl,
                MediumUrl = smi.MediumUrl,
                LargeUrl = smi.LargeUrl,
                VideoUrl = smi.VideoUrl,
                IsPrimary = smi.IsPrimary
            }).ToList();

            var orderedService = DependencyResolverHelper<IOrderedService<EventMedia>>.GetService();
            eventMedia.ForEach(orderedService.Create);
        }

        // Возвращает фотоальбомы.
        public string GetPhotoSets()
        {
            var photosets = FlickrService.GetPhotosets(CultureHelper.CurrentCulture);

            return JsonConvert.SerializeObject(photosets);
        }

        // Возвращает фотографии из фотоальбома. Ппри создании.
        public string GetPhotos(string photosetId)
        {
            var photos = FlickrService.GetPhotosetPhotos(photosetId);

            return JsonConvert.SerializeObject(photos);
        }

        private List<MediaItem> GetEventMediaItemsByKey(string key)
        {
            if (!HttpContext.Request.Form.AllKeys.Contains(key))
                RedirectToAction(Consts.Controllers.Error.Actions.Unexpected, Consts.Controllers.Error.Name);

            var mediaItemsJson = HttpContext.Request.Form[key];

            if (string.IsNullOrWhiteSpace(mediaItemsJson))
                RedirectToAction(Consts.Controllers.Error.Actions.Unexpected, Consts.Controllers.Error.Name);

            return JsonHelper.Deserialize<List<MediaItem>>(mediaItemsJson);
        }

        // Возвращает список событий.
        public IEnumerable<EventItem> GetNeighborElements()
        {
            var events = _service.GetNeighborElements();

            return events;
        }

        // Возвращает список событий по отношению к указанному.
        public string GetLastEventsById(int eventId)
        {
            var eventsJson = _service.GetNeighborElements(eventId);

            return eventsJson;
        }

        #endregion
    }
}