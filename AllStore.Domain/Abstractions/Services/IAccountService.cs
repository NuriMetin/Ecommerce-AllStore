using AllStore.Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Domain.Abstractions.Services
{
    public interface IAccountService
    {
        Task<AppUser> GetByEmailAsync(string email);
        Task<SignInResult> UserSignInAsync(AppUser user, string password);
        public string EncodePassword(string password);
        public string DecodePassword(string password);
        Task<bool> CreateAsync(AppUser user);
        public string GenerateJSONWebToken(IConfiguration config, AppUser userInfo, List<Role> roles);
        Task LockUserAsync(string email);
        Task UnlockUserAsync(string email);
        Task<bool> AddRoleAsync(int userId, int roleId);
    }
}
