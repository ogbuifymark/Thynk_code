using System;
using Thynk_Code.Model.Util;

namespace Thynk_Code.Service.Util
{
    public static class DateUtilities
    {
        public static CurrentDate GetCurrentDate()
        {
            DateTime currentDateTime = DateTime.UtcNow;
            double timeStamp = currentDateTime.ToTimeStamp();
            CurrentDate currentDate = new CurrentDate(currentDateTime, timeStamp);
            return currentDate;
        }
        public static double ToTimeStamp(this DateTime dateInstance)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1);
            return (dateInstance - epochDateTime).TotalMilliseconds;
        }

    }
}
