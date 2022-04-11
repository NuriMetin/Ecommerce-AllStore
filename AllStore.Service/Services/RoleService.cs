using AllStore.Dal.AdoNet;
using AllStore.Domain.Abstractions.Repositories;
using AllStore.Domain.Abstractions.Services;
using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService()
        {
            _roleRepository = new RoleRepository();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
        {
            return await _roleRepository.GetRolesByUserIdAsync(userId);
        }
    }
}
