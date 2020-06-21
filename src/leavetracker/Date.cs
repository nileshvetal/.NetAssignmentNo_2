
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
    }
}