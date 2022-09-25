using  EmployeeCRUD.Core.Utilities.StringExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  EmployeeCRUD.Core.Utilities.DateAndTime
{
    public static class Extentions
    {
        //-----------------------------------------------------------------------------------------
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
        //-----------------------------------------------------------------------------------------
        public static DateTime LastDayOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        //-----------------------------------------------------------------------------------------
        public static DateTime StartDayOfMonth(this DateTime dt) => new DateTime(dt.Year, dt.Month, 1);
        //-----------------------------------------------------------------------------------------
        public static DateTime ToLocalTimeZone(this DateTime dateTime, int hours, int minutes)
        {
            string displayName = "(UTC+03:00) Antarctica/Mawson Time";
            string standardName = "Egypt Time";
            TimeSpan offset = new TimeSpan(hours, minutes, 00);
            TimeZoneInfo mawson = TimeZoneInfo.CreateCustomTimeZone(standardName, offset, displayName, standardName);
            return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, mawson);
        }
        //-----------------------------------------------------------------------------------------
        public static IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
            return date;
        }
        //-----------------------------------------------------------------------------------------
        public static string To12HourString(this TimeSpan time, bool IsLocal)
        {
            var hours = time.Hours;
            var minutes = time.Minutes;
            var amPmDesignator = IsLocal ? "ص" : "AM";
            if (hours == 0)
                hours = 12;
            else if (hours == 12)
                amPmDesignator = IsLocal?"م": "PM";
            else if (hours > 12)
            {
                hours -= 12;
                amPmDesignator = IsLocal ? "م" : "PM";
            }
            var formattedTime =
              String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
            return formattedTime;
        }
       
        //-----------------------------------------------------------------------------------------
    }
}
