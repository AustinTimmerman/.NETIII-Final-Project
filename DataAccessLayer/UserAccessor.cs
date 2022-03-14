using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class UserAccessor : IUserAccessor
    {
        public bool AuthenticateUserWithEmail(string email)
        {
            bool result = true;
            int amount = 0;

            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_authenticate_email";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50);

            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                amount = Convert.ToInt32(cmd.ExecuteScalar());
                if (amount >= 1)
                {
                    result = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_authenticate_user";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);

            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool AuthenticateUserWithUsername(string username)
        {
            bool result = true;
            int amount = 0;

            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_authenticate_username";

            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50);

            cmd.Parameters["@Username"].Value = username;

            try
            {
                conn.Open();
                amount = Convert.ToInt32(cmd.ExecuteScalar());
                if (amount >= 1){
                    result = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }

        public int InsertNewUser(string username, string email, string passwordHash, List<string> roles)
        {
            int rowsAffected = 0;
            bool usernameResult = AuthenticateUserWithUsername(username);
            bool emailResult = AuthenticateUserWithEmail(email);

            if(usernameResult && emailResult)
            {
                var conn = DBConnection.GetConnection();
                var cmdText = @"sp_insert_user";
                var cmd = new SqlCommand(cmdText, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 100);
                cmd.Parameters["@Username"].Value = username;
                cmd.Parameters["@Email"].Value = email;
                cmd.Parameters["@PasswordHash"].Value = passwordHash;
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            else if (!usernameResult && !emailResult)
            {
                throw new ApplicationException("Username and Email are already taken.");
            }
            else if (!usernameResult)
            {
                throw new ApplicationException("Username is already taken.");
            }
            else if (!emailResult)
            {
                throw new ApplicationException("Email is already taken.");
            }
            else
            {
                throw new ApplicationException("Bad email or password.");
            }

            return rowsAffected;
        }

        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<string>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_select_user_roles_by_userID";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_user_by_email";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
            cmd.Parameters["@Email"].Value = email;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            UserID = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Email = reader.GetString(2),
                            Active = reader.GetBoolean(3)
                        };
                    }
                }
                else
                {
                    throw new ApplicationException("User not found");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return user;
        }

        public List<string> SelectUserRoles()
        {
            List<string> roles = new List<string>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_select_all_roles";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roles;
        }
    }
}
