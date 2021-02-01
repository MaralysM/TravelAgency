using System.Globalization;

namespace TravelAgency.Data.Helper
{
    public static class DecimalToString
    {
        public static string GetString(decimal value)
        {
            return value.ToString("N", CultureInfo.InvariantCulture);
        }
    }
}
