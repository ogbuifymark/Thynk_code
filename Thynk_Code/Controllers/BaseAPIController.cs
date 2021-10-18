using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Thynk_Code.Controllers
{
    public abstract class BaseAPIController : Controller
    {
        [NonAction]

        public OkObjectResult Ok(object value, string message = "", string code = null)
        {
            return base.Ok(new
            {
                Status = StaticFields.success,
                status_code = 200,
                code,
                Message = message,
                Data = value
            });
        }

        [NonAction]
        public BadRequestObjectResult BadRequest(object value, string message = "", int status_code = 409, string code = "")
        {
            return base.BadRequest(new
            {
                Status = StaticFields.failure,
                Status_code = status_code,
                Code = code,
                Message = message,
                Data = value


            });
        }


        public static class StaticFields
        {
            public static string success = "success";
            public static string failure = "failure";
        }
    }
}
