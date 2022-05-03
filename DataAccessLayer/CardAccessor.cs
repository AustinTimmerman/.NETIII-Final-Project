﻿using System;
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
    public class CardAccessor : ICardAccessor
    {
        public int DeleteUserCard(UserCard card)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_delete_user_card";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@CardID"].Value = card.CardID;
            cmd.Parameters["@UserID"].Value = card.UserID;

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

        public int InsertUserCard(UserCard card)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_insert_user_card";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@IsOwned", SqlDbType.Bit);
            cmd.Parameters.Add("@IsWishlisted", SqlDbType.Bit);
            cmd.Parameters["@CardID"].Value = card.CardID;
            cmd.Parameters["@UserID"].Value = card.UserID;
            cmd.Parameters["@IsOwned"].Value = card.OwnedCard;
            cmd.Parameters["@IsWishlisted"].Value = card.Wishlisted;

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

        public List<Cards> SelectAllCards(int userID)
        {
            List<Cards> cards = new List<Cards>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_all_cards";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cards.Add(new Cards()
                        {
                            CardID = reader.GetInt32(0),
                            CardName = reader.GetString(1),
                            ImageID = reader.GetInt32(2),
                            CardDescription = reader.GetString(3),
                            CardColorID = reader.GetString(4),
                            CardConvertedManaCost = reader.GetInt32(5),
                            CardTypeID = reader.GetString(6),
                            CardRarityID = reader.GetString(7),
                            HasSecondaryCard = reader.GetBoolean(8),
                            CardSecondaryName = reader.IsDBNull(9) ? null : reader.GetString(9),
                            SecondaryImageID = reader.IsDBNull(10) ? -1 : reader.GetInt32(10),
                            CardSecondaryDescription = reader.IsDBNull(11) ? null : reader.GetString(11),
                            CardSecondaryColorID = reader.IsDBNull(12) ? null : reader.GetString(12),
                            CardSecondaryConvertedManaCost = reader.IsDBNull(13) ? -1 : reader.GetInt32(13),
                            CardSecondaryTypeID = reader.IsDBNull(14) ? null : reader.GetString(14),
                            CardSecondaryRarityID = reader.IsDBNull(15) ? null : reader.GetString(15),
                            IsOwned = reader.IsDBNull(16) ? false : reader.GetBoolean(16),
                            IsWishlisted = reader.IsDBNull(17) ? false : reader.GetBoolean(17)
                        });

                    }
                }
                else
                {
                    throw new ApplicationException("There are no cards!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return cards;
        }

        public Cards SelectCardByCardID(int cardID, int userID)
        {
            Cards cards = new Cards();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_card_by_cardID";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters["@CardID"].Value = cardID;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cards = new Cards()
                        {
                            CardID = cardID,
                            CardName = reader.GetString(0),
                            ImageID = reader.GetInt32(1),
                            CardDescription = reader.GetString(2),
                            CardColorID = reader.GetString(3),
                            CardConvertedManaCost = reader.GetInt32(4),
                            CardTypeID = reader.GetString(5),
                            CardRarityID = reader.GetString(6),
                            HasSecondaryCard = reader.GetBoolean(7),
                            CardSecondaryName = reader.IsDBNull(8) ? null : reader.GetString(8),
                            SecondaryImageID = reader.IsDBNull(9) ? -1 : reader.GetInt32(9),
                            CardSecondaryDescription = reader.IsDBNull(10) ? null : reader.GetString(10),
                            CardSecondaryColorID = reader.IsDBNull(11) ? null : reader.GetString(11),
                            CardSecondaryConvertedManaCost = reader.IsDBNull(12) ? -1 : reader.GetInt32(12),
                            CardSecondaryTypeID = reader.IsDBNull(13) ? null : reader.GetString(13),
                            CardSecondaryRarityID = reader.IsDBNull(14) ? null : reader.GetString(14),
                            IsOwned = reader.IsDBNull(15) ? false : reader.GetBoolean(15),
                            IsWishlisted = reader.IsDBNull(16) ? false : reader.GetBoolean(16)
                        };

                    }
                }
                else
                {
                    throw new ApplicationException("There are no cards!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return cards;
        }

        public List<Cards> SelectCardsByPage(int pageNum)
        {
            List<Cards> cards = new List<Cards>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_cards_by_page";
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
                        cards.Add(new Cards()
                        {
                            CardID = reader.GetInt32(0),
                            CardName = reader.GetString(1),
                            ImageID = reader.GetInt32(2),
                            CardDescription = reader.GetString(3),
                            CardColorID = reader.GetString(4),
                            CardConvertedManaCost = reader.GetInt32(5),
                            CardTypeID = reader.GetString(6),
                            CardRarityID = reader.GetString(7),
                            HasSecondaryCard = reader.GetBoolean(8),
                            CardSecondaryName = reader.IsDBNull(9) ? null : reader.GetString(9),
                            SecondaryImageID = reader.IsDBNull(10) ? -1 : reader.GetInt32(10),
                            CardSecondaryDescription = reader.IsDBNull(11) ? null : reader.GetString(11),
                            CardSecondaryColorID = reader.IsDBNull(12) ? null : reader.GetString(12),
                            CardSecondaryConvertedManaCost = reader.IsDBNull(13) ? -1 : reader.GetInt32(13),
                            CardSecondaryTypeID = reader.IsDBNull(14) ? null : reader.GetString(14),
                            CardSecondaryRarityID = reader.IsDBNull(15) ? null : reader.GetString(15)

                        }) ;
                        
                    }
                }
                else
                {
                    throw new ApplicationException("There are no cards!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return cards;
        }

        public string SelectImageByImageID(int imageID)
        {
            String imageName = null;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_image_by_imageID";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ImageID", SqlDbType.Int);
            cmd.Parameters["@ImageID"].Value = imageID;

            try
            {
                conn.Open();
                imageName = (String)cmd.ExecuteScalar();
            }
            catch (Exception)
            {

                throw;
            }

            return imageName;
        }

        public List<UserCard> SelectUserCardsByUserID(int userID, int pageNum)
        {
            List<UserCard> cards = new List<UserCard>();
            var conn = DBConnection.GetConnection();
            string commandText = @"sp_select_user_cards_by_userID";
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
                        cards.Add(new UserCard()
                        {
                            UserID = userID,
                            CardID = reader.GetInt32(0),
                            CardName = reader.GetString(1),
                            ImageID = reader.GetInt32(2),
                            CardDescription = reader.GetString(3),
                            CardColorID = reader.GetString(4),
                            CardConvertedManaCost = reader.GetInt32(5),
                            CardTypeID = reader.GetString(6),
                            CardRarityID = reader.GetString(7),
                            HasSecondaryCard = reader.GetBoolean(8),
                            CardSecondaryName = reader.IsDBNull(9) ? null : reader.GetString(9),
                            SecondaryImageID = reader.IsDBNull(10) ? -1 : reader.GetInt32(10),
                            CardSecondaryDescription = reader.IsDBNull(11) ? null : reader.GetString(11),
                            CardSecondaryColorID = reader.IsDBNull(12) ? null : reader.GetString(12),
                            CardSecondaryConvertedManaCost = reader.IsDBNull(13) ? -1 : reader.GetInt32(13),
                            CardSecondaryTypeID = reader.IsDBNull(14) ? null : reader.GetString(14),
                            CardSecondaryRarityID = reader.IsDBNull(15) ? null : reader.GetString(15),
                            OwnedCard = reader.GetBoolean(16),
                            Wishlisted = reader.GetBoolean(17)

                        });
                    }
                }
                else
                {
                    //throw new ApplicationException("There are no user cards!");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return cards;
        }

        public int UpdateUserCard(UserCard oldCard, UserCard newCard)
        {
            int rowsAffected = 0;

            var conn = DBConnection.GetConnection();
            string commandText = @"sp_update_user_card";
            var cmd = new SqlCommand(commandText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CardID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@OldIsOwned", SqlDbType.Bit);
            cmd.Parameters.Add("@OldIsWishlisted", SqlDbType.Bit);
            cmd.Parameters.Add("@NewIsOwned", SqlDbType.Bit);
            cmd.Parameters.Add("@NewIsWishlisted", SqlDbType.Bit);
            cmd.Parameters["@CardID"].Value = oldCard.CardID;
            cmd.Parameters["@UserID"].Value = oldCard.UserID;
            cmd.Parameters["@OldIsOwned"].Value = oldCard.OwnedCard;
            cmd.Parameters["@OldIsWishlisted"].Value = oldCard.Wishlisted;
            cmd.Parameters["@NewIsOwned"].Value = newCard.OwnedCard;
            cmd.Parameters["@NewIsWishlisted"].Value = newCard.Wishlisted;

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
