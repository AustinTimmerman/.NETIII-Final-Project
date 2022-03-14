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
    public class DeckManager : IDeckManager
    {
        IDeckAccessor _deckAccessor;

        public DeckManager()
        {
            _deckAccessor = new DeckAccessor();
        }

        public DeckManager(IDeckAccessor deckAccessor)
        {
            _deckAccessor = deckAccessor;
        }

        public bool CreateDeck(string deckName, int userID, bool isPublic)
        {
            bool result;

            try
            {
                result = (1 == _deckAccessor.InsertDeck(deckName, userID, isPublic));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool CreateDeckCard(DeckCard card)
        {
            bool result;

            try
            {
                result = (1 == _deckAccessor.InsertDeckCard(card));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool EditDeck(Deck oldDeck, Deck newDeck)
        {
            bool result;

            try
            {
                result = (1 == _deckAccessor.UpdateDeck(oldDeck, newDeck));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool EditDeckCard(DeckCard oldCard, DeckCard newCard)
        {
            bool result;

            try
            {
                result = (1 == _deckAccessor.UpdateDeckCard(oldCard, newCard));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool RemoveDeck(Deck deck)
        {
            bool result;

            try
            {
                result = (1 == _deckAccessor.DeleteDeck(deck));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool RemoveDeckCard(DeckCard card)
        {
            bool result;

            try
            {
                result = (1 == _deckAccessor.DeleteDeckCard(card));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<DeckCard> RetrieveDeckCards(int deckID)
        {
            List<DeckCard> deckCards = new List<DeckCard>();

            try
            {
                deckCards = _deckAccessor.SelectDeckCards(deckID);
            }
            catch (Exception)
            {

                throw;
            }

            return deckCards;
        }

        public List<Deck> RetrieveDecksByPage(int pageNum = 1)
        {
            List<Deck> decks = new List<Deck>();
            try
            {
                decks = _deckAccessor.SelectDecksByPage(pageNum);
            }
            catch (Exception)
            {
                throw;
            }

            return decks;
        }

        public List<Deck> RetrieveUserDecksByUserID(int userID, int pageNum = 1)
        {
            List<Deck> decks = new List<Deck>();

            try
            {
                decks = _deckAccessor.SelectUserDecksByUserID(userID, pageNum);
            }
            catch (Exception)
            {
                throw;
            }

            return decks;
        }
    }
}
