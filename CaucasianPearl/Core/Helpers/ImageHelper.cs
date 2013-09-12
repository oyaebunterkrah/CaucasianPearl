using System.IO;
using System.Web;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.Core.Helpers
{
    public class ImageHelper
    {
        // Возвращает имя изображения.
        public static string GetImageName(dynamic model)
        {
            if (model == null)
                return Consts.DefaultImage;

            var imageName = string.Format("{0}{1}",
                                 model.ID,
                                 string.IsNullOrWhiteSpace(model.ImageExt) ? Consts.DefaultImage : model.ImageExt.TrimEnd());

            return imageName;
        }

        // Возвращает url изображения.
        public static string GetImageUrl(dynamic model, string imageFolder)
        {
            var defaultImageUrl = string.Format("{0}/{1}", VirtualPathUtility.ToAppRelative(Consts.Paths.Img.SiteImgFolder), Consts.DefaultImage);

            if (model == null || string.IsNullOrWhiteSpace(imageFolder))
                return defaultImageUrl;

            var imageUrl = string.Format("{0}/{1}/{2}",
                                 VirtualPathUtility.ToAppRelative(Consts.Paths.Img.EntityImgFolder),
                                 imageFolder,
                                 ImageHelper.GetImageName(model));

            return !File.Exists(HttpContext.Current.Server.MapPath(imageUrl)) ? defaultImageUrl : imageUrl;
        }

        // Возвращает url изображения по умолчанию.
        public static string GetDefaultImageUrl(string imagePath)
        {
            return VirtualPathUtility.ToAppRelative(string.Format("{0}/{1}", imagePath, Consts.DefaultImage));
        }
    }
}