using System;
using System.ComponentModel.DataAnnotations.Schema;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Model.Entities
{
    public class BookingDetail : BaseEntity
    {
        public BookingDetail()
        {
        }
        public Guid spaceAllocationId { get; set; }
        public string UserId { get; set; }
        public bool IsCanceld { get; set; }
        public TestStatus TestStatus { get; set; }
        public string time { get; set; }
        public DateTime TestDate { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }

}
