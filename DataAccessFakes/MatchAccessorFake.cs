using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class MatchAccessorFake : IMatchAccessor
    {
        private List<Match> fakeMatches = new List<Match>();
        private List<MatchDeck> fakeMatchDecks = new List<MatchDeck>();
        private DeckAccessorFake deckAccessor = new DeckAccessorFake();
        private int rowCount = 20;

        public MatchAccessorFake()
        {
            fakeMatches.Add(new Match()
            {
                MatchID = 999999,
                MatchName = "Match 1",
                UserID = 999999,
                IsPublic = true
            });
            fakeMatches.Add(new Match()
            {
                MatchID = 999998,
                MatchName = "Match 2",
                UserID = 999999,
                IsPublic = true
            });
            fakeMatchDecks.Add(new MatchDeck()
            {
                MatchID = 999999,
                DeckID = 999999,
                DeckName = deckAccessor.SelectDeckByDeckID(999999).DeckName,
                Winner = false
            });
            fakeMatchDecks.Add(new MatchDeck()
            {
                MatchID = 999999,
                DeckID = 999998,
                DeckName = deckAccessor.SelectDeckByDeckID(999998).DeckName,
                Winner = true
            });
            fakeMatchDecks.Add(new MatchDeck()
            {
                MatchID = 999998,
                DeckID = 999997,
                DeckName = deckAccessor.SelectDeckByDeckID(999997).DeckName,
                Winner = false
            });
            fakeMatchDecks.Add(new MatchDeck()
            {
                MatchID = 999998,
                DeckID = 999999,
                DeckName = deckAccessor.SelectDeckByDeckID(999999).DeckName,
                Winner = true
            });
        }

        public int DeleteMatch(Match match)
        {
            int rowsAffected = 0;

            try
            {
                for (int i = 0; i < fakeMatches.Count; i++)
                {
                    if (fakeMatches[i].MatchID == match.MatchID)
                    {
                        fakeMatches.RemoveAt(i);
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

        public int DeleteMatchDeck(MatchDeck matchDeck)
        {
            int rowsAffected = 0;

            try
            {
                for (int i = 0; i < fakeMatchDecks.Count; i++)
                {
                    if (fakeMatchDecks[i].DeckID == matchDeck.DeckID && fakeMatchDecks[i].MatchID == matchDeck.MatchID)
                    {
                        fakeMatchDecks.RemoveAt(i);
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

        public int InsertMatch(string matchName, int userID, bool isPublic)
        {
            int rowsAffected = 0;

            try
            {

                Match newMatch = new Match()
                {
                    MatchID = fakeMatches[fakeMatches.Count - 1].MatchID - 1,
                    MatchName = matchName,
                    UserID = userID,
                    IsPublic = isPublic
                };

                fakeMatches.Add(newMatch);
                rowsAffected++;

            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        public int InsertMatchDeck(MatchDeck matchDeck)
        {
            int rowsAffected = 0;

            try
            {
                for (int i = 0; i < fakeMatchDecks.Count; i++)
                {
                    if (fakeMatchDecks[i].DeckID == matchDeck.DeckID && fakeMatchDecks[i].MatchID == matchDeck.MatchID)
                    {
                        throw new ApplicationException();
                    }
                }
                fakeMatchDecks.Add(matchDeck);
                rowsAffected++;
            }
            catch (Exception)
            {
                throw;
            }

            return rowsAffected;
        }

        public List<MatchDeck> SelectMatchDecksByMatchID(int matchID)
        {
            List<MatchDeck> matchDecks = new List<MatchDeck>();

            try
            {
                for (int i = 0; i < fakeMatchDecks.Count; i++)
                {
                    if (fakeMatchDecks[i].MatchID == matchID)
                    {
                        //deckCards.Add(fakeDeckCards[i]);

                        matchDecks.Add(fakeMatchDecks[i]);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return matchDecks;
        }

        public List<Match> SelectMatchesByPage(int pageNum)
        {
            List<Match> matches = new List<Match>();
            int index = (pageNum - 1) * rowCount;

            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (index > fakeMatches.Count() - 1)
                    {
                        return matches;
                    }
                    if (fakeMatches[index].IsPublic == true)
                    {
                        matches.Add(fakeMatches[index]);
                    }
                    index++;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return matches;
        }

        public List<Match> SelectUserMatchesByUserID(int userID, int pageNum)
        {
            List<Match> userMatches = new List<Match>();
            int index = (pageNum - 1) * rowCount;

            try
            {
                for (int i = 0; i < rowCount; i++)
                {
                    if (index > fakeMatches.Count() - 1)
                    {
                        return userMatches;
                    }
                    if (fakeMatches[index].UserID == userID)
                    {
                        userMatches.Add(fakeMatches[index]);
                    }
                    index++;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return userMatches;
        }

        public int UpdateMatch(Match oldMatch, Match newMatch)
        {
            int rowsAffected = 0;

            try
            {
                for (int i = 0; i < fakeMatches.Count; i++)
                {
                    if (fakeMatches[i].MatchID == oldMatch.MatchID)
                    {
                        fakeMatches[i] = newMatch;
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
