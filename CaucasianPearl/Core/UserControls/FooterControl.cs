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
                // �������� ������ ��� ����������:��������, ��� ������� ����� ���������� �� �����
                //var controllerActionPairs = SiteSettingsHelper
                //    .GetSiteSettingValueAsString(Consts.SiteSettings.FooterVisibility)
                //    .Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                //if (controllerActionPairs)

                //var controllerActionPairs = footerVisibility.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);
                return true;
            }
        }
        
        #endregion

        // ��������� � Url'�� .css � .js ������ ������, �������� "?rev=yyyyMMddhhmmss".
        // ��� ����� ��� ����, ����� ����� ������ ������� ������� ���������� ����� �����, ���� ��� ���� ��������.
        public static string xxx()
        {
            return string.Empty;
        }
    }
}