using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Thynk_Code.Data.Interfaces;
using Thynk_Code.Model.Entities;
using Thynk_Code.Model.ServiceModels;
using Thynk_Code.Model.Util;
using Thynk_Code.Service.Interfaces;
using Thynk_Code.Service.Util;

namespace Thynk_Code.Service.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork, IServiceFactory serviceFactory) :base(unitOfWork, serviceFactory)
        {
            _userManager = userManager;
        }
        public async Task<(bool, object)> CreateUser(CreateUserModel userModel)
        {
            CurrentDate currentDatetime = DateUtilities.GetCurrentDate();
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.userName,
                Email = userModel.email,
                FirstName = userModel.firstName,
                LastName = userModel.lastName,
                OtherName = userModel.otherName,
                PhoneNumber = userModel.phoneNumber,
                Role = userModel.role,
                CreatedAt = currentDatetime.CurrentDateTime,
                CreatedAtTimeStamp = currentDatetime.TimeStamp,
                EntityStatus = Model.EnumClass.EntityStatus.Active
                

            };
            //checking if email already exist
            ApplicationUser emailCheck = await _userManager.FindByEmailAsync(userModel.email);
            if (emailCheck != null)
                throw new InvalidOperationException("User with email already exist");

            //checking if username is already taken
            ApplicationUser userNameCheck = await _userManager.FindByNameAsync(userModel.userName);
            if (userNameCheck != null)
                throw new InvalidOperationException("Username already taken");

            var result = await _userManager.CreateAsync(user, userModel.password);
            if (result.Succeeded)
            {

                //Todo: Implement the email notification

                return (true,null);
                
            }
            //Todo: Implement what will happen if it did not succeed
            return (false, result.Errors);


        }
        public async Task<ApplicationUser> GetUserById(string id)
        {

            
            //checking if email already exist
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException("Cannot find user with id");

            return user;


        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {


            //checking if email already exist
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new InvalidOperationException("Cannot find user with email");

            return user;


        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {


            //checking if email already exist
            IEnumerable<ApplicationUser> users =  _userManager.Users;
            return users;


        }
        public async Task<ApplicationUser> GetUserByUserName(string username)
        {


            //checking if email already exist
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            if (user == null)
                throw new InvalidOperationException("Cannot find user with username");

            return user;


        }

    }
}
