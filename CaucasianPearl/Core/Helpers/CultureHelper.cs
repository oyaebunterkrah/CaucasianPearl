﻿using System.Globalization;
using System.Threading;
using System.Web;
using CaucasianPearl.Core.Constants;
using System.Linq;

namespace CaucasianPearl.Core.Helpers
{
    public class CultureHelper
    {
        public static string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name;
            }
        }

        public static string CurrentCultureAbbr
        {
            get
            {
                return Thread.CurrentThread.CurrentCulture.Name.Split('-').FirstOrDefault();
            }
        }

        public static string Ru
        { get { return "ru-RU"; } }

        public static string Hy
        { get { return "hy-AM"; } }

        // Возвращает строку с первым символом в нижнем регистре.
        public static void Initialize()
        {
            var context = HttpContext.Current;

            // очень важно проверять готовность объекта сессии.
            if (context.Session != null)
            {
                var ci = (CultureInfo) context.Session[Consts.SessionKeys.Culture];

                if (ci == null)
                {
                    var culture = Consts.Culture.Ru;

                    if (context.Request.UserLanguages != null && context.Request.UserLanguages.Length != 0)
                        culture = context.Request.UserLanguages[0].Substring(0, 2);

                    ci = new CultureInfo(culture);
                    context.Session[Consts.SessionKeys.Culture] = ci;
                }

                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }
    }
}