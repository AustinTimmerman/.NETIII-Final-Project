using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IMatchAccessor
    {
        List<Match> SelectMatchesByPage(int pageNum);
        List<MatchDeck> SelectMatchDecksByMatchID(int matchID);
        List<Match> SelectUserMatchesByUserID(int userID, int pageNum);
        int InsertMatch(string matchName, int userID, bool isPublic);
        int UpdateMatch(Match oldMatch, Match newMatch);
        int DeleteMatch(Match match);
        int InsertMatchDeck(MatchDeck matchDeck);
        int DeleteMatchDeck(MatchDeck matchDeck);
    }
}
