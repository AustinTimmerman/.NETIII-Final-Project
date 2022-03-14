using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IDeckAccessor
    {
        List<Deck> SelectDecksByPage(int pageNum);
        List<DeckCard> SelectDeckCards(int deckID);
        List<Deck> SelectUserDecksByUserID(int userID, int pageNum);
        int InsertDeck(string deckName, int userID, bool isPublic);
        int UpdateDeck(Deck oldDeck, Deck newDeck);
        int DeleteDeck(Deck deck);
        int InsertDeckCard(DeckCard card);
        int UpdateDeckCard(DeckCard oldCard, DeckCard newCard);
        int DeleteDeckCard(DeckCard card);
    }
}
