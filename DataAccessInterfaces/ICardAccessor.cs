using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ICardAccessor
    {
        List<Cards> SelectCardsByPage(int pageNum);
        List<UserCard> SelectUserCardsByUserID(int userID, int pageNum);
        int InsertUserCard(UserCard card);
        int UpdateUserCard(UserCard oldCard, UserCard newCard);
        int DeleteUserCard(UserCard card);
        string SelectImageByImageID(int imageID);
    }
}
