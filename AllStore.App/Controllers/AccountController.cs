using AllStore.Domain.Abstractions.Services;
using AllStore.Domain.Models;
using AllStore.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AllStore.App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        public AccountController(IConfiguration config)
        {
            _config = config;
            _accountService = new AccountService();
            _roleService = new RoleService();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel register)
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

            bool userCreated = await _accountService.CreateAsync(appUser);

            if (!userCreated)
                return BadRequest();

            AppUser user = await _accountService.GetByEmailAsync(register.Email);


            Role roleFromDb = await _roleService.GetByIdAsync(2);

            if (roleFromDb != null && user.ID != 0)
                await _accountService.AddRoleAsync(user.ID, 2);

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            IActionResult response = Unauthorized();

            AppUser user = await _accountService.GetByEmailAsync(login.Email);

            if (user != null)
            {
                var result = await _accountService.UserSignInAsync(user, login.Password);

                if (result.Success)
                {
                    List<Role> roles = await _roleService.GetRolesByUserIdAsync(user.ID);

                    var tokenStr = _accountService.GenerateJSONWebToken(_config, user, roles);

                    response = Ok(new { token = tokenStr });

                    if (user.WrongPasswordCount > 0)
                        await _accountService.UnlockUserAsync(user.Email);
                }
            }

            return response;
        }
    }
}
