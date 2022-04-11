using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Domain.Abstractions.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<List<Role>> GetRolesByUserIdAsync(int userId);
    }
}
