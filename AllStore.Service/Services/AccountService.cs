using AllStore.Dal.AdoNet;
using AllStore.Domain.Abstractions.Repositories;
using AllStore.Domain.Abstractions.Services;
using AllStore.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAppUserRepository _appUserRepository;
        public AccountService()
        {
            _appUserRepository = new AppUserRepository();
        }

        public async Task<SignInResult> UserSignInAsync(AppUser user, string password)
        {
            SignInResult signInResult = new SignInResult
            {
                AppUser = user
            };

            try
            {
                if (user.Active == false)
                {
                    if (user.LockedDate.AddHours(4) > DateTime.Now)
                    {
                        await _appUserRepository.UnlockUserAsync(user.Email);
                    }
                }

                string passwordFromDb = DecodePassword(user.Password);

                if (password == passwordFromDb)
                    signInResult.Success = true;

                else
                {
                    signInResult.Success = false;
                    await _appUserRepository.LockUserAsync(user.Email);
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return signInResult;
        }

        public string EncodePassword(string password)
        {
            byte[] encData_byte = new byte[password.Length];

            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        public string DecodePassword(string password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public string GenerateJSONWebToken(IConfiguration config, AppUser userInfo, List<Role> roles)
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

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creditials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddSeconds(5),
                    signingCredentials: creditials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return encodetoken;
        }

        public async Task<bool> CreateAsync(AppUser user)
        {
            try
            {
                user.Password = EncodePassword(user.Password);
                await _appUserRepository.CreateAsync(user);

                return true;
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return await _appUserRepository.GetByEmailAsync(email);
        }

        public async Task LockUserAsync(string email)
        {
            await _appUserRepository.LockUserAsync(email);
        }

        public async Task UnlockUserAsync(string email)
        {
            await _appUserRepository.UnlockUserAsync(email);
        }

        public async Task<bool> AddRoleAsync(int userId, int roleId)
        {
            return await _appUserRepository.AddRoleAsync(userId, roleId);
        }
    }
}
