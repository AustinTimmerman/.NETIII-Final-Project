using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using DataAccessInterfaces;
using DataAccessFakes;

namespace LogicLayer
{
    public class CardManager : ICardManager
    {
        private ICardAccessor _cardAccessor = null;
        private int pageNumber;

        public CardManager()
        {
            _cardAccessor = new CardAccessor();
        }

        public CardManager(ICardAccessor cardAccessor)
        {
            _cardAccessor = cardAccessor;
        }

        public bool CreateUserCard(UserCard card)
        {
            bool result;

            try
            {
                result = (1 == _cardAccessor.InsertUserCard(card));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool EditUserCard(UserCard oldCard, UserCard newCard)
        {
            bool result;

            try
            {
                result = (1 == _cardAccessor.UpdateUserCard(oldCard, newCard));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool RemoveUserCard(UserCard card)
        {
            bool result = false;

            try
            {
                result = (1 == _cardAccessor.DeleteUserCard(card));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<Cards> RetrieveCardsByPage(int pageNum = 1)
        {
            List<Cards> cards = new List<Cards>();
            pageNumber = pageNum;

            try
            {
                cards = _cardAccessor.SelectCardsByPage(pageNumber);
            }
            catch (Exception)
            {

                throw;
            }

            return cards;
        }

        public string RetrieveImageByImageID(int imageID)
        {
            string imageName = null;

            try
            {
                imageName = _cardAccessor.SelectImageByImageID(imageID);
            }
            catch (Exception)
            {

                throw;
            }

            return imageName;
        }

        public List<UserCard> RetrieveUserCardsByUserID(int userID, int pageNum = 1)
        {
            List<UserCard> userCards = new List<UserCard>();
            pageNumber = pageNum;
            try
            {
                userCards = _cardAccessor.SelectUserCardsByUserID(userID, pageNumber);
            }
            catch (Exception)
            {

                throw;
            }

            return userCards;
        }
    }
}
