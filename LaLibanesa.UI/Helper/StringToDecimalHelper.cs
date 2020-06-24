using System.Globalization;

namespace Qmos.UI.Helper
{
    public static class StringToDecimalHelper
    {
        public static decimal Convert(string value) 
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            value = value.Contains(",") ? value.Replace(',', '.'): value;
            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result))
                return result;
            return 0;
        }
    }
    public static class DecimalToStringHelper
    {
        public static string Convert(decimal value)
        {
            return value.ToString("N", CultureInfo.InvariantCulture);
        }
    }
}
