using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CaucasianPearl.Core.UserControls
{
    public class CssJsRegistrationControl : WebControl
    {
        #region Templates

        private const string CssTemplate = "<link href=\"{0}?rev={1}\" type=\"text/css\" />{2}";
        private const string CssTemplateWithRel = "<link href=\"{0}?rev={1}\" rel=\"stylesheet\" type=\"text/css\" />{2}";
        private const string JsTemplate = "<script type=\"text/javascript\" src=\"{0}?rev={1}\"></script>{2}";
        private const string JsTemplateWithCharset = "<script type=\"text/javascript\" src=\"{0}?rev={1}\" charset=\"{3}\"></script>{2}";
        private const string UnknownTypeTemplate = "<script type=\"text/javascript\" src=\"{0}\"></script>{1}";
        private const string UnknownTypeTemplateWithCharset = "<script type=\"text/javascript\" src=\"{0}\" charset=\"{2}\"></script>{1}";
        
        private readonly string _separator = Environment.NewLine;

        #endregion

        // Типы файлов.
        public enum FileTypes
        {
            Css,
            Js,
            Unknown
        }

        #region Properties

        // Тип файла (css/js).
        public FileTypes FileType
        {
            get
            {
                var extension = Path.GetExtension(Src);
                return extension != null
                           ? ToEnum(extension.Replace(".", string.Empty), FileTypes.Unknown)
                           : FileTypes.Unknown;
            }
        }

        // Url-адрес файла.
        public string Src { get; set; }
        // Всегда загружать по-новому
        public bool AlwaysLoad { get; set; }
        // Кодировка.
        public string Charset { get; set; }
        // rel.
        public string Rel { get; set; }

        #endregion

        // Добавляет к Url'ам .css и .js файлов версию, например "?rev=yyyyMMddhhmmss".
        // Это нужно для того, чтобы после релиза браузер клиента запрашивал новые файлы, если они были изменены.
        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                writer.Write(GetTemplate(FileType));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogFacade.LogWarning("CssJsRegistrationControl::Render (Src: {0}; ex.Message: {1}; ex.StackTrace: {2})", Src, ex, ex.StackTrace);
            }
        }

        #region Helpers

        // Возвращает шаблон на основе типа файла.
        private string GetTemplate(FileTypes fileType)
        {
            var revValue = fileType != FileTypes.Unknown && !AlwaysLoad
                               ? GetLastWriteTime()
                               : Environment.TickCount.ToString(CultureInfo.InvariantCulture);
            switch (fileType)
            {
                case FileTypes.Css:
                    return string.IsNullOrEmpty(Rel)
                               ? string.Format(CssTemplate, Src, revValue, _separator)
                               : string.Format(CssTemplateWithRel, Src, revValue, _separator);
                case FileTypes.Js:
                    return string.IsNullOrEmpty(Charset)
                               ? string.Format(JsTemplate, Src, revValue, _separator)
                               : string.Format(JsTemplateWithCharset, Src, revValue, _separator, Charset);
                case FileTypes.Unknown:
                    return string.IsNullOrEmpty(Charset)
                               ? string.Format(UnknownTypeTemplate, Src, _separator)
                               : string.Format(UnknownTypeTemplateWithCharset, Src, _separator, Charset);
            }

            return string.Empty;
        }

        // Возвращает версию для ?rev (версия = дата последнего изменения файла).
        private string GetLastWriteTime()
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
