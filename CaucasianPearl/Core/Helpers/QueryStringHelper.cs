using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CaucasianPearl.Core.Helpers
{
    public class QueryStringHelper
    {
        /// <summary>
        /// Возвращает, содержит ли url указанный параметр.
        /// </summary>
        /// <param name="key">Параметр.</param>
        /// <returns></returns>
        public static bool IsUrlContains(string key)
        {
            return HttpContext.Current.Request.RawUrl.Contains(key);
        }
    }
}