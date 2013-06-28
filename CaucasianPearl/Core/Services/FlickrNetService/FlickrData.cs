using FlickrNet;
using System.Linq;
using CaucasianPearl.Core.Helpers;

namespace CaucasianPearl.Core.Services.FlickrNetService
{
    public class FlickrData
    {
    }

    public class FlickrMedia
    {
    }

    public class FlickrObject
    {
        private static IFlickrService FlickrService
        {
            get { return DependencyResolverHelper<IFlickrService>.GetService(); }
        }

        public FlickrObject()
        {
        }

        public FlickrObject(Photo photo, string photosetId)
        {
            PhotoId = photo.PhotoId;
            PhotosetId = photosetId;
            Description = string.Empty;
            Content = string.Empty;
            MediaType = FlickrNet.MediaType.Photos.ToString().ToLowerInvariant();
            FlickrUrl = photo.WebUrl;
            ThumbnailUrl = photo.ThumbnailUrl;
            SmallUrl = photo.SmallUrl;
            MediumUrl = photo.MediumUrl;
            LargeUrl = photo.LargeUrl;
            VideoUrl = string.Empty;
            IsPrimary = false;

            var videoSizes = FlickrService.GetPhotoSizes(photo.PhotoId).Where(ps => ps.MediaType == FlickrNet.MediaType.Videos).ToList();
            if (videoSizes.Count > 0)
            {
                MediaType = FlickrNet.MediaType.Videos.ToString().ToLowerInvariant();
                var videoSize = videoSizes.FirstOrDefault(vs => vs.Label == "Video Player");
                if (videoSize != null)
                    VideoUrl = videoSize.Source;
            }
        }

        public string PhotoId;
        public string PhotosetId;
        public string Description;
        public string Content;
        public string MediaType;
        public string FlickrUrl;
        public string ThumbnailUrl;
        public string SmallUrl;
        public string MediumUrl;
        public string LargeUrl;
        public string VideoUrl;
        public bool? IsPrimary;
    }
}