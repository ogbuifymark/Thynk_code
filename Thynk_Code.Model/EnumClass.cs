using System;
namespace Thynk_Code.Model
{
    public class EnumClass
    {
        public enum EntityStatus
        {
            Active = 1,
            Inactive
        }
        public enum Role
        {
            Admin = 1,
            User,
            Lab_Admin
        }
        public enum TestStatus
        {
            Pending = 0,
            Positive = 1,
            Negative=2 
        }

    }
}
