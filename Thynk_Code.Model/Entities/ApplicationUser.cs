using System;
using Microsoft.AspNetCore.Identity;
using static Thynk_Code.Model.EnumClass;

namespace Thynk_Code.Model.Entities
{
    
    public class ApplicationUser : IdentityUser
    {
        public double CreatedAtTimeStamp { get; set; }
        public double UpdatedAtTimeStamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public EntityStatus EntityStatus { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public string Role { get; set; }


        public string GetFullName()
        {
            return $"{FirstName} {OtherName} {LastName}";
        }

        
    }
}
