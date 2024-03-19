using Microsoft.AspNetCore.Http;

namespace ClinicManagementAPI.Classes
{
    public class Utility
    {
        public static string GetDateTimeToString(DateTime dt)
        {
            return dt.Day + "-" + dt.Month + "-" + dt.Year;
        }
        private static string format = "dd-MM-yyyy";

        public static DateTime GetDateTimeFromString(string dt)
        {
            if (!string.IsNullOrEmpty(dt))
                return DateTime.ParseExact(dt, format, System.Globalization.CultureInfo.InvariantCulture);
            else
                throw new Exception("date of birth cannot be null");
        }
    }
}
