using System.Collections.Generic;
using System.Web;
using FlickrNet;
using Photoset = CaucasianPearl.Models.Photoset;

namespace CaucasianPearl.Core.Services.FlickrNet
{
    public interface IFlickrService
    {
        Flickr Flickr { get; }

        bool IsPageble { get; set; }

        int PerPage { get; set; }

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

        int PhotosetsCount();

        PhotosetPhotoCollection GetPhotosetPhotos(string photosetId, string lang);

        PhotoCollection GetLastPhotos(int limit, string lang, int page);

        string GetTitle(string title, string language);

        string GetDescription(string description, string lang);
    }
}