
using System;

namespace leavetracker
{
    public class Date
    {
        public static DateTime GetFormatedDate(string dateString)
        {
            var dateValue = new DateTime();
            try
            {
                dateValue = DateTime.Parse(dateString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Unable to convert {dateString} into date.");
                throw ex;
            }
            return dateValue;
        }

        internal static bool isValid(DateTime startDate, DateTime endDate)
        {
            if (endDate >= startDate)
            {
                return true;
            }

            return false;
        }
    }
}