using AllStore.Dal.AdoNet.Queries;
using AllStore.Domain.Abstractions.Repositories;
using AllStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace AllStore.Dal.AdoNet
{
    public class RoleRepository : RoleQueries, IRoleRepository
    {
        public async Task<bool> CreateAsync(Role role)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@name", role.Name);

                        await sqlCommand.ExecuteNonQueryAsync();
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                }
                return true;
            }

            catch (Exception exp)
            {
               throw new Exception(exp.Message);
            }
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

        public async Task<List<Role>> GetRolesByUserIdAsync(int userId)
        {
            List<Role> roles = new List<Role>();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(GetRolesByUserIdQuery(userId), sqlConnection))
                    {
                        SqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync();

                        while (await dataReader.ReadAsync())
                        {
                            Role role = new Role
                            {
                                ID = Convert.ToInt32(dataReader["ID"]),
                                Name = dataReader["Name"].ToString()
                            };
                            roles.Add(role);
                        }

                        await dataReader.CloseAsync();
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return roles;
        }

        public Task<bool> UpdateAsync(Role data)
        {
            throw new NotImplementedException();
        }
    }
}
