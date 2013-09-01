using System;
using System.Globalization;
using System.Linq;

namespace CaucasianPearl.Core.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Возвращает строку с первым символом в нижнем регистре.
        /// </summary>
        /// <param name="value">Входная строка.</param>
        /// <returns></returns>
        public static string FirstCharToLower(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.First().ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + string.Join(string.Empty, value.Skip(1));
        }

        /// <summary>
        /// Восстанавливает скриптовые скобки.
        /// </summary>
        /// <param name="value">Входная строка.</param>
        /// <returns></returns>
        public static string DecodeScriptTags(string value)
        {
            return value.Replace('[', '<').Replace(']', '>');
        }

        /// <summary>
        /// Обрезает строку.
        /// </summary>
        /// <param name="value">Входная строка.</param>
        /// <param name="count">Величина для сравнения.</param>
        /// <returns></returns>
        public static string Substring(string value, int count)
        {
            return string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value.Length < count
                                      ? value
                                      : value.Substring(0, count) + "...";
        }
    }
}