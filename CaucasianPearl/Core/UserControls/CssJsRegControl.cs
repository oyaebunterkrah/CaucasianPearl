using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CaucasianPearl.Core.UserControls
{
    public static class CssJsRegControl
    {
        private static readonly string Separator = Environment.NewLine;

        #region Templates

        private const string CssTemplate = "<link href=\"{0}?rev={1}\" rel=\"stylesheet\" type=\"text/css\" />{2}";
        private const string JsTemplate = "<script src=\"{0}?rev={1}\" charset=\"{3}\" type=\"text/javascript\"></script>{2}";

        #endregion

        // Типы файлов.
        private enum FileTypes
        {
            Css,
            Js
        }

        #region Properties

        // Тип файла (css/js).
        private static FileTypes FileType
        {
            get
            {
                var extension = Path.GetExtension(Src);
                return extension != null
                           ? ToEnum(extension.Replace(".", string.Empty), FileTypes.Js)
                           : FileTypes.Js;
            }
        }

        // Всегда загружать.
        private static bool ForceLoad { get; set; }
        // Url-адрес файла.
        private static string Src { get; set; }
        // Кодировка.
        private static string Charset { get; set; }
        
        #endregion

        // Добавляет к Url'ам .css и .js файлов версию, например "?rev=yyyyMMddhhmmss".
        // Это нужно для того, чтобы после релиза браузер клиента запрашивал новые файлы, если они были изменены.
        public static string Render(string src, string prefix = "", string charset = "", bool alwaysLoad = false)
        {
            ForceLoad = alwaysLoad;
            Src = VirtualPathUtility.ToAbsolute(string.Format("{0}{1}{2}", prefix, !string.IsNullOrWhiteSpace(prefix) ? "/" : string.Empty, src));
            Charset = FileType == FileTypes.Js ? charset : string.Empty;

            try
            {
                return GetTemplate(FileType);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
                //LogFacade.LogWarning("CssJsRegistrationControl::Render (Src: {0}; ex.Message: {1}; ex.StackTrace: {2})", Src, ex, ex.StackTrace);
            }
        }

        #region Helpers

        // Возвращает шаблон на основе типа файла.
        private static string GetTemplate(FileTypes fileType)
        {
            var revValue = !ForceLoad
                               ? GetLastWriteTime()
                               : Environment.TickCount.ToString(CultureInfo.InvariantCulture);

            return fileType == FileTypes.Css
                ? string.Format(CssTemplate, Src, revValue, Separator)
                : string.Format(JsTemplate, Src, revValue, Separator, Charset);
        }

        // Возвращает версию для ?rev (версия = дата последнего изменения файла).
        private static string GetLastWriteTime()
        {
            var fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(Src));
            return fileInfo.Exists ? fileInfo.LastWriteTime.ToString("yyyyMMddhhmmss") : Environment.TickCount.ToString(CultureInfo.InvariantCulture);
        }

        private static T ToEnum<T>(string value, T defaultValue)
        {
            if (Enum.GetNames(typeof(T)).All(e => String.Compare(e, value, StringComparison.OrdinalIgnoreCase) != 0))
                return defaultValue;

            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion
    }
}