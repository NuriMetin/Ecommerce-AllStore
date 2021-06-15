using AllStore.Models;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.AdoNet;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly AppUserService _appUserService;
        private readonly RoleService _roleService;
        public AccountController(IConfiguration config)
        {
            _config = config;
            _appUserService = new AppUserService();
            _roleService = new RoleService();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            AppUser appUser = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                FatherName = register.FatherName,
                Email = register.Email,
                CreatedDate = DateTime.Now,
                WrongPasswordCount = 0,
                Username = "test",
                Password = register.Password,
                Active = true
            };

            bool userCreated = await _appUserService.CreateAsync(appUser);

            if (!userCreated)
                return BadRequest();

            int userId = _appUserService.GetByEmail(register.Email).ID;

            Role roleFromDb = await _roleService.GetByIdAsync(2);

            if (roleFromDb != null && userId != 0)
                await _appUserService.AddRole(userId, 2);

            return Ok();
        }

        [HttpGet]
        public IActionResult Login([FromForm] LoginModel login)
        {

            IActionResult response = Unauthorized();

            AppUser user = _appUserService.GetByEmail(login.Email);

            if (user != null)
            {
                var result = _appUserService.UserSignIn(user, login.Password);

                if (result.Success)
                {
                    List<Role> roles = _roleService.GetRolesByUserId(user.ID);

                    var tokenStr = GenerateJSONWebToken(user, roles);

                    response = Ok(new { token = tokenStr });

                    if (user.WrongPasswordCount > 0)
                        _appUserService.UnlockUser(user.Email);
                }
            }

            return response;
        }

        public async Task<IActionResult> Update([FromForm] RegisterModel userModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            AppUser appUser = _appUserService.GetByEmail(userModel.Email);

            if (appUser == null)
                return NotFound();

            appUser.Name = userModel.Name;
            appUser.Surname = userModel.Surname;
            appUser.FatherName = userModel.FatherName;

            bool userUpdated = await _appUserService.UpdateAsync(appUser);

            if (!userUpdated)
                return BadRequest();

            return Ok();
        }

        private string GenerateJSONWebToken(AppUser userInfo, List<Role> roles)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (Role role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creditials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creditials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }

    }
}
