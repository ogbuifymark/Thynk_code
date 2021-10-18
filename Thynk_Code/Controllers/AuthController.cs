using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Thynk_Code.Model.ServiceModels;
using Thynk_Code.Service.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Thynk_Code.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseAPIController
    {
        IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserModel userModel)
        {
            try
            {
                (bool,object) result = await _userService.CreateUser(userModel);
                if (result.Item1)
                {
                    return Ok();
                }
                else
                {
                    IEnumerable<IdentityError> identityError = (IEnumerable<IdentityError>)result.Item2;
                    foreach (var error in identityError)
                    {
                        this.ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return this.BadRequest(this.ModelState,"unable to save because of some errors");
                }

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _userService.GetAllUser();
                
                return Ok(result);
                

            }
            catch (Exception ex)
            {
                // Todo: add logger here to log the error
                return BadRequest(ex.Message);
            }
        }
    }
}
