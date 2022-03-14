using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        private List<User> fakeUsers = new List<User>();
        private List<string> passwordHashes = new List<string>();

        public UserAccessorFake()
        {
            fakeUsers.Add(new User()
            {
                UserID = 999999,
                Username = "TessTheGoat",
                Email = "tess@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeUsers.Add(new User()
            {
                UserID = 999998,
                Username = "DuplicateMan",
                Email = "duplicate@company.com",
                Active = true,
                Roles = new List<string>()
            });
            fakeUsers.Add(new User()
            {
                UserID = 999997,
                Username = "DuplicateMan",
                Email = "duplicate@company.com",
                Active = true,
                Roles = new List<string>()
            });
            passwordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            passwordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            passwordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            fakeUsers[0].Roles.Add("Admin");
            fakeUsers[0].Roles.Add("Logged in");
        }

        public bool AuthenticateUserWithEmail(string email)
        {
            bool result = true;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].Email == email)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int numAuthenticated = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                int userIndex = -1;
                if (fakeUsers[i].Email == email)
                {
                    userIndex = i;
                    if (passwordHashes[userIndex] == passwordHash && fakeUsers[userIndex].Active)
                    {
                        numAuthenticated += 1;
                    }
                }
            }

            return numAuthenticated;
        }

        public bool AuthenticateUserWithUsername(string username)
        {
            bool result = true;

            for (int i = 0; i < fakeUsers.Count; i++)
            {   
                if (fakeUsers[i].Username == username)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public int InsertNewUser(string username, string email, string password, List<string> roles)
        {
            int rowsAffected = 0;
            bool emailResult = AuthenticateUserWithEmail(email);
            bool usernameResult = AuthenticateUserWithUsername(username);

            if (emailResult && usernameResult)
            {
                fakeUsers.Add(new User()
                {
                    UserID = (fakeUsers[fakeUsers.Count - 1].UserID) - 1,
                    Username = username,
                    Email = email,
                    Active = true,
                    Roles = roles
                });
                passwordHashes.Add(password);
                rowsAffected++;
            }
            else if(!emailResult && !usernameResult)
            {
                throw new ApplicationException("Username and Email are already taken.");
            }
            else if(!usernameResult)
            {
                throw new ApplicationException("Username is already taken.");
            }
            else if(!emailResult)
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
            bool foundUser = false;


            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].UserID == userID)
                {
                    roles = fakeUsers[i].Roles;
                    foundUser = true;
                    break;
                }
            }
            if (!foundUser)
            {
                throw new ApplicationException("User roles unavailable. User not found");
            }
            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.Email == email)
                {
                    user = fakeUser;
                }
            }
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }

            return user;
        }

        public List<string> SelectUserRoles()
        {
            List<string> roles = new List<string>();

            roles.Add("Admin");
            roles.Add("Logged in");
            roles.Add("Not logged in");

            return roles;
        }

    }
}
