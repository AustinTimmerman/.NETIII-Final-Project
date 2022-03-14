using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class CardAccessorFake : ICardAccessor
    {
        private List<Cards> fakeCards = new List<Cards>();
        private List<UserCard> fakeUserCards = new List<UserCard>();
        private Dictionary<int, string> fakeImages = new Dictionary<int, string>();
        private int rowCount = 20;

        public CardAccessorFake()
        {
            fakeCards.Add(new Cards()
            {
                CardID = 100000,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false
            });
            fakeCards.Add(new Cards()
            {
                CardID = 100001,
                CardName = "Runo Stormkirk",
                ImageID = 100001,
                CardDescription = "When Runo Stormkirk enters the battlefield, put up to one target creature card from your graveyard on top of your library",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 3,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false
            });
            fakeCards.Add(new Cards()
            {
                CardID = 100003,
                CardName = "Shipwreck Marsh",
                ImageID = 100003,
                CardDescription = "Shipwreck Marsh enters the battlefield tapped unless you control two or more other lands.",
                CardColorID = "Colorless",
                CardConvertedManaCost = 0,
                CardRarityID = "Rare",
                CardTypeID = "Land",
                HasSecondaryCard = false
            });
            fakeCards.Add(new Cards()
            {
                CardID = 100004,
                CardName = "Curse of Leeches",
                ImageID = 100004,
                CardDescription = "As this permanent transforms into Curse of leeches, attach it to a player.",
                CardColorID = "Black",
                CardConvertedManaCost = 3,
                CardRarityID = "Rare",
                CardTypeID = "Enchantment",
                HasSecondaryCard = true,
                CardSecondaryName = "Leeching Lurker",
                SecondaryImageID = 100005,
                CardSecondaryDescription = "Nightbound",
                CardSecondaryColorID = "Black",
                CardSecondaryConvertedManaCost = 0,
                CardSecondaryRarityID = "Rare",
                CardSecondaryTypeID = "Creature"
            });

            fakeUserCards.Add(new UserCard()
            {
                UserID = 999999,
                CardID = 100004,
                CardName = "Curse of Leeches",
                ImageID = 100004,
                CardDescription = "As this permanent transforms into Curse of leeches, attach it to a player.",
                CardColorID = "Black",
                CardConvertedManaCost = 3,
                CardRarityID = "Rare",
                CardTypeID = "Enchantment",
                HasSecondaryCard = true,
                CardSecondaryName = "Leeching Lurker",
                SecondaryImageID = 100005,
                CardSecondaryDescription = "Nightbound",
                CardSecondaryColorID = "Black",
                CardSecondaryConvertedManaCost = 0,
                CardSecondaryRarityID = "Rare",
                CardSecondaryTypeID = "Creature",
                OwnedCard = true,
                Wishlisted = false
            });
            fakeUserCards.Add(new UserCard()
            {
                UserID = 999999,
                CardID = 100003,
                CardName = "Shipwreck Marsh",
                ImageID = 100003,
                CardDescription = "Shipwreck Marsh enters the battlefield tapped unless you control two or more other lands.",
                CardColorID = "Colorless",
                CardConvertedManaCost = 0,
                CardRarityID = "Rare",
                CardTypeID = "Land",
                HasSecondaryCard = false,
                OwnedCard = false,
                Wishlisted = true
            });
            fakeUserCards.Add(new UserCard()
            {
                UserID = 999999,
                CardID = 100001,
                CardName = "Runo Stormkirk",
                ImageID = 100001,
                CardDescription = "When Runo Stormkirk enters the battlefield, put up to one target creature card from your graveyard on top of your library",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 3,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = false
            });

            fakeImages.Add(100000, "Nadier-Agent-of-the-Duskenel.jfif");
            fakeImages.Add(100001, "Prava-of-the-steel-legion.jpg");
        }

        public List<Cards> SelectCardsByPage(int pageNum)
        {
            List<Cards> cards = new List<Cards>();
            int index = (pageNum - 1) * rowCount;

            try
            {
                for(int i = 0; i < rowCount; i++)
                {
                    if(index > fakeCards.Count() - 1) 
                    {
                        return cards;
                    }
                    cards.Add(fakeCards[index]);
                    index++;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return cards;
        }

        public Cards SelectCardByCardID(int cardID)
        {
            for (int i = 0; i < fakeCards.Count; i++)
            {
                if (cardID == fakeCards[i].CardID)
                {
                    return fakeCards[i];
                }
            }
            throw new ApplicationException();
        }

        public List<UserCard> SelectUserCardsByUserID(int userID, int pageNum)
        {
            List<UserCard> userCards = new List<UserCard>();
            if (pageNum > 0)
            {
                int index = (pageNum - 1) * rowCount;
                try
                {

                    for (int i = 0; i < rowCount; i++)
                    {
                        if (index > fakeUserCards.Count() - 1)
                        {
                            return userCards;
                        }
                        if (fakeUserCards[index].UserID == userID)
                        {
                            userCards.Add(fakeUserCards[index]);
                        }
                        index++;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                foreach(UserCard card in fakeUserCards)
                {
                    if (card.UserID == userID)
                    {
                        userCards.Add(card);
                    }
                }
            }

            return userCards;
        }

        public int InsertUserCard(UserCard card)
        {
            int result = 0;

            try
            {
                foreach(UserCard userCard in fakeUserCards)
                {
                    if(card.CardID == userCard.CardID)
                    {
                        return result;
                    }
                }
                fakeUserCards.Add(card);
                result = 1;
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public int UpdateUserCard(UserCard oldCard, UserCard newCard)
        {
            int result = 0;

            try
            {
                for(int i = 0; i < fakeUserCards.Count; i++)
                {
                    if (oldCard.CardID == fakeUserCards[i].CardID)
                    {
                        fakeUserCards[i] = newCard;
                        result = 1;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public int DeleteUserCard(UserCard card)
        {
            int result = 0;

            try
            {
                for (int i = 0; i < fakeUserCards.Count; i++)
                {
                    if (card.CardID == fakeUserCards[i].CardID)
                    {
                        fakeUserCards.RemoveAt(i);
                        result = 1;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public string SelectImageByImageID(int imageID)
        {
            string imageName = null;

            try
            {
                if (fakeImages.ContainsKey(imageID))
                {
                    imageName = fakeImages[imageID];
                }
            }
            catch (Exception)
            {

                throw;
            }

            return imageName;
        }
    }
}
