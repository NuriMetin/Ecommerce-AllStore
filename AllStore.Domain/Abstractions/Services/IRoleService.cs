﻿using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Domain.Abstractions.Services
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRolesByUserIdAsync(int userId);
        Task<Role> GetByIdAsync(int id);
    }
}
