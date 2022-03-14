using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using DataObjects;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class MatchManagerTests
    {
        MatchManager matchManager;

        [TestInitialize]
        public void TestSetup()
        {
            matchManager = new MatchManager(new MatchAccessorFake());
        }

        [TestMethod]
        public void TestRetrieveMatchesByPageReturnsMatches()
        {
            const int pageNum = 1;
            const int expectedCount = 2;
            int actualCount;

            actualCount = matchManager.RetrieveMatchesByPage(pageNum).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveMatchDecksReturnMatchDecks()
        {
            const int matchID = 999999;
            const int expectedCount = 2;
            int actualCount;

            actualCount = matchManager.RetrieveMatchDecksByMatchID(matchID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestRetrieveUserMatchesByUserIDReturnsMatches()
        {
            const int userID = 999999;
            const int pageNum = 1;
            const int expectedCount = 2;
            int actualCount;

            actualCount = matchManager.RetrieveUserMatchesByUserID(userID, pageNum).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestMethod]
        public void TestCreateMatchReturnsTrue()
        {
            const string matchName = "Match new";
            const int userID = 999999;
            const bool isPublic = true;
            const bool expectedResult = true;
            bool actualResult;

            actualResult = matchManager.CreateMatch(matchName, userID, isPublic);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestUpdateMatchReturnsTrue()
        {
            Match oldMatch = new Match()
            {
                MatchID = 999999,
                MatchName = "Match Time",
                UserID = 999999,
                IsPublic = false
            };
            Match newMatch = new Match()
            {
                MatchID = 999999,
                MatchName = "Not Match Time",
                UserID = 999999,
                IsPublic = true
            };
            const bool expectedResult = true;
            bool actualResult;

            actualResult = matchManager.EditMatch(oldMatch, newMatch);

            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void TestUpdateMatchReturnsFalse()
        {
            Match oldMatch = new Match()
            {
                MatchID = 9999999,
                MatchName = "Match Time",
                UserID = 999999,
                IsPublic = false
            };
            Match newMatch = new Match()
            {
                MatchID = 999999,
                MatchName = "Not Match Time",
                UserID = 999999,
                IsPublic = true
            };
            const bool expectedResult = false;
            bool actualResult;

            actualResult = matchManager.EditMatch(oldMatch, newMatch);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestDeleteMatchReturnsTrue()
        {
            Match match = new Match()
            {
                MatchID = 999999,
                MatchName = "Losing Match",
                UserID = 999999,
                IsPublic = true
            };
            const bool expectedResult = true;
            bool actualResult;

            actualResult = matchManager.RemoveMatch(match);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestDeleteMatchReturnsFalse()
        {
            Match match = new Match()
            {
                MatchID = 9999999,
                MatchName = "Losing Match",
                UserID = 999999,
                IsPublic = true
            };
            const bool expectedResult = false;
            bool actualResult;

            actualResult = matchManager.RemoveMatch(match);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestCreateMatchDeckReturnsTrue()
        {
            MatchDeck matchDeck = new MatchDeck()
            {
                MatchID = 999999,
                DeckID = 999990,
                DeckName = "Purple",
                Winner = true
            };

            const bool expectedResult = true;
            bool actualResult;

            actualResult = matchManager.CreateMatchDeck(matchDeck);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateDeckCardThrowsException()
        {
            MatchDeck matchDeck = new MatchDeck()
            {
                MatchID = 999999,
                DeckID = 999999,
                DeckName = "Purple",
                Winner = false
            };

            bool actualResult;

            actualResult = matchManager.CreateMatchDeck(matchDeck);
        }

        [TestMethod]
        public void TestRemoveDeckCardReturnsTrue()
        {
            MatchDeck matchDeck = new MatchDeck()
            {
                MatchID = 999999,
                DeckID = 999999,
                DeckName = "Purple",
                Winner = true
            };

            const bool expectedResult = true;
            bool actualResult;

            actualResult = matchManager.RemoveMatchDeck(matchDeck);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestRemoveDeckCardReturnsFalse()
        {
            MatchDeck matchDeck = new MatchDeck()
            {
                MatchID = 9999999,
                DeckID = 999999,
                DeckName = "Purple",
                Winner = true
            };

            const bool expectedResult = false;
            bool actualResult;

            actualResult = matchManager.RemoveMatchDeck(matchDeck);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
