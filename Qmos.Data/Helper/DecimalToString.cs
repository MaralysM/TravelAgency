using System.Globalization;

namespace Qmos.Data.Helper
{
    public static class DecimalToString
    {
        public static string GetString(decimal value)
        {
            return value.ToString("N", CultureInfo.InvariantCulture);
        }
    }
}
