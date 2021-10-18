using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thynk_Code.Model.Entities;
using Thynk_Code.Model.ServiceModels;

namespace Thynk_Code.Service.Interfaces
{
    public interface IUserService
    {
        Task<(bool, object)> CreateUser(CreateUserModel userModel);
        Task<ApplicationUser> GetUserById(string id);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserByUserName(string username);
        Task<IEnumerable<ApplicationUser>> GetAllUser();
    }
}
