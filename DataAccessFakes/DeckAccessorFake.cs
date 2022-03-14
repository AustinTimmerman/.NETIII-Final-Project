using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class DeckAccessorFake : IDeckAccessor
    {
        private List<Deck> fakeDecks = new List<Deck>();
        private List<DeckCard> fakeDeckCards = new List<DeckCard>();
        CardAccessorFake cardAccessor = new CardAccessorFake();
        private int rowCount = 20;

        public DeckAccessorFake()
        {
            fakeDecks.Add(new Deck()
            {
                DeckID = 999999,
                DeckName = "Green Deck",
                UserID = 999999,
                IsPublic = true
            });
            fakeDecks.Add(new Deck()
            {
                DeckID = 999998,
                DeckName = "Black Deck",
                UserID = 999999,
                IsPublic = true
            });
            fakeDecks.Add(new Deck()
            {
                DeckID = 999997,
                DeckName = "Blue Deck",
                UserID = 999998,
                IsPublic = false
            });
            fakeDecks.Add(new Deck()
            {
                DeckID = 999996,
                DeckName = "Red Deck",
                UserID = 999999,
                IsPublic = false
            });

            fakeDeckCards.Add(new DeckCard()
            {
                DeckID = 999999,
                CardID = 100000,
                CardName = cardAccessor.SelectCardByCardID(100000).CardName,
                ImageID = cardAccessor.SelectCardByCardID(100000).ImageID,
                CardDescription = cardAccessor.SelectCardByCardID(100000).CardDescription,
                CardColorID = cardAccessor.SelectCardByCardID(100000).CardColorID,
                CardConvertedManaCost = cardAccessor.SelectCardByCardID(100000).CardConvertedManaCost,
                CardRarityID = cardAccessor.SelectCardByCardID(100000).CardRarityID,
                CardTypeID = cardAccessor.SelectCardByCardID(100000).CardTypeID,
                HasSecondaryCard = cardAccessor.SelectCardByCardID(100000).HasSecondaryCard,
                CardCount = 40
            });
            fakeDeckCards.Add(new DeckCard()
            {
                DeckID = 999999,
                CardID = 100001,
                CardName = cardAccessor.SelectCardByCardID(100001).CardName,
                ImageID = cardAccessor.SelectCardByCardID(100001).ImageID,
                CardDescription = cardAccessor.SelectCardByCardID(100001).CardDescription,
                CardColorID = cardAccessor.SelectCardByCardID(100001).CardColorID,
                CardConvertedManaCost = cardAccessor.SelectCardByCardID(100001).CardConvertedManaCost,
                CardRarityID = cardAccessor.SelectCardByCardID(100001).CardRarityID,
                CardTypeID = cardAccessor.SelectCardByCardID(100001).CardTypeID,
                HasSecondaryCard = cardAccessor.SelectCardByCardID(100001).HasSecondaryCard,
                CardCount = 3
            });
            //fakeDeckCards.Add(new DeckCard()
            //{
            //    DeckID = 999999,
            //    CardID = 100000,
            //    CardName = cardAccessor.SelectCardByCardID(100000).CardName,
            //    ImageID = cardAccessor.SelectCardByCardID(100000).ImageID,
            //    CardDescription = cardAccessor.SelectCardByCardID(100000).CardDescription,
            //    CardColorID = cardAccessor.SelectCardByCardID(100000).CardColorID,
            //    CardConvertedManaCost = cardAccessor.SelectCardByCardID(100000).CardConvertedManaCost,
            //    CardRarityID = cardAccessor.SelectCardByCardID(100000).CardRarityID,
            //    CardTypeID = cardAccessor.SelectCardByCardID(100000).CardTypeID,
            //    HasSecondaryCard = cardAccessor.SelectCardByCardID(100000).HasSecondaryCard,
            //    CardCount = 1
            //});
            //fakeDeckCards.Add(new DeckCard()
            //{
            //    DeckID = 999999,
            //    CardID = 100001,
            //    CardName = cardAccessor.SelectCardByCardID(100001).CardName,
            //    ImageID = cardAccessor.SelectCardByCardID(100001).ImageID,
            //    CardDescription = cardAccessor.SelectCardByCardID(100001).CardDescription,
            //    CardColorID = cardAccessor.SelectCardByCardID(100001).CardColorID,
            //    CardConvertedManaCost = cardAccessor.SelectCardByCardID(100001).CardConvertedManaCost,
            //    CardRarityID = cardAccessor.SelectCardByCardID(100001).CardRarityID,
            //    CardTypeID = cardAccessor.SelectCardByCardID(100001).CardTypeID,
            //    HasSecondaryCard = cardAccessor.SelectCardByCardID(100001).HasSecondaryCard,
            //    CardCount = 1
            //});
            //fakeDeckCards.Add(new DeckCard()
            //{
            //    DeckID = 999999,
            //    CardID = 100000,
            //    CardName = cardAccessor.SelectCardByCardID(100000).CardName,
            //    ImageID = cardAccessor.SelectCardByCardID(100000).ImageID,
            //    CardDescription = cardAccessor.SelectCardByCardID(100000).CardDescription,
            //    CardColorID = cardAccessor.SelectCardByCardID(100000).CardColorID,
            //    CardConvertedManaCost = cardAccessor.SelectCardByCardID(100000).CardConvertedManaCost,
            //    CardRarityID = cardAccessor.SelectCardByCardID(100000).CardRarityID,
            //    CardTypeID = cardAccessor.SelectCardByCardID(100000).CardTypeID,
            //    HasSecondaryCard = cardAccessor.SelectCardByCardID(100000).HasSecondaryCard,
            //    CardCount = 1
            //});
            //fakeDeckCards.Add(new DeckCard()
            //{
            //    DeckID = 999999,
            //    CardID = 100001,
            //    CardName = cardAccessor.SelectCardByCardID(100001).CardName,
            //    ImageID = cardAccessor.SelectCardByCardID(100001).ImageID,
            //    CardDescription = cardAccessor.SelectCardByCardID(100001).CardDescription,
            //    CardColorID = cardAccessor.SelectCardByCardID(100001).CardColorID,
            //    CardConvertedManaCost = cardAccessor.SelectCardByCardID(100001).CardConvertedManaCost,
            //    CardRarityID = cardAccessor.SelectCardByCardID(100001).CardRarityID,
            //    CardTypeID = cardAccessor.SelectCardByCardID(100001).CardTypeID,
            //    HasSecondaryCard = cardAccessor.SelectCardByCardID(100001).HasSecondaryCard,
            //    CardCount = 1
            //});

            fakeDeckCards.Add(new DeckCard()
            {
                DeckID = 999998,
                CardID = 100000,
                CardName = cardAccessor.SelectCardByCardID(100000).CardName,
                ImageID = cardAccessor.SelectCardByCardID(100000).ImageID,
                CardDescription = cardAccessor.SelectCardByCardID(100000).CardDescription,
                CardColorID = cardAccessor.SelectCardByCardID(100000).CardColorID,
                CardConvertedManaCost = cardAccessor.SelectCardByCardID(100000).CardConvertedManaCost,
                CardRarityID = cardAccessor.SelectCardByCardID(100000).CardRarityID,
                CardTypeID = cardAccessor.SelectCardByCardID(100000).CardTypeID,
                HasSecondaryCard = cardAccessor.SelectCardByCardID(100000).HasSecondaryCard,
                CardCount = 100
            });
        }

        public List<DeckCard> SelectDeckCards(int deckID)
        {
            List<DeckCard> deckCards = new List<DeckCard>();

            try
            {
                for(int i = 0; i < fakeDeckCards.Count; i++)
                {
                    if(fakeDeckCards[i].DeckID == deckID)
                    {
                        //deckCards.Add(fakeDeckCards[i]);
                        deckCards.Add(fakeDeckCards[i]);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return deckCards;
        }

        public List<Deck> SelectDecksByPage(int pageNum)
        {
            List<Deck> cards = new List<Deck>();
            int index = (pageNum - 1) * rowCount;

            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (index > fakeDecks.Count() - 1)
                    {
                        return cards;
                    }
                    if(fakeDecks[index].IsPublic == true)
                    {
                        cards.Add(fakeDecks[index]);
                    }
                    index++;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return cards;
        }

        public Deck SelectDeckByDeckID(int deckID)
        {
            for (int i = 0; i < fakeDecks.Count; i++)
            {
                if (deckID == fakeDecks[i].DeckID)
                {
                    return fakeDecks[i];
                }
            }
            throw new ApplicationException();
        }

        public List<Deck> SelectUserDecksByUserID(int userID, int pageNum)
        {
            List<Deck> decks = new List<Deck>();


            try
            {
                for(int i = 0; i < fakeDecks.Count; i++)
                {
                    if(fakeDecks[i].UserID == userID)
                    {
                        decks.Add(fakeDecks[i]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return decks;
        }

        public int InsertDeck(string deckName, int userID, bool isPublic)
        {
            int rowsAffected = 0;
            try
            {
                Deck newDeck = new Deck()
                {
                    DeckID = fakeDecks[fakeDecks.Count - 1].DeckID - 1,
                    DeckName = deckName,
                    UserID = userID,
                    IsPublic = isPublic
                };

                fakeDecks.Add(newDeck);
                rowsAffected++;
            }
            catch (Exception)
            {

                throw;
            }
            

            return rowsAffected;
        }

        public int UpdateDeck(Deck oldDeck, Deck newDeck)
        {
            int rowsAffected = 0;

            try
            {
                for(int i = 0; i < fakeDecks.Count; i++)
                {
                    if(fakeDecks[i].DeckID == oldDeck.DeckID)
                    {
                        fakeDecks[i] = newDeck;
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        public int DeleteDeck(Deck deck)
        {
            int rowsAffected = 0;

            try
            {
                for(int i = 0; i < fakeDecks.Count; i++)
                {
                    if(fakeDecks[i].DeckID == deck.DeckID)
                    {
                        fakeDecks.RemoveAt(i);
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        public int InsertDeckCard(DeckCard card)
        {
            int rowsAffected = 0;
            
            try
            {
                for(int i = 0; i < fakeDeckCards.Count; i++)
                {
                    if(fakeDeckCards[i].DeckID == card.DeckID && fakeDeckCards[i].CardID == card.CardID)
                    {
                        throw new ApplicationException();
                    }
                }
                fakeDeckCards.Add(card);
                rowsAffected++;
            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public int UpdateDeckCard(DeckCard oldCard, DeckCard newCard)
        {
            int rowsAffected = 0;

            try
            {
                for (int i = 0; i < fakeDeckCards.Count; i++)
                {
                    if(fakeDeckCards[i].CardID == oldCard.CardID && fakeDeckCards[i].DeckID == oldCard.DeckID)
                    {
                        fakeDeckCards[i].CardCount = newCard.CardCount;
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        public int DeleteDeckCard(DeckCard card)
        {
            int rowsAffected = 0;

            try
            {
                for (int i = 0; i < fakeDeckCards.Count; i++)
                {
                    if (fakeDeckCards[i].CardID == card.CardID && fakeDeckCards[i].DeckID == card.DeckID)
                    {
                        fakeDeckCards.RemoveAt(i);
                        rowsAffected++;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }
    }
}
