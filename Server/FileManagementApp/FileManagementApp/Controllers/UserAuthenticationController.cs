using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementApp.Models.Users;
using FileManagementApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace FileManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {

        private readonly IUserAuthenticationService _userAuthService;
        public UserAuthenticationController(IUserAuthenticationService userAuthService)
        {
            this._userAuthService = userAuthService;
        }

        [HttpPost]
        public IActionResult AuthenticateUser([FromBody]UserAuthenticateRequest model)
        {

            var response = this._userAuthService.AuthenticateUser(model);
            if (response == null)
            {
                Log.Information("email or password incorrect");
                return new BadRequestObjectResult(new { message = "email or password is incorrect" });
            }
            return Ok(response);
        }

    }
}