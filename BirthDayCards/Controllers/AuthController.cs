using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.AspNetCore.Identity.Cognito;
using BirthDayCards.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using BirthDayCards.Models;

namespace BirthDayCards.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<CognitoUser> _userManager;
        private readonly SignInManager<CognitoUser> _signInManager;
        private IConfiguration _config;
        private readonly CognitoUserPool _pool;
        private BirthDayCard_dbContext _context;

        public AuthController(SignInManager<CognitoUser> signInManager,
           UserManager<CognitoUser> userManager,
           IConfiguration config,
           CognitoUserPool pool,
           BirthDayCard_dbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _pool = pool;
            _context = context;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<JsonResult> Register([FromBody]LoginVM loginVM)
        {
            dynamic jsonResponse = new JObject();

            var user = _pool.GetUser(loginVM.UserName);//these line was changed
            user.Attributes.Add(CognitoAttribute.Email.AttributeName, loginVM.Email); //these line was changed
            var result = await _userManager.CreateAsync(user, loginVM.Password);

            if (result.Succeeded)
            {
                if (user != null)
                {
                    Users userRole = new Users
                    {
                        RoleId = 3,
                        UserName = loginVM.Email
                    };

                    _context.Users.Add(userRole);
                    _context.SaveChanges();

                    var tokenString = GenerateJSONWebToken(user);
                    jsonResponse.token = tokenString;
                    jsonResponse.status = "OK";
                    return Json(jsonResponse);
                }
            }
            jsonResponse.token = "";
            jsonResponse.status = "Invalid Login";
            return Json(jsonResponse);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<JsonResult> Login([FromBody]LoginVM loginVM)
        {
            dynamic jsonResponse = new JObject();

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginVM.Email, loginVM.Password, loginVM.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = _pool.GetUser(loginVM.Email);

                    if (user != null)
                    {
                        var tokenString = GenerateJSONWebToken(user);
                        jsonResponse.token = tokenString;
                        jsonResponse.status = "OK";
                        return Json(jsonResponse);
                    }
                }
                else if (result.IsLockedOut)
                {
                    jsonResponse.token = "";
                    jsonResponse.status = "Locked Out";
                    return Json(jsonResponse);
                }
            }
            jsonResponse.token = "";
            jsonResponse.status = "Invalid Login";
            return Json(jsonResponse);
        }

        List<Claim> AddUserRoleClaims(List<Claim> claims, string Email)
        {
            // Get current user's role. 
            var Role = _context.Users.Where(ur => ur.UserName == Email).FirstOrDefault();

            var userRole = _context.Roles.Where(ur => ur.RoleId == Role.RoleId).FirstOrDefault();
                          
            // Add user's role to the claims list.

            claims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));

            return claims;
        }

        string GenerateJSONWebToken(CognitoUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserID),
                new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserID)
            };

            claims = AddUserRoleClaims(claims, user.UserID);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}