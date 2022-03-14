using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        User SelectUserByEmail(string email);
        List<string> SelectRolesByUserID(int userID);
        bool AuthenticateUserWithUsername(string username);
        bool AuthenticateUserWithEmail(string email);
        int InsertNewUser(string username, string email, string password, List<String> roles);
        List<string> SelectUserRoles();
    }
}
