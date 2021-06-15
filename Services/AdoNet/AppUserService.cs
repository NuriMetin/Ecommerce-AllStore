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
    public class AppUserService : AppUserQueries, IAppUserService
    {
        public async Task<bool> AddRole(int userId, int roleId)
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
                return false;
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
                        sqlCommand.Parameters.AddWithValue("@password", EncodePassword(appUser.Password));
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
                return false;
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

        public AppUser GetByEmail(string email)
        {
            AppUser appUser = null;

            using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(GetUserByEmailQuery(email), sqlConnection))
                {
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
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
                            WrongPasswordCount=Convert.ToInt32(dataReader["WrongPasswordCount"]),
                            CreatedDate=Convert.ToDateTime(dataReader["InsertDate"]),
                            LockedDate= Convert.ToDateTime(dataReader["LockDate"]),
                            Active = Convert.ToBoolean(dataReader["Active"])
                        };
                    }
                    sqlCommand.Dispose();
                }
                sqlConnection.Close();
            }
            return appUser;
        }

        public Task<AppUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public AppUser GetByUsername(string username)
        {
            throw new NotImplementedException();
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
                return false;
            }
        }

        public SignInResult UserSignIn(AppUser user, string password)
        {
            SignInResult signInResult = new SignInResult();

            if (user.Active == false)
            {
                if (user.LockedDate.AddHours(4) > DateTime.Now)
                {
                    UnlockUser(user.Email);
                }
            }

            string passwordFromDb = DecodePassword(user.Password);

            if (password == passwordFromDb)
                signInResult.Success = true;

            else
            {
                signInResult.Success = false;
                UserBlock(user.Email);
            }

            return signInResult;
        }

        private string EncodePassword(string password)
        {
            byte[] encData_byte = new byte[password.Length];

            encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        private string DecodePassword(string password)
        {
            UTF8Encoding encoder = new System.Text.UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public void UserBlock(string email)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand("udp_blockUser", sqlConnection))
                    {
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                        sqlCommand.Parameters.AddWithValue("@email", email);

                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Dispose();
                    }
                    sqlConnection.Close();
                }
            }

            catch (Exception exp)
            {

            }
        }

        public void UnlockUser(string email)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(AllStoreConnection))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = new SqlCommand(UnlockUserQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@email", email);

                        sqlCommand.ExecuteNonQuery();

                        sqlCommand.Dispose();
                    }
                    sqlConnection.Close();
                }
            }

            catch (Exception exp)
            {

            }
        }
    }
}
