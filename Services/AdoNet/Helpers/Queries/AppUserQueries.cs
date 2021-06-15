using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.AdoNet.Helpers.Queries
{
    public class AppUserQueries : DbConnection
    {
        protected string GetAllUsersQuery { get { return "SELECT * FROM USERS"; } }

        protected string GetUserByEmailQuery(string email)
        {
            return $@"SELECT   [ID]
                              ,[Name]
                              ,[Surname]
                              ,[FatherName]
                              ,[Username]
                              ,[Email]
                              ,[Password]
                              ,[WrongPasswordCount]
                              ,[InsertDate],
                              CASE
		                        WHEN LockDate IS NULL THEN ''
		                        ELSE LockDate
	                          END
	                          AS LockDate
                                ,[Active]
                        FROM USERS
                        WHERE Email='{email}'";
        }

        protected string CreateUserQuery
        {
            get
            {
                return $@"INSERT INTO USERS(Name, Surname, FatherName, Email, Username, Password, WrongPasswordCount, InsertDate, Active) VALUES(@name, @surname, @fatherName, @email, @username, @password, @wrongPasswordCount, @insertDate, @active)";
            }
        }

        protected string AddRoleToUserQuery
        {
            get
            {
                return $@"INSERT INTO USERSROLES(UserId, RoleID) VALUES (@userId, @roleId)";
            }
        }

        protected string UpdateUserByEmailQuery(string email)
        {
            return $@"UPDATE USERS SET 
	                    Name=@name, 
	                    Surname=@surname, 
	                    FatherName=@fatherName, 
	                    WHERE Email='{email}'";
        }

        protected string UnlockUserQuery
        {
            get
            {
                return $@"UPDATE USERS SET WrongPasswordCount=0, Active=1 WHERE Email=@email";
            }
        }
    }
}
