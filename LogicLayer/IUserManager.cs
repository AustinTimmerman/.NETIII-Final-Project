using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface IUserManager
    {
        User LoginUser(string email, string password);
        bool AuthenticateUser(string email, string passwordHash);
        string HashSha256(string source);
        User GetUserByEmail(String email);
        List<string> GetRolesForUser(int userID);
        //bool AuthenticateUsernameOrEmailNotTaken(String username, string email);
        int InsertNewUser(string username, string email, string passwordHash, List<String> roles);
        List<string> RetrieveUserRoles();
        int RetrieveUserIDFromEmail(string email);
    }
}
