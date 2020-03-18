using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestAPIApp.Models;
using RestAPIApp.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestAPIApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        private IConfiguration _config;

        public LoginController(LoginService service, IConfiguration config)
        {
            _loginService = service;
            _config = config;
        }

        //For user login   
        //[Route("api/Login/UserLogin")]
        [HttpPost]
        public IActionResult Login([FromBody] Login Lg)
        {
            IActionResult response = Unauthorized();
            var user = _loginService.UserLogin(Lg);
            
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);

                UserVM userVM = new UserVM
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    RetypePassword = user.RetypePassword,
                    Address = user.Address,
                    Mobile = user.Mobile,
                    UserId = user.UserId
                };

                userVM.Token = tokenString;
                response = Ok(userVM);
            }
            return response;
        }

        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var claims = new[] {
            //    new Claim(JwtRegisteredClaimNames.Sub, user.Password),
            //    new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //};

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            null,
            expires: DateTime.Now.AddMinutes(2),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
