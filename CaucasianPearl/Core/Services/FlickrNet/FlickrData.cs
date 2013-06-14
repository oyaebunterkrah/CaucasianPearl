using CaucasianPearl.Core.Constants;
using FlickrNet;
using Ninject;
using Resources.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using CaucasianPearl.Core.Helpers;
using CaucasianPearl.Core.Services.FlickrNet;
using CaucasianPearl.Core.Services.Logging;
using CaucasianPearl.Properties;

namespace CaucasianPearl.Core.Services.FlickrNet
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

        public FlickrObject(Photo photo)
        {
            photoid = photo.PhotoId;
            thumbnailurl = photo.ThumbnailUrl;
            mediumurl = photo.MediumUrl;
            largeurl = photo.LargeUrl;
            videourl = string.Empty;
            mediatype = MediaType.Photos.ToString().ToLowerInvariant();

            var videoSizes = FlickrService.GetPhotoSizes(photo.PhotoId).Where(ps => ps.MediaType == MediaType.Videos).ToList();
            if (videoSizes.Count > 0)
            {
                mediatype = MediaType.Videos.ToString().ToLowerInvariant();
                var videoSize = videoSizes.FirstOrDefault(vs => vs.Label == "Video Player");
                if (videoSize != null)
                    videourl = videoSize.Source;
            }
        }

        public string photoid;
        public string thumbnailurl;
        public string mediumurl;
        public string largeurl;
        public string mediatype;
        public string videourl;
    }
}