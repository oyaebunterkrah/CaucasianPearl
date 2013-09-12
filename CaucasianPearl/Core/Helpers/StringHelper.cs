using System;
using System.Collections.Generic;
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

            return value.First().ToString(CultureInfo.InvariantCulture).ToLowerInvariant() +
                   string.Join(string.Empty, value.Skip(1));
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

        /// <summary>
        /// Склоняет месяц.
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns></returns>
        public static string DeclineMonth(DateTime date)
        {
            var monthDict = new Dictionary<string, string>
                {
                    { "январь", "января" },
                    { "февраль", "февраля" },
                    { "март", "марта" },
                    { "апрель", "апреля" },
                    { "май", "мая" },
                    { "июнь", "июня" },
                    { "июль", "июля" },
                    { "август", "августа" },
                    { "сентябрь", "сентября" },
                    { "октябрь", "октября" },
                    { "ноябрь", "ноября" },
                    { "декабрь", "декабря" },

                    { "հունվար", "հունվարի" },
                    { "փետրվար", "փետրվարի" },
                    { "մարտ", "մարտի" },
                    { "ապրիլ", "ապրիլի" },
                    { "մայիս", "մայիսի" },
                    { "հունիս", "հունիսի" },
                    { "հուլիս", "հուլիսի" },
                    { "օգոստոս", "օգոստոսի" },
                    { "սեպտեմբեր", "սեպտեմբերի" },
                    { "հոկտեմբեր", "հոկտեմբերի" },
                    { "նոյեմբեր", "նոյեմբերի" },
                    { "դեկտեմբեր", "դեկտեմբերի" },
                };

            return monthDict[date.ToString("MMMM", CultureInfo.CurrentCulture).ToLower()];
        }
    }
}