using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    public interface ICardManager
    {
        List<Cards> RetrieveCardsByPage(int pageNum = 1);
        List<UserCard> RetrieveUserCardsByUserID(int userID, int pageNum = 1);
        bool CreateUserCard(UserCard card);
        bool EditUserCard(UserCard oldCard, UserCard newCard);
        bool RemoveUserCard(UserCard card);
        string RetrieveImageByImageID(int imageID);
    }
}
