using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using System.Security.Cryptography;
using DataAccessLayer;
using DataAccessFakes;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        private IUserAccessor _userAccessor = null;

        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        public UserManager(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }

        public bool AuthenticateUser(string email, string passwordHash)
        {
            bool result = false;

            try
            {
                result = (1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, passwordHash));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<string> GetRolesForUser(int userID)
        {
            List<string> roles = null;

            try
            {
                roles = _userAccessor.SelectRolesByUserID(userID);
            }
            catch (Exception)
            {
                throw;
            }

            return roles;
        }

        public User GetUserByEmail(string email)
        {
            User requestedUser = null;

            try
            {
                requestedUser = _userAccessor.SelectUserByEmail(email);
            }
            catch (Exception ex)
            {
                if(ex.Message == "User not found")
                {
                    return requestedUser;
                }
                else
                {
                    throw;
                }
            }

            return requestedUser;
        }

        //public bool AuthenticateUsernameOrEmailNotTaken(string username, string email)
        //{
        //    bool result = true;

        //    if(_userAccessor.AuthenticateUserWithUsernameAndEmail(username, email) == false)
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        public string HashSha256(string source)
        {
            string result = "";
            
            byte[] data;

            using (SHA256 sha256Hasher = SHA256.Create())
            {
                data = sha256Hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }

            var s = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }

            result = s.ToString();

            return result.ToUpper();
        }

        public User LoginUser(string email, string password)
        {
            User loggedInUser = null;

            try
            {

                if (email == "")
                {
                    throw new ArgumentException("Missing email.");
                }
                if (password == "")
                {
                    throw new ArgumentException("Missing password.");
                }


                password = HashSha256(password);
                if (AuthenticateUser(email, password))
                {
                    loggedInUser = GetUserByEmail(email);
                    loggedInUser.Roles = GetRolesForUser(loggedInUser.UserID);
                }
                else
                {
                    throw new ApplicationException("Bad Email or Password.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Login failed. Please check your credentials", ex);
            }

            return loggedInUser;
        }

        public int InsertNewUser(string username, string email, string passwordHash, List<string> roles)
        {
            int rowsAffected = 0;
            try
            {
                rowsAffected = _userAccessor.InsertNewUser(username, email, HashSha256(passwordHash), roles);
            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public List<string> RetrieveUserRoles()
        {
            List<string> roles = new List<string>();

            try
            {
                roles = _userAccessor.SelectUserRoles();
            }
            catch (Exception)
            {

                throw;
            }

            return roles;
        }

        public int RetrieveUserIDFromEmail(string email)
        {
            try
            {
                return _userAccessor.SelectUserByEmail(email).UserID;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Database error", ex);
            }
        }
    }
}
