using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace CaucasianPearl.Core.Helpers.HtmlHelpers
{
    public static class ImageHtmlHelper
    {
        private const string GifExtension = ".gif";
        private const string JpgExtension = ".jpg";
        private const string JpegExtension = ".jpeg";
        private const string PngExtension = ".png";

        // Изменение размеров картинки.
        public static Image Resize(this Image image, int maxHeight, int maxWidth)
        {
            var destHeight = 0;
            var destWidth = 0;

            // Определяем новые размеры картинки.
            // Если она меньше максимального размера, то оставляем её без изменения.
            // Если хотя бы по одному измерению больше максимального размера,
            // то уменьшаем её пропорционально до максимального размера.
            if ((maxHeight == 0 || maxHeight >= image.Height) && (maxWidth == 0 || maxWidth >= image.Width))
                return image;

            if (maxHeight == 0 && maxWidth > 0)
            {
                destWidth = maxWidth;
                destHeight = image.Height*maxWidth/image.Width;
            }

            if (maxHeight > 0 && maxWidth == 0)
            {
                destHeight = maxHeight;
                destWidth = image.Width*maxHeight/image.Height;
            }

            if (maxHeight > 0 && maxWidth > 0)
            {
                destWidth = maxWidth;
                destHeight = image.Height*maxWidth/image.Width;

                if (destHeight > maxHeight)
                {
                    destHeight = maxHeight;
                    destWidth = image.Width*maxHeight/image.Height;
                }
            }

            return new Bitmap(image, new Size(destWidth, destHeight));
        }

        // Сохранение картинки на диск одновременно с изменением размеров.
        public static void ResizeAndSave(this HttpPostedFileBase imagefile, int maxHeight, int maxWidth, string strSavePath)
        {
            if (imagefile != null)
            {
                var format = ImageFormat.Bmp;
                var strExtension = Path.GetExtension(strSavePath);

                if (strExtension != null)
                    switch (strExtension.ToLower())
                    {
                        case GifExtension:
                            format = ImageFormat.Gif;
                            break;

                        case JpgExtension:
                            format = ImageFormat.Jpeg;
                            break;

                        case JpegExtension:
                            format = ImageFormat.Jpeg;
                            break;

                        case PngExtension:
                            format = ImageFormat.Png;
                            break;
                    }

                Image.FromStream(imagefile.InputStream).Resize(maxHeight, maxWidth).Save(strSavePath, format);
            }
        }

        // Рендерит изображение с ссылкой.
        public static MvcHtmlString ActionImage(this HtmlHelper html, string action, string controller, object routeValues, string imagePath, string alt)
        {
            var url = new UrlHelper(html.ViewContext.RequestContext);

            // build the <img> tag
            var imgBuilder = new TagBuilder("img");
            imgBuilder.MergeAttribute("src", url.Content(imagePath));
            imgBuilder.MergeAttribute("alt", alt);
            var imgHtml = imgBuilder.ToString(TagRenderMode.SelfClosing);

            // build the <a> tag
            var anchorBuilder = new TagBuilder("a");
            anchorBuilder.MergeAttribute("href", url.Action(action, controller, routeValues));
            anchorBuilder.InnerHtml = imgHtml; // include the <img> tag inside
            var anchorHtml = anchorBuilder.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(anchorHtml);
        }
    }
}