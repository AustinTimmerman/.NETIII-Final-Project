using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessLayer
{
    public class MatchAccessor : IMatchAccessor
    {
        public int DeleteMatch(Match match)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_delete_match";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatchID", SqlDbType.Int);
            cmd.Parameters["@MatchID"].Value = match.MatchID;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        public int DeleteMatchDeck(MatchDeck matchDeck)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_delete_match_deck";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatchID", SqlDbType.Int);
            cmd.Parameters["@MatchID"].Value = matchDeck.MatchID;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters["@DeckID"].Value = matchDeck.DeckID;
            cmd.Parameters.Add("@Winner", SqlDbType.Bit);
            cmd.Parameters["@Winner"].Value = matchDeck.Winner;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        public int InsertMatch(string matchName, int userID, bool isPublic)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_insert_match";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatchName", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@IsPublic", SqlDbType.Bit);

            cmd.Parameters["@MatchName"].Value = matchName;
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@IsPublic"].Value = isPublic;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        public int InsertMatchDeck(MatchDeck matchDeck)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_insert_match_deck";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatchID", SqlDbType.Int);
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters.Add("@Winner", SqlDbType.Bit);

            cmd.Parameters["@MatchID"].Value = matchDeck.MatchID;
            cmd.Parameters["@DeckID"].Value = matchDeck.DeckID;
            cmd.Parameters["@Winner"].Value = matchDeck.Winner;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        public List<MatchDeck> SelectMatchDecksByMatchID(int matchID)
        {
            List<MatchDeck> matchDecks = new List<MatchDeck>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_match_decks_by_matchID";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatchID", SqlDbType.Int);
            cmd.Parameters["@MatchID"].Value = matchID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        matchDecks.Add(new MatchDeck()
                        {
                            MatchID = matchID,
                            DeckID = reader.GetInt32(0),
                            DeckName = reader.GetString(1),
                            Winner = reader.GetBoolean(2)
                        });
                    }
                }
                //else
                //{
                //    throw new ApplicationException("There are no decks!");
                //}
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
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_matches_by_page";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PageNumber", SqlDbType.Int);
            cmd.Parameters["@PageNumber"].Value = pageNum;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        matches.Add(new Match()
                        {
                            MatchID = reader.GetInt32(0),
                            MatchName = reader.GetString(1),
                            UserID = reader.GetInt32(2),
                            IsPublic = reader.GetBoolean(3)
                        });
                    }
                }
                //else
                //{
                //    throw new ApplicationException("There are no matches!");
                //}
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
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_user_matches_by_userID";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@PageNumber", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@PageNumber"].Value = pageNum;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userMatches.Add(new Match()
                        {
                            MatchID = reader.GetInt32(0),
                            MatchName = reader.GetString(1),
                            UserID = userID,
                            IsPublic = reader.GetBoolean(3)
                        });
                    }
                }
                //else
                //{
                //    throw new ApplicationException("There are no user matches!");
                //}
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

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_update_match";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MatchID", SqlDbType.Int);
            cmd.Parameters["@MatchID"].Value = oldMatch.MatchID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = oldMatch.UserID;

            cmd.Parameters.Add("@OldMatchName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@OldMatchName"].Value = oldMatch.MatchName;
            cmd.Parameters.Add("@OldIsPublic", SqlDbType.Bit);
            cmd.Parameters["@OldIsPublic"].Value = oldMatch.IsPublic;
            cmd.Parameters.Add("@NewMatchName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewMatchName"].Value = newMatch.MatchName;
            cmd.Parameters.Add("@NewIsPublic", SqlDbType.Bit);
            cmd.Parameters["@NewIsPublic"].Value = newMatch.IsPublic;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return rowsAffected;
        }
    }
}
