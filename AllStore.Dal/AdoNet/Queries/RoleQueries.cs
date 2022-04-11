using System;
using System.Collections.Generic;
using System.Text;

namespace AllStore.Dal.AdoNet.Queries
{
    public class RoleQueries : DbConnection
    {
        protected string GetAllRolesQuery { get { return "SELECT * FROM ROLES"; } }

        protected string GetRoleByIdQuery(int id)
        {
            return $@"SELECT * FROM ROLES WHERE ID = {id}";
        }

        protected string GetRolesByUserIdQuery(int userId)
        {
            return $@"SELECT * FROM ROLES WHERE ID IN(SELECT RoleId FROM USERSROLES WHERE UserId={userId})";
        }

        protected string InsertRoleQuery { get { return @"INSERT INTO ROLES VALUES (@name)"; } private set { } }
    }
}
