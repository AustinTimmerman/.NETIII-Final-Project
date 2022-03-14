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
    public class MatchManager : IMatchManager
    {
        IMatchAccessor _matchAccessor;

        public MatchManager()
        {
            _matchAccessor = new MatchAccessor();
        }

        public MatchManager(IMatchAccessor matchAccessor)
        {
            _matchAccessor = matchAccessor;
        }

        public bool CreateMatch(string matchName, int userID, bool isPublic)
        {
            bool result;

            try
            {
                result = (1 == _matchAccessor.InsertMatch(matchName, userID, isPublic));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool CreateMatchDeck(MatchDeck matchDeck)
        {
            bool result;

            try
            {
                result = (1 == _matchAccessor.InsertMatchDeck(matchDeck));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool RemoveMatchDeck(MatchDeck matchDeck)
        {
            bool result;

            try
            {
                result = (1 == _matchAccessor.DeleteMatchDeck(matchDeck));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool EditMatch(Match oldMatch, Match newMatch)
        {
            bool result;

            try
            {
                result = (1 == _matchAccessor.UpdateMatch(oldMatch, newMatch));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool RemoveMatch(Match match)
        {
            bool result;

            try
            {
                result = (1 == _matchAccessor.DeleteMatch(match));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<MatchDeck> RetrieveMatchDecksByMatchID(int matchID)
        {
            List<MatchDeck> matchDecks = new List<MatchDeck>();

            try
            {
                matchDecks = _matchAccessor.SelectMatchDecksByMatchID(matchID);
            }
            catch (Exception)
            {

                throw;
            }

            return matchDecks;
        }

        public List<Match> RetrieveMatchesByPage(int pageNum = 1)
        {
            List<Match> matches = new List<Match>();

            try
            {
                matches = _matchAccessor.SelectMatchesByPage(pageNum);
            }
            catch (Exception)
            {
                throw;
            }

            return matches;

        }

        public List<Match> RetrieveUserMatchesByUserID(int userID, int pageNum = 1)
        {
            List<Match> userMatches = new List<Match>();

            try
            {
                userMatches = _matchAccessor.SelectUserMatchesByUserID(userID, pageNum);
            }
            catch (Exception)
            {

                throw;
            }

            return userMatches;
        }
    }
}
