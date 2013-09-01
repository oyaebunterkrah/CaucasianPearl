using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaucasianPearl.Core.Utilities
{
    public static class Utility
    {

        public static T ToEnum<T>(string value, T defaultValue)
        {
            if (Enum.GetNames(typeof(T)).All(e => string.Compare(e, value, StringComparison.OrdinalIgnoreCase) != 0))
                return defaultValue;

            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}