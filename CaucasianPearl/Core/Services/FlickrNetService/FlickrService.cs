using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;

using FlickrNet;
using Ninject;

using CaucasianPearl.Core.DAL.Data;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Properties;
using Resources;
using Photoset = CaucasianPearl.Models.Photoset;

namespace CaucasianPearl.Core.Services.FlickrNetService
{
    public class FlickrService : IFlickrService
    {
        #region Properties

        private static ILogService LogService
        {
            get { return ServiceHelper<ILogService>.GetService(); }
        }

        public bool IsPageable { get; set; }

        public int PerPage { get; set; }

        private static Flickr _flickr { get; set; }
        public Flickr Flickr
        {
            get
            {
                if (_flickr != null)
                    return _flickr;

                try
                {
                    LogService.Info(HostingEnvironment.MapPath(Settings.Default.FlickrCacheLocation));

                    Flickr.CacheLocation = HostingEnvironment.MapPath(Settings.Default.FlickrCacheLocation);

                    return _flickr = new Flickr(Settings.Default.FlickrKey,
                                      Settings.Default.FlickrSecret,
                                      Settings.Default.FlickrAuthToken);
                }
                catch (Exception exception)
                {
                    LogService.Error(exception);
                    throw new Exception("flickr is null");
                }
            }
        }

        /// <summary>
        /// Возвращает кол-во альбомов (Photosets).
        /// </summary>
        /// <returns>Количество альбомов.</returns>
        public int PhotosetsCount
        {
            get { return Flickr.PhotosetsGetList().Count; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// UploadPhoto by file.InputStream.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="tags"></param>
        /// <param name="isPublic"></param>
        /// <param name="contentType"></param>
        /// <param name="safetyLevel"></param>
        /// <param name="hiddenFromSearch"></param>
        /// <param name="isFamily"></param>
        /// <param name="isFriend"></param>
        /// <returns></returns>
        public string UploadPhoto(HttpPostedFile file, string fileName, string title, string description, string tags,
                                  bool isPublic, ContentType contentType, SafetyLevel safetyLevel,
                                  HiddenFromSearch hiddenFromSearch, bool isFamily = false, bool isFriend = false)
        {
            try
            {
                var photoId = Flickr.UploadPicture(file.InputStream,
                                                   fileName,
                                                   title,
                                                   description,
                                                   tags,
                                                   isPublic,
                                                   isFamily,
                                                   isFriend,
                                                   contentType,
                                                   safetyLevel,
                                                   hiddenFromSearch);

                return photoId;
            }
            catch (Exception exception)
            {
                LogService.Error(exception);
                return string.Empty;
            }
        }

        /// <summary>
        /// UploadPhoto by fileName.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="tags"></param>
        /// <param name="isPublic"></param>
        /// <param name="isFamily"></param>
        /// <param name="isFriend"></param>
        /// <returns></returns>
        public string UploadPhoto(string fileName, string title, string description, string tags, bool isPublic,
                                  bool isFamily, bool isFriend)
        {
            try
            {
                var photoId = Flickr.UploadPicture(fileName,
                                                   title,
                                                   description,
                                                   tags,
                                                   isPublic,
                                                   isFamily,
                                                   isFriend);

                return photoId;
            }
            catch (Exception exception)
            {
                LogService.Error(exception);
                return string.Empty;
            }
        }

        /// <summary>
        /// Создаёт фотоальбом.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public string CreatePhotoSet(string title, string description)
        {
            var sets = Flickr.PhotosetsGetList(Settings.Default.FlickrUserId);

            if (sets.Count == 0)
            {
                LogService.Warning("sets.Count = 0");
                throw new Exception("sets.Count = 0");
            }

            var primaryPhotoId = sets[0].PrimaryPhotoId;

            string photosetId;

            if (sets.CanCreate)
            {
                var set = Flickr.PhotosetsCreate(title, description, primaryPhotoId);
                set.OwnerId = Settings.Default.FlickrUserId;
                photosetId = set.PhotosetId;
            }
            else
                throw new Exception(ErrorRes.YouDoNotHaveCreatePermissionsOnTheFlickrSite);

            return photosetId;
        }

        /// <summary>
        /// Возвращает список объектов Photoset.
        /// </summary>
        /// <param name="lang"></param>
        /// <returns>Photoset list</returns>
        public List<Photoset> GetPhotosets(string lang)
        {
            var flickrPhotosets = IsPageable
                                      ? Flickr.PhotosetsGetList(Settings.Default.FlickrUserId,
                                                                 ControllerHelper.GetCurrentPageNumber(),
                                                                 PerPage)
                                      : Flickr.PhotosetsGetList(Settings.Default.FlickrUserId);

            var photosets = new List<Photoset>();

            foreach (var flickrPhotoset in flickrPhotosets)
            {
                var photoset = new Photoset
                {
                    PhotosetId = flickrPhotoset.PhotosetId,
                    Title = flickrPhotoset.Title,
                    Description = GetDescription(flickrPhotoset.Description, lang),
                    DateCreated = flickrPhotoset.DateCreated.ToString(CultureInfo.CurrentCulture),
                    FlickrUrl = flickrPhotoset.Url,
                    ThumbnailUrl = flickrPhotoset.PhotosetThumbnailUrl,
                    SmallUrl = flickrPhotoset.PhotosetSmallUrl,
                    Photos = GetPhotosetPhotos(flickrPhotoset.PhotosetId, lang)
                };
                
                photoset.PrimaryPhoto =
                    photoset.Photos.ToList().Find(photo => photo.PhotoId == flickrPhotoset.PrimaryPhotoId);

                photosets.Add(photoset);
            }

            return photosets;
        }

        /// <summary>
        /// Возвращает список фотоальбомов.
        /// </summary>
        /// <returns>PhotosetCollection</returns>
        public PhotosetCollection PhotosetsGetList()
        {
            return Flickr.PhotosetsGetList(Settings.Default.FlickrUserId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photosetId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public PhotosetPhotoCollection GetPhotosetPhotos(string photosetId, string lang)
        {
            var photos = Flickr.PhotosetsGetPhotos(photosetId, PhotoSearchExtras.Description);

            foreach (var photo in photos)
            {
                photo.Title = GetTitle(photo.Title, lang);
                photo.Description = GetDescription(photo.Description, lang);
            }

            return photos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="lang"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public PhotoCollection GetLastPhotos(int limit, string lang, int page)
        {
            var options = new PhotoSearchOptions
            {
                UserId = Settings.Default.FlickrUserId,
                SortOrder = PhotoSearchSortOrder.DatePostedDescending,
                MediaType = MediaType.Photos,
                Extras = PhotoSearchExtras.Description,
                PerPage = limit,
                Page = page
            };

            var photos = Flickr.PhotosSearch(options);

            foreach (var photo in photos)
            {
                photo.Title = GetTitle(photo.Title, lang);
                //replace with Description later, when major release with correction will be published
                photo.OriginalFormat = GetDescription(photo.Description, lang);
            }

            return photos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string GetTitle(string title, string lang)
        {
            if (string.IsNullOrEmpty(title))
                return string.Empty;

            var regTitle = new Regex(@"^([^\|].*)\s+\|\s+(.*)$");
            var match = regTitle.Match(title);

            return match.Success
                       ? match.Groups[lang == CultureHelper.CurrentCulture ? 2 : 1].Value.Trim()
                       : title;
        }

        /// <summary>
        /// Возвращает описание на указанном языке.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string GetDescription(string description, string lang)
        {
            description =
                Regex.Replace(description, Settings.Default.FooterToReplace, @"",
                              RegexOptions.IgnoreCase | RegexOptions.Multiline).Trim();
            if (string.IsNullOrEmpty(description))
                return string.Empty;

            var regexDescriptionEn = new Regex(@"description english(.*)description english", RegexOptions.Singleline);
            var regexDescriptionRu = new Regex(@"description russian(.*)description russian", RegexOptions.Singleline);
            var match = lang == CultureHelper.CurrentCulture
                            ? regexDescriptionRu.Match(description)
                            : regexDescriptionEn.Match(description);

            return match.Success ? match.Groups[1].Value.Trim() : description;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        /// <summary>
        /// Возвращает все размеры фотографии/видео.
        /// </summary>
        /// <param name="photoId">ID фотографии</param>
        public SizeCollection GetPhotoSizes(string photoId)
        {
            return Flickr.PhotosGetSizes(photoId);
        }

        /// <summary>
        /// Возвращает фотографии из альбома.
        /// </summary>
        /// <param name="photosetId"></param>
        /// <returns></returns>
        public IEnumerable<MediaItem> GetPhotosetPhotos(string photosetId)
        {
            var photos = Flickr.PhotosetsGetPhotos(photosetId).Select(p => new MediaItem(p, photosetId));

            return photos;
        }

        #endregion
    }
}