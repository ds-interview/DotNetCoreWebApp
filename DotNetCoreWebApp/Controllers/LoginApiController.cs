using DotNetCoreWebApp.Models;
using DotNetCoreWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        private readonly ILoginRepo _loginRepo;
        public LoginApiController(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        [HttpPost]
        [Route("ValidateLogin")]
        public async Task<IActionResult> ValidateLogin(UserDto userDto)
        {
            if (userDto.Password != null)
                userDto.Password = Helper.EncodePasswordToBase64(userDto.Password);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Password is required");

            var result = await _loginRepo.ValidateLoginAsync(userDto);
            if (result != null)
            {
                result.Token = Helper.EncodePasswordToBase64(userDto.UserId + "$" + userDto.UserName);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
                return StatusCode(StatusCodes.Status204NoContent, "No Data");
        }

        [HttpPost]
        [Route("SignUpUser")]
        public async Task<IActionResult> SignUpUser(SignUpDto signUpDto)
        {
            if (signUpDto.Password != null)
                signUpDto.Password = Helper.EncodePasswordToBase64(signUpDto.Password);
            else
                return StatusCode(StatusCodes.Status400BadRequest, "Password is required");

            var result = await _loginRepo.SignUpUser(signUpDto);
            if (result > 0)
            {
                return StatusCode(StatusCodes.Status200OK, result);
            }
            else
                return StatusCode(StatusCodes.Status204NoContent, "No Data");
        }
    }
}
