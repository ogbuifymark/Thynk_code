using System;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Model.ServiceModels
{
    public class SpaceAllocationModel
    {
        public string userId { get; set; }
        public string location { get; set; }
        public DateTime testDate { get; set; }
        public string time { get; set; }

        
    }
    public class AvailabilityModel
    {
        public Guid spaceId { get; set; }
        public bool isAvailable { get; set; }

    }

    public class UpdateBookingModel
    {
        public Guid bookingId { get; set; }
        public string userId { get; set; }
        public TestStatus? testStatus { get; set; }

    }
    public class ReportModel
    {
        public int bookingCapacity { get; set; }
        public int bookings { get; set; }
        public int test { get; set; }
        public int positiveCases { get; set; }
        public int negativeCases { get; set; }

    }

}
