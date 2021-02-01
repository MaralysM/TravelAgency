using System.Globalization;

namespace TravelAgency.UI.Helper
{
    public static class StringToDecimalHelper
    {
         public static decimal ApplyFormat(decimal amount)
        {

            string specifierN2 = "N2";
            string strCultureUS = "en-US";
            System.Globalization.NumberFormatInfo nfiUS = new System.Globalization.CultureInfo(strCultureUS, false).NumberFormat;
            return decimal.Parse(amount.ToString(specifierN2, nfiUS), nfiUS);
        }


    }

}
