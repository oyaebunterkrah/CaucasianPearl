using System.Collections.Generic;
using System.Web;
using FlickrNet;
using Photoset = CaucasianPearl.Models.Photoset;

namespace CaucasianPearl.Core.Services.FlickrNetService
{
    public interface IFlickrService
    {
        Flickr Flickr { get; }

        bool IsPageable { get; set; }

        int PerPage { get; set; }

        int PhotosetsCount { get; }

        string UploadPhoto(HttpPostedFile file,
                           string fileName,
                           string title,
                           string description,
                           string tags,
                           bool isPublic,
                           ContentType contentType,
                           SafetyLevel safetyLevel,
                           HiddenFromSearch hiddenFromSearch,
                           bool isFamily = false,
                           bool isFriend = false);

        string UploadPhoto(string fileName,
                           string title, string description,
                           string tags, bool isPublic,
                           bool isFamily,
                           bool isFriend);

        string CreatePhotoSet(string title, string description);

        List<Photoset> GetPhotosets(string lang);

        PhotosetCollection PhotosetsGetList();

        PhotosetPhotoCollection GetPhotosetPhotos(string photosetId, string lang);

        IEnumerable<FlickrObject> GetPhotosetPhotos(string photosetId);

        PhotoCollection GetLastPhotos(int limit, string lang, int page);

        string GetTitle(string title, string language);

        string GetDescription(string description, string lang);

        SizeCollection GetPhotoSizes(string photoId);
    }
}