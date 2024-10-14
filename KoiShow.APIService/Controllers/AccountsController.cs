using KoiShow.APIService.Helper;
using KoiShow.Common;
using KoiShow.Data.DTO.Authentication;
using KoiShow.Data.Models;
using KoiShow.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KoiShow.APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly JwtHelper _jwtHelper;

        public AccountsController(JwtHelper jwtHelper, AccountService accountService)
        {
            _accountService = accountService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
        {
            var businessResult = await _accountService.GetByUserNameAndPassword(loginModel.UserName, loginModel.Password);

            if (businessResult.Status == Const.SUCCESS_READ_CODE)
            {
                var account = (Account)businessResult.Data;
                var token = _jwtHelper.GenerateToken(account);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [Authorize]  
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var id = User.FindFirst("id")?.Value;
            var address = User.FindFirst("address")?.Value;
            var email = User.FindFirst("appEmail")?.Value;
            var birthday = User.FindFirst("birthday")?.Value;
            var username = User.FindFirst("username")?.Value;
            var role = User.FindFirst("appRole")?.Value;
            var fullName = User.FindFirst("fullName")?.Value;

            return Ok(new
            {
                Id = id,
                Address = address,
                Email = email,
                Birthday = birthday,
                UserName = username,
                Role = role,
                FullName = fullName
            });
        }
    }
}
