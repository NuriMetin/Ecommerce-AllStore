using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IAppUserService : IRepository<AppUser>
    {
        AppUser GetByUsername(string username);
        AppUser GetByEmail(string email);
        Task<bool> AddRole(int userId, int roleId);
        SignInResult UserSignIn(AppUser user, string password);
        void UserBlock(string email);
        void UnlockUser(string email);
    }
}
