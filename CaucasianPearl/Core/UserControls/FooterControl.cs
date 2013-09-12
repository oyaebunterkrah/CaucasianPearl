using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using CaucasianPearl.Core.Constants;
using CaucasianPearl.Core.Helpers;

namespace CaucasianPearl.Core.UserControls
{
    public static class Footerontrol
    {
        #region Properties

        // xxx.
        public static bool Show
        {
            get
            {
                // получаем массив пар контроллер:действие, для который футер отображать не нужно
                //var controllerActionPairs = SiteSettingsHelper
                //    .GetSiteSettingValueAsString(Consts.SiteSettings.FooterVisibility)
                //    .Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                //if (controllerActionPairs)

                //var controllerActionPairs = footerVisibility.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                return true;
            }
        }
        
        #endregion

        // Добавляет к Url'ам .css и .js файлов версию, например "?rev=yyyyMMddhhmmss".
        // Это нужно для того, чтобы после релиза браузер клиента запрашивал новые файлы, если они были изменены.
        public static string xxx()
        {
            return string.Empty;
        }
    }
}