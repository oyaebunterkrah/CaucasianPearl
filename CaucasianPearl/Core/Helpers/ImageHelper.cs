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
                return Consts.NoImage;

            return string.Format("{0}{1}",
                model.ID,
                string.IsNullOrWhiteSpace(model.ImageExt) ? Consts.NoImage : model.ImageExt.TrimEnd());
        }

        // Возвращает url изображения.
        public static string GetImageUrl(dynamic model, string imageFolder)
        {
            var imageUrl = string.Format("{0}/{1}", VirtualPathUtility.ToAppRelative(Consts.EntityImagesFolder), Consts.NoImage);

            if (model == null || string.IsNullOrWhiteSpace(imageFolder))
                return imageUrl;

            return string.Format("{0}/{1}/{2}",
                                 VirtualPathUtility.ToAppRelative(Consts.EntityImagesFolder),
                                 imageFolder,
                                 ImageHelper.GetImageName(model));
        }
    }
}
