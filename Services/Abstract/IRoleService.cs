using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstract
{
    public interface IRoleService : IRepository<Role>
    {
        List<Role> GetRolesByUserId(int userId);
    }
}
