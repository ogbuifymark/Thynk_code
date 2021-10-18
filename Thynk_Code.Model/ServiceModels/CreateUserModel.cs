using System;
namespace Thynk_Code.Model.ServiceModels
{
    public class CreateUserModel
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string otherName { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string role { get; set; }

        public CreateUserModel()
        {
        }
    }
}
