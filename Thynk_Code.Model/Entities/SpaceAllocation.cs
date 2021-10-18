using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thynk_Code.Model.Entities
{
    public class SpaceAllocation: BaseEntity
    {
        public SpaceAllocation()
        {
        }
        public DateTime TestDate { get; set; }
        public string time { get; set; }
        public string Location { get; set; }
        public string AllocatedBy { get; set; }

        [ForeignKey("AllocatedBy")]
        public virtual ApplicationUser User { get; set; }

    }
}
