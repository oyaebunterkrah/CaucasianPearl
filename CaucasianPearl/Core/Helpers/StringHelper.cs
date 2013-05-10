using System.Globalization;
using System.Linq;

namespace CaucasianPearl.Core.Helpers
{
    public class StringHelper
    {
        // Возвращает строку с первым символом в нижнем регистре.
        public static string FirstCharToLower(string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            return source.First().ToString(CultureInfo.InvariantCulture).ToLowerInvariant() + string.Join(string.Empty, source.Skip(1));
        }
    }
}