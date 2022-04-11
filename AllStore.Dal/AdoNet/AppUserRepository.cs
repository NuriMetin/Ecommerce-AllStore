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
    public class AppUserRepository : AppUserQueries, IAppUserRepository
    {
        public async Task<bool> AddRoleAsync(int userId, int roleId)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(AddRoleToUserQuery, sqlConnection))
                    {
                        //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@userId", userId);
                        sqlCommand.Parameters.AddWithValue("@roleId", roleId);

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

        public async Task<bool> CreateAsync(AppUser appUser)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(CreateUserQuery, sqlConnection))
                    {
                        //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@name", appUser.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", appUser.Surname);
                        sqlCommand.Parameters.AddWithValue("@fatherName", appUser.FatherName);
                        sqlCommand.Parameters.AddWithValue("@email", appUser.Email);
                        sqlCommand.Parameters.AddWithValue("@username", appUser.Email);
                        sqlCommand.Parameters.AddWithValue("@password", appUser.Password);
                        sqlCommand.Parameters.AddWithValue("@wrongPasswordCount", 0);
                        sqlCommand.Parameters.AddWithValue("@insertDate", DateTime.Now);
                        sqlCommand.Parameters.AddWithValue("@active", appUser.Active);

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

        public async Task<IEnumerable<AppUser>> GetAllAsync()
        {
            List<AppUser> allUsers = new List<AppUser>();

            using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(GetAllUsersQuery, sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AppUser appUser = new AppUser
                        {
                            ID = Convert.ToInt32(dataReader["ID"]),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            FatherName = dataReader["FatherName"].ToString(),
                            Username = dataReader["Username"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            Active = Convert.ToBoolean(dataReader["Active"])
                        };
                        allUsers.Add(appUser);
                    }
                    await sqlCommand.DisposeAsync();
                }
                await sqlConnection.CloseAsync();
            }
            return allUsers;
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            AppUser appUser = null;

            using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
            {
                await sqlConnection.OpenAsync();

                using (SqlCommand sqlCommand = new SqlCommand(GetUserByEmailQuery(email), sqlConnection))
                {
                    SqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync();

                    while (await dataReader.ReadAsync())
                    {
                        appUser = new AppUser
                        {
                            ID = Convert.ToInt32(dataReader["ID"]),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            FatherName = dataReader["FatherName"].ToString(),
                            Username = dataReader["Username"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            WrongPasswordCount = Convert.ToInt32(dataReader["WrongPasswordCount"]),
                            CreatedDate = Convert.ToDateTime(dataReader["InsertDate"]),
                            LockedDate = Convert.ToDateTime(dataReader["LockDate"]),
                            Active = Convert.ToBoolean(dataReader["Active"])
                        };
                    }
                    sqlCommand.Dispose();
                }
                sqlConnection.Close();
            }
            return appUser;
        }

        public async Task<AppUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task LockUserAsync(string email)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand("udp_blockUser", sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@email", email);

                        await sqlCommand.ExecuteNonQueryAsync();

                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public async Task UnlockUserAsync(string email)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(UnlockUserQuery, sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@email", email);

                        await sqlCommand.ExecuteNonQueryAsync();

                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                }
            }

            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }

        public async Task<bool> UpdateAsync(AppUser appUser)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand(UpdateUserByEmailQuery(appUser.Email), sqlConnection))
                    {
                        //sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@name", appUser.Name);
                        sqlCommand.Parameters.AddWithValue("@surname", appUser.Surname);
                        sqlCommand.Parameters.AddWithValue("@fatherName", appUser.FatherName);

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
    }
}
