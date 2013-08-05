using System.Globalization;
using System.Linq;

namespace CaucasianPearl.Core.Helpers
{
    public class StringHelper
    {
        /// <summary>
        /// Возвращает строку с первым символом в нижнем регистре.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FirstCharToLower(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            return source.First().ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + string.Join(string.Empty, source.Skip(1));
        }

        /// <summary>
        /// Восстанавливает скриптовые скобки.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecodeScriptTags(string value)
        {
            return value.Replace('[', '<').Replace(']', '>');
        }
    }
}