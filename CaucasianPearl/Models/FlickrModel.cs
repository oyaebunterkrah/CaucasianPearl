using System.Collections.Generic;
using CaucasianPearl.Core.DAL.Interface;
using FlickrNet;

namespace CaucasianPearl.Models
{
    public class Photoset
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public string PhotosetId { get; set; }
        public Photo PrimaryPhoto;
        public PhotosetPhotoCollection Photos;
    }

    public class FlickrModel
    {
        public List<Photoset> Photosets;
        public Photo CurrentPhoto;
        public string PhotosetId;
        public List<Photo> LastPhotos;
        public string Language;
        public int IndexOfCurrentPhoto;
        public string UserName;
        public string UserID;
        public string Email;
        public string PhotosJson;
        public string SiteUrl;
    }

    public class PhotoJson
    {
        public string thumb { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string url { get; set; }
    }
}