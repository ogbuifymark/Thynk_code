using System;
namespace Thynk_Code.Model.Util
{
    public class CurrentDate
    {
        public DateTime CurrentDateTime { get; }
        public double TimeStamp { get; }

        public CurrentDate(DateTime currentDateTime, double timeStamp) => (CurrentDateTime, TimeStamp) = (currentDateTime, timeStamp);
    }
}
