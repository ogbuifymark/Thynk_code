using System;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Model.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public double CreatedAtTimeStamp { get; set; }
        public double UpdatedAtTimeStamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public EntityStatus EntityStatus { get; set; }
    }
}
