using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Filters;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Services.FlickrNetService;
using CaucasianPearl.Models;
using CaucasianPearl.Properties;
using FlickrNet;

namespace CaucasianPearl.Controllers
{
    public class GalleryController : Controller
    {
        public GalleryController(IFlickrService flickrService)
        {
            flickrService.IsPageable = true;
            flickrService.PerPage = Consts.PaginatorControl.FlickrItemsPerPage;
            
            _flickrService = flickrService;
        }

        private IFlickrService _flickrService { get; set; }

        [LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult Index(string language)
        {
            var flickrModel = new FlickrModel { Photosets = _flickrService.GetPhotosets(language) };

            return View(flickrModel);
        }

        // TODO
        public ActionResult CreatePhotoSet()
        {
            var photosetId = _flickrService.CreatePhotoSet("test1", "description1");
            var photoId = _flickrService.UploadPhoto("c:\\Users\\днс\\Desktop\\test1.jpg", "title", "description", "", false, false,
                                     false);
            _flickrService.Flickr.PhotosetsAddPhoto(photosetId, photoId);

            return RedirectToAction(Consts.Actions.Index);
        }

        [LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult Photo(string id)
        {
            var flickrModel = new FlickrModel
            {
                UserName = Settings.Default.UserName,
                SiteUrl = Settings.Default.SiteURL
            };

            List<Photo> galleryPhotos;
            var page = 0;

            do
            {
                page++;
                galleryPhotos = _flickrService.GetLastPhotos(100, CultureHelper.CurrentCulture, page).ToList();
                if (galleryPhotos.Count == 0)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                flickrModel.IndexOfCurrentPhoto = galleryPhotos.FindIndex(ph => ph.PhotoId == id);
            } while (flickrModel.IndexOfCurrentPhoto == -1);

            flickrModel.CurrentPhoto = galleryPhotos[flickrModel.IndexOfCurrentPhoto];
            flickrModel.PhotosJson = GetJson(galleryPhotos, null);

            return View(Consts.Actions.Index, flickrModel);
        }

        private string GetJson(IEnumerable<Photo> photos, string p)
        {
            var json = photos.Select(photo => new PhotoJson
                {
                    thumb = photo.ThumbnailUrl,
                    image = photo.LargeUrl,
                    title = photo.Title,
                    description =
                        !String.IsNullOrEmpty(photo.OriginalFormat) && photo.Title != photo.OriginalFormat
                            ? photo.OriginalFormat
                            : string.Empty,
                    //link = photo.WebUrl,
                    url =
                        String.IsNullOrEmpty(p)
                            ? Settings.Default.SiteURL +
                              Url.Action(Consts.Controllers.Gallery.Actions.Photo,
                                         new {controller = "gallery", id = photo.PhotoId})
                            : Settings.Default.SiteURL +
                              Url.Action(Consts.Controllers.Gallery.Actions.Preview,
                                         new {controller = "gallery", id = photo.PhotoId, photoset = p})
                }).ToList();

            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(json);
        }

        [LocalizedCache(Duration = Consts.OutputCacheDuration)]
        public ActionResult Preview(string photoId, string photosetId)
        {
            var flickrModel = new FlickrModel();

            List<Photo> galleryPhotos;

            try
            {
                galleryPhotos = _flickrService.GetPhotosetPhotos(photosetId, CultureHelper.CurrentCulture).ToList();
            }
            catch
            {
                Response.StatusCode = 404;
                return null;
            }

            flickrModel.IndexOfCurrentPhoto = galleryPhotos.FindIndex(ph => ph.PhotoId == photoId);
            
            if (galleryPhotos.Count == 0 || galleryPhotos[0].UserId != Settings.Default.FlickrUserId ||
                flickrModel.IndexOfCurrentPhoto == -1)
            {
                Response.StatusCode = 404;
                return null;
            }

            flickrModel.CurrentPhoto = galleryPhotos[flickrModel.IndexOfCurrentPhoto];
            flickrModel.PhotosetId = photosetId;
            flickrModel.PhotosJson = GetJson(galleryPhotos, flickrModel.PhotosetId);

            return View(flickrModel);
        }
    }
}