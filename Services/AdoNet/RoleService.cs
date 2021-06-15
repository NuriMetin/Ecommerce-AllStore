using DataAccess.Models;
using Services.Abstract;
using Services.AdoNet.Helpers.Queries;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Services.AdoNet
{
    public class RoleService : RoleQueries, IRoleService
    {
        public async Task<bool> CreateAsync(Role role)
        {
            bool result = true;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();
                    
                    using(SqlCommand sqlCommand=new SqlCommand("", sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@name", role.Name);

                        await sqlCommand.ExecuteNonQueryAsync();
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception exp)
            {
                result = false;
            }

            return result;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            Role role = null;

            using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(GetRoleByIdQuery(id), sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        role = new Role
                        {
                            ID = Convert.ToInt32(dataReader["ID"]),
                            Name = dataReader["Name"].ToString()
                        };
                    }
                    await sqlCommand.DisposeAsync();
                }
                await sqlConnection.CloseAsync();
            }
            return role;
        }


        public List<Role> GetRolesByUserId(int userId)
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(GetRolesByUserIdQuery(userId), sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Role role = new Role
                        {
                            ID = Convert.ToInt32(dataReader["ID"]),
                            Name = dataReader["Name"].ToString()
                        };
                        roles.Add(role);
                    }
                    sqlCommand.Dispose();
                }
                sqlConnection.Close();
            }
            return roles;
        }

        public async Task<bool> UpdateAsync(Role data)
        {
            throw new NotImplementedException();
        }
    }
}
