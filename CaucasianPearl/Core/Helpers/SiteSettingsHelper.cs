using System;
using System.Linq;
using CaucasianPearl.Core.EntityServices;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Models.EDM;
using CaucasianPearl.Core.Utilities;

namespace CaucasianPearl.Core.Helpers
{
    public class SiteSettingsHelper
    {
        #region Site settings types
        
        public enum HomePageMode
        {
            Events,
            Cover
        }
        
        #endregion

        public static SiteSettingsEntityService SiteSettingsService
        {
            get
            {
                return ServiceHelper<SiteSettingsEntityService>.GetService();
            }
        }

        /// <summary>
        /// Возвращает объект настроек сайта по ключу Name.
        /// </summary>
        /// <param name="settingName">Ключ.</param>
        /// <returns></returns>
        public static SiteSetting GetSiteSetting(string settingName)
        {
            return SiteSettingsService.GetFirstByCondition(ss => ss.Name == settingName);
        }

        /// <summary>
        /// Возвращает строковое значение параметра по ключу Name.
        /// </summary>
        /// <param name="settingName">Ключ.</param>
        /// <returns></returns>
        public static string GetSiteSettingValueAsString(string settingName)
        {
            var setting = SiteSettingsService.GetFirstByCondition(ss => ss.Name == settingName);

            return !string.IsNullOrWhiteSpace(setting.Value) ? setting.Value : setting.DefaultValue;
        }

        /// <summary>
        /// Возвращает типизированное значение параметра по ключу Name.
        /// </summary>
        /// <param name="settingName">Ключ.</param>
        /// <param name="defaultValue">Значение по умолчанию.</param>
        /// <returns></returns>
        public static T GetSiteSettingTyped<T>(string settingName, T defaultValue) where T : new()
        {
            var setting = SiteSettingsService.GetFirstByCondition(ss => ss.Name == settingName);

            return setting != null ? Utility.ToEnum(setting.Value, defaultValue) : default(T);
        }
    }
}