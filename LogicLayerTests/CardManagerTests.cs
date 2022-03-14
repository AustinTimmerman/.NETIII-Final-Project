using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayer;
using DataAccessInterfaces;
using DataAccessFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class CardManagerTests
    {
        private ICardManager cardManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            cardManager = new CardManager(new CardAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveCardsByPageReturnsCards()
        {
            const int pageNum = 1;
            const int expectedCount = 4;
            int actualCount;

            actualCount = cardManager.RetrieveCardsByPage(pageNum).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveUserCardsByUserID()
        {
            const int userID = 999999;
            const int expectedCount = 3;
            int actualCount;

            actualCount = cardManager.RetrieveUserCardsByUserID(userID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestCreateUserCardReturnsTrue()
        {
            UserCard userCard = new UserCard()
            {
                UserID = 999999,
                CardID = 100000,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = false
            };
            const bool expectedResult = true;
            bool actualResult;

            actualResult = cardManager.CreateUserCard(userCard);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateUserCardReturnsFalse()
        {
            UserCard userCard = new UserCard()
            {
                UserID = 999999,
                CardID = 100001,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = false
            };
            const bool expectedResult = false;
            bool actualResult;

            actualResult = cardManager.CreateUserCard(userCard);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestUpdateUserCardReturnsTrue()
        {
            UserCard oldCard = new UserCard()
            {
                UserID = 999999,
                CardID = 100001,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = false
            };
            UserCard newCard = new UserCard()
            {
                UserID = 999999,
                CardID = 100001,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = true
            };
            const bool expectedResult = true;
            bool actualResult;

            actualResult = cardManager.EditUserCard(oldCard, newCard);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestUpdateUserCardReturnsFalse()
        {

            UserCard oldCard = new UserCard()
            {
                UserID = 999999,
                CardID = 9999999,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = false
            };
            UserCard newCard = new UserCard()
            {
                UserID = 999999,
                CardID = 100001,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = true
            };
            const bool expectedResult = false;
            bool actualResult;

            actualResult = cardManager.EditUserCard(oldCard, newCard);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRemoveUserCardReturnsTrue()
        {
            UserCard card = new UserCard()
            {
                UserID = 999999,
                CardID = 100001,
                CardName = "Toxrill, the Corrosive",
                ImageID = 100000,
                CardDescription = "At the beginning of each end step, put a slime counter on each creature you don't control.",
                CardColorID = "Multi-Colored",
                CardConvertedManaCost = 7,
                CardRarityID = "Mythic Rare",
                CardTypeID = "Legendary Creature",
                HasSecondaryCard = false,
                OwnedCard = true,
                Wishlisted = false
            };

            const bool expectedResult = true;
            bool actualResult;

            actualResult = cardManager.RemoveUserCard(card);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
