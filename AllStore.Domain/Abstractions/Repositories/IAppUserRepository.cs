using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Domain.Abstractions.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByUsernameAsync(string username);
        Task<AppUser> GetByEmailAsync(string email);
        Task<bool> AddRoleAsync(int userId, int roleId);
        Task LockUserAsync(string email);
        Task UnlockUserAsync(string email);
    }
}
