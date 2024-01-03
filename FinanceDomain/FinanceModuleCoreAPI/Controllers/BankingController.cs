using FinanceModuleCoreAPI.Entities;
using FinanceModuleCoreAPI.Entities.Entities;
using FinanceModuleCoreAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceModuleCoreAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BankingController : ControllerBase
    {
        private readonly IBankingService _bankingService;

        IConfiguration _configuration;
        public BankingController(IBankingService bankingService, IConfiguration configuration)
        {
            this._bankingService = bankingService;
            this._configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("user")]
        public IActionResult CreateUser(string userName, string password)
        {
            var loggedInUser = new Users { Username = userName, Password = password };
            var newUser = _bankingService.CreateUser(loggedInUser);
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", loggedInUser.UserId.ToString()!),
                        new Claim("UserName", loggedInUser.Username!.ToString()),
                    };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);


            loggedInUser.Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(newUser);
        }

        [HttpPost("login")]
        public IActionResult Login(string userName, string password)
        {
            var loggedInUser = _bankingService.Login(userName, password);
            if (loggedInUser == null)
            {
                // If user is null, either username doesn't exist or password is incorrect
                return BadRequest("Username and/or password do not match.");
            }
            else
            {
                return Ok(loggedInUser);
            }
        }

        [HttpGet("checkBalance/{userName}")]
        public IActionResult CheckBalance(string userName)
        {
            var balance = _bankingService.CheckBalance(userName);
            return Ok(balance);
        }

        [HttpPost("transferFunds")]
        public IActionResult TransferFunds([FromBody] TransferRequest request)
        {
            var success = _bankingService.TransferFunds(request.UserName, request.Amount);

            if (success.IsSuccess)
            {
                return Ok(new { Message = success.Message });
            }
            else
            {
                return BadRequest(new { Message = success.Message });
            }
        }
    }

}
