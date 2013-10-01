using System;
using System.Linq;
using System.Web;
using CaucasianPearl.Core.Constants;

namespace CaucasianPearl.Core.Utilities
{
    public static class Utility
    {
        public static string GetReturnUrl
        {
            get { return HttpContext.Current.Request.QueryString[Consts.QueryStringParameters.ReturnUrl] ?? string.Empty; }
        }

        public static T ToEnum<T>(string value, T defaultValue)
        {
            if (Enum.GetNames(typeof (T)).All(e => string.Compare(e, value, StringComparison.OrdinalIgnoreCase) != 0))
                return defaultValue;

            return (T) Enum.Parse(typeof (T), value, true);
        }
    }
}