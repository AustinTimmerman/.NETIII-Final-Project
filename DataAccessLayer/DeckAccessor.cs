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
    public class DeckAccessor : IDeckAccessor
    {
        public int DeleteDeck(Deck deck)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_delete_deck";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters["@DeckID"].Value = deck.DeckID;

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

        public int DeleteDeckCard(DeckCard card)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_delete_deck_card";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters["@DeckID"].Value = card.DeckID;
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters["@CardID"].Value = card.CardID;

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

        public int InsertDeck(string deckName, int userID, bool isPublic)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_insert_deck";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@IsPublic", SqlDbType.Bit);
            
            cmd.Parameters["@DeckName"].Value = deckName;
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

        public int InsertDeckCard(DeckCard card)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_insert_deck_card";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters.Add("@CardCount", SqlDbType.Int);

            cmd.Parameters["@DeckID"].Value = card.DeckID;
            cmd.Parameters["@CardID"].Value = card.CardID;
            cmd.Parameters["@CardCount"].Value = card.CardCount;

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

        public List<DeckCard> SelectDeckCards(int deckID)
        {
            List<DeckCard> deckCards = new List<DeckCard>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_deck_cards_by_deckID";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters["@DeckID"].Value = deckID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        deckCards.Add(new DeckCard()
                        {
                            DeckID = deckID,
                            CardID = reader.GetInt32(0),
                            CardCount = reader.GetInt32(1),
                            CardName = reader.GetString(2),
                            ImageID = reader.GetInt32(3),
                            CardDescription = reader.GetString(4),
                            CardColorID = reader.GetString(5),
                            CardConvertedManaCost = reader.GetInt32(6),
                            CardTypeID = reader.GetString(7),
                            CardRarityID = reader.GetString(8),
                            HasSecondaryCard = reader.GetBoolean(9),
                            CardSecondaryName = reader.IsDBNull(10) ? null : reader.GetString(10),
                            SecondaryImageID = reader.IsDBNull(11) ? -1 : reader.GetInt32(11),
                            CardSecondaryDescription = reader.IsDBNull(12) ? null : reader.GetString(12),
                            CardSecondaryColorID = reader.IsDBNull(13) ? null : reader.GetString(13),
                            CardSecondaryConvertedManaCost = reader.IsDBNull(14) ? -1 : reader.GetInt32(14),
                            CardSecondaryTypeID = reader.IsDBNull(15) ? null : reader.GetString(15),
                            CardSecondaryRarityID = reader.IsDBNull(16) ? null : reader.GetString(16)
                        });
                    }
                }
                //else
                //{
                    //throw new ApplicationException("There are no cards in this deck!");
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return deckCards;
        }

        public List<Deck> SelectDecksByPage(int pageNum)
        {
            List<Deck> decks = new List<Deck>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_decks_by_page";
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
                        decks.Add(new Deck()
                        {
                            DeckID = reader.GetInt32(0),
                            DeckName = reader.GetString(1),
                            UserID = reader.GetInt32(2),
                            IsPublic = reader.GetBoolean(3)
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

            return decks;
        }

        public List<Deck> SelectUserDecksByUserID(int userID, int pageNum)
        {
            List<Deck> decks = new List<Deck>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_user_decks_by_userID";
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
                        decks.Add(new Deck()
                        {
                            DeckID = reader.GetInt32(0),
                            DeckName = reader.GetString(1),
                            UserID = userID,
                            IsPublic = reader.GetBoolean(2)
                        });
                    }
                }
                //else
                //{
                //    throw new ApplicationException("There are no user decks!");
                //}
            }
            catch (Exception)
            {
                throw;
            }

            return decks;
        }

        public int UpdateDeck(Deck oldDeck, Deck newDeck)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_update_deck";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters["@DeckID"].Value = oldDeck.DeckID;
            cmd.Parameters.Add("@OldDeckName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@OldDeckName"].Value = oldDeck.DeckName;
            cmd.Parameters.Add("@OldIsPublic", SqlDbType.Bit);
            cmd.Parameters["@OldIsPublic"].Value = oldDeck.IsPublic;
            cmd.Parameters.Add("@NewDeckName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@NewDeckName"].Value = newDeck.DeckName;
            cmd.Parameters.Add("@NewIsPublic", SqlDbType.Bit);
            cmd.Parameters["@NewIsPublic"].Value = newDeck.IsPublic;

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

        public int UpdateDeckCard(DeckCard oldCard, DeckCard newCard)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_update_deck_card";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DeckID", SqlDbType.Int);
            cmd.Parameters["@DeckID"].Value = oldCard.DeckID;
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters["@CardID"].Value = oldCard.CardID;
            cmd.Parameters.Add("@OldCardCount", SqlDbType.Int);
            cmd.Parameters["@OldCardCount"].Value = oldCard.CardCount;
            cmd.Parameters.Add("@NewCardCount", SqlDbType.Int);
            cmd.Parameters["@NewCardCount"].Value = newCard.CardCount;

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
