using System;
namespace Thynk_Code.Model.ServiceModels
{
    public class BookingModel
    {
        public Guid space_id { get; set; }
        public string userId { get; set; }
        public DateTime testDate { get; set; }
        public string time { get; set; }

        public BookingModel()
        {
        }
    }
}
