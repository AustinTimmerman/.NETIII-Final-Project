IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
		  WHERE name = 'mtg_db')
		  
BEGIN 
	DROP DATABASE mtg_db
	print '' print '*** dropping database mtg_db ***'
END 
GO

print '' print '*** creating database mtg_db ***'
GO 
CREATE DATABASE mtg_db
GO

print '' print '*** using database mtg_db ***'
GO
USE [mtg_db]
GO


/* --------------------------------------------------------------------------------------------------- */
/* 												Table Creation */
/* --------------------------------------------------------------------------------------------------- */


/* Images table */
print '' print '*** creating Images table ***'
GO
CREATE TABLE [dbo].[Images] (
	[ImageID]				[int]IDENTITY(100000,1)	NOT NULL,
	[ImageName]				[varchar](MAX)			NOT NULL,
	CONSTRAINT [pk_ImageID] PRIMARY KEY([ImageID]),
	CONSTRAINT [ak_ImageID] UNIQUE([ImageID])
)
GO

/* CardColor table */
print '' print '*** creating CardColor table ***'
GO
CREATE TABLE [dbo].[CardColor] (
	[CardColorID] 			[nvarchar](50)			NOT NULL,
	CONSTRAINT [pk_CardColorID] PRIMARY KEY([CardColorID])
)
GO

/* CardType table */
print '' print '*** creating CardType table ***'
GO
CREATE TABLE [dbo].[CardType] (
	[CardTypeID] 			[nvarchar](50)			NOT NULL,
	CONSTRAINT [pk_CardTypeID] PRIMARY KEY([CardTypeID])
)
GO

/* CardRarity table */
print '' print '*** creating CardRarity table ***'
GO
CREATE TABLE [dbo].[CardRarity] (
	[CardRarityID] 			[nvarchar](50)			NOT NULL,
	CONSTRAINT [pk_CardRarityID] PRIMARY KEY([CardRarityID])
)
GO

/* Cards table */
print '' print '*** creating Cards table ***'
GO
CREATE TABLE [dbo].[Cards] (
	[CardID] 			    [int]IDENTITY(100000,1) NOT NULL,
	[CardName]				[nvarchar](50)			NOT NULL,
    [ImageID]				[int]					NOT NULL,
	[CardDescription]	    [nvarchar](800)			NOT NULL,
	[CardColorID]		    [nvarchar](50)			NOT NULL,
    [CardConvertedManaCost] [int]                   NOT NULL,
	[CardTypeID]		    [nvarchar](50)			NOT NULL,
    [CardRarityID]          [nvarchar](50)          NOT NULL,
    [HasSecondaryCard]		[bit]					NOT NULL,
	[CardSecondaryName]		[nvarchar](50)			NULL,
	[SecondaryImageID]		[int]					NULL,
	[CardSecondaryDescription]		[nvarchar](800) NULL,
	[CardSecondaryColorID]	[nvarchar](50)			NULL,
	[CardSecondaryConvertedManaCost][int]			NULL,
	[CardSecondaryTypeID]	[nvarchar](50)			NULL,
	[CardSecondaryRarityID]	[nvarchar](50)			NULL,
	CONSTRAINT [pk_CardID] PRIMARY KEY([CardID]),
	CONSTRAINT [fk_Cards_Images_ImageID] FOREIGN KEY([ImageID])
		REFERENCES [Images]([ImageID]),
	CONSTRAINT [fk_Cards_CardColor_CardColorID] FOREIGN KEY([CardColorID])
		REFERENCES [CardColor]([CardColorID]),
	CONSTRAINT [fk_Cards_CardType_CardTypeID] FOREIGN KEY([CardTypeID])
		REFERENCES [CardType]([CardTypeID]),
	CONSTRAINT [fk_Cards_CardRarity_CardRarityID] FOREIGN KEY([CardRarityID])
		REFERENCES [CardRarity]([CardRarityID]),
	CONSTRAINT [fk_Cards_Images_CardSecondaryImageID] FOREIGN KEY([SecondaryImageID])
		REFERENCES [Images]([ImageID]),
	CONSTRAINT [fk_Cards_CardColor_CardSecondaryColorID] FOREIGN KEY([CardSecondaryColorID])
		REFERENCES [CardColor]([CardColorID]),
	CONSTRAINT [fk_Cards_CardType_CardSecondaryTypeID] FOREIGN KEY([CardSecondaryTypeID])
		REFERENCES [CardType]([CardTypeID]),
	CONSTRAINT [fk_Cards_CardRarity_CardSecondaryRarityID] FOREIGN KEY([CardSecondaryRarityID])
		REFERENCES [CardRarity]([CardRarityID])
)
GO

/* Users table */
print '' print '*** creating Users table ***'
GO
CREATE TABLE [dbo].[Users] (
	[UserID] 				[int]IDENTITY(100000,1)	NOT NULL,
	[Username] 				[nvarchar](50)			NOT NULL,
	[Email]					[nvarchar](100)			NOT NULL,
	[PasswordHash]			[nvarchar](100)			NOT NULL DEFAULT '9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E',
	[Active]				[bit]					NOT NULL DEFAULT 1,
	CONSTRAINT [pk_UserID] PRIMARY KEY([UserID]),
	CONSTRAINT [ak_Email] UNIQUE([Email])
)
GO

/* Decks table */
print '' print '*** creating Decks table ***'
GO
CREATE TABLE [dbo].[Decks] (
    [DeckID]                [int]IDENTITY(100000,1) NOT NULL,
	[DeckName] 				[nvarchar](50)			NOT NULL,
	[UserID]				[int]					NOT NULL,
    [IsPublic]              [bit]              	 	NOT NULL DEFAULT 1,
	CONSTRAINT [pk_DeckID] PRIMARY KEY([DeckID])
)
GO

/* DeckCards table */
print '' print '*** creating DeckCards table ***'
GO
CREATE TABLE [dbo].[DeckCards] (
	[DeckID] 				[int]         			NOT NULL,
	[CardID]				[int]					NOT NULL,
	[CardCount]				[int]					NOT NULL 	
	CONSTRAINT [fk_DeckCards_Decks_DeckID] FOREIGN KEY([DeckID])
		REFERENCES [Decks]([DeckID]) ON DELETE CASCADE,
	CONSTRAINT [fk_DeckCards_Cards_CardID] FOREIGN KEY([CardID])
		REFERENCES [Cards]([CardID]),
	CONSTRAINT [pk_DeckCardID] PRIMARY KEY([DeckID], [CardID])
)
GO

/* Matches table */
print '' print '*** creating Matches table ***'
GO
CREATE TABLE [dbo].[Matches] (
	[MatchID] 				[int]IDENTITY(100000,1) NOT NULL,
    [MatchName]             [nvarchar](100)         NOT NULL,
	[UserID]				[int]					NOT NULL,
    [IsPublic]              [bit]               	NOT NULL DEFAULT 1,
	CONSTRAINT [pk_MatchID] PRIMARY KEY([MatchID]),
	CONSTRAINT [fk_Matches_User_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID])
)
GO

/* MatchDecks table */
print '' print '*** creating MatchDecks table ***'
GO
CREATE TABLE [dbo].[MatchDecks] (
	[MatchID] 				[int]         			NOT NULL,
	[DeckID]				[int]          			NOT NULL,
    [Winner]                [bit]               	NOT NULL,
	CONSTRAINT [fk_MatchDecks_Matches_MatchID] FOREIGN KEY([MatchID])
		REFERENCES [Matches]([MatchID]) ON DELETE CASCADE,
	CONSTRAINT [fk_MatchDecks_Decks_DeckID] FOREIGN KEY([DeckID])
		REFERENCES [Decks]([DeckID]) ON DELETE CASCADE,
	CONSTRAINT [pk_MatchDeckID] PRIMARY KEY([MatchID], [DeckID])
)
GO

/* UserCards table */
print '' print '*** creating UserCards table ***'
GO
CREATE TABLE [dbo].[UserCards] (
	[CardID] 				[int]         			NOT NULL,
	[UserID]				[int]          			NOT NULL,
    [IsOwned]               [bit]	            	NOT NULL DEFAULT 0,
	[IsWishlisted]			[bit]					NOT NULL DEFAULT 0,
	CONSTRAINT [fk_UserCards_Cards_CardID] FOREIGN KEY([CardID])
		REFERENCES [Cards]([CardID]),
	CONSTRAINT [fk_UserCards_Users_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID]),
	CONSTRAINT [pk_CardUserID] PRIMARY KEY([CardID], [UserID])
)
GO

/* Roles table */
print '' print '*** creating Roles table ***'
GO
CREATE TABLE [dbo].[Roles] (
	[RoleID] 				[nvarchar](50)			NOT NULL DEFAULT 'Not Logged In User',
	[RoleDescription]		[nvarchar](200)			NOT NULL,
	CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID])
)
GO

/* UsersRoles table */
print '' print '*** creating UsersRoles table ***'
GO
CREATE TABLE [dbo].[UserRoles] (
	[UserID] 				[int]					NOT NULL,
	[RoleID]				[nvarchar](50)			NOT NULL,
	CONSTRAINT [fk_UserRoles_Users_UserID] FOREIGN KEY([UserID])
		REFERENCES [Users]([UserID]),
	CONSTRAINT [fk_UserRoles_Roles_RoleID] FOREIGN KEY([RoleID])
		REFERENCES [Roles]([RoleID]),
	CONSTRAINT [pk_UserRoleID] PRIMARY KEY([UserID], [RoleID])
)
GO


/* --------------------------------------------------------------------------------------------------- */
/* 												Inserts */
/* --------------------------------------------------------------------------------------------------- */


/* Images */
print '' print '*** adding Images records'
GO
INSERT INTO [dbo].[Images]
	(
		[ImageName]
	)
VALUES 
	("Nadier-Agent-of-the-Duskenel.jfif"),
	("Prava-of-the-steel-legion.jpg"),
	("Plains.jpg"),
	("Akki-coalflinger.jpg"),
	("Ramunap-ruins.jpg"),
	("Trudge-garden.jpg"),
	("Forging-the-tyrite-sword.jpg"),
	("Heirs-of-stromkirk.jpg"),
	("Read-the-bones.jpg"),
	("Knight-of-the-white-orchid.jpg"),
	("Victimize.jpg"),
	("Dark-impostor.jpg"),
	("Sun-ce-young-conquerer.jpg"),
	("Rousing-refrain.jpg"),
	("Orcish-artillery.jpg"),
	("Feral-prowler.jpg"),
	("Swamp.jpg"),
	("Fireborn-knight.jpg"),
	("Shady-traveler.jpg"),
	("Stalking-predator.jpg"),
	("Booby-trap.jpg"),
	("Gratuitous-Violence.jpg"),
	("Disappear.jpg"),
	("Dismal-backwater.jpg"),
	("Warded-battlements.jpg"),
	("Cabal-ritual.jpg"),
	("Bandage.jpg"),
	("Dark-confidant.jpg"),
	("Big-game-hunter.jpg"),
	("Oracle-en-vec.jpg"),
	("Scryb-sprites.jpg")
GO

/* CardColor */
print '' print '*** adding CardColor records'
GO
INSERT INTO [dbo].[CardColor]
	(
		[CardColorID]
	)
VALUES 
	("Black"),
	("White"),
	("Red"),
	("Green"),
	("Blue"),
	("Colorless"),
	("Hybrid"),
	("Multi-colored")
GO

/* CardType */
print '' print '*** adding CardType records'
GO
INSERT INTO [dbo].[CardType]
	(
		[CardTypeID]
	)
VALUES 
	("Instant"),
	("Sorcery"),
	("Artifact"),
	("Enchantment"),
	("Creature"),
	("Land"),
	("Planeswalker"),
	("Legendary Planeswalker"),
	("Legendary Instant"),
	("Legendary Sorcery"),
	("Legendary Artifact"),
	("Legendary Enchantment"),
	("Legendary Creature"),
	("Legendary Land")
GO

/* CardRarity */
print '' print '*** adding CardRarity records'
GO
INSERT INTO [dbo].[CardRarity]
	(
		[CardRarityID]
	)
VALUES 
	("Common"),
	("Uncommon"),
	("Rare"),
	("Mythic Rare")
GO

/* Cards */
print '' print '*** adding Cards records'
GO
INSERT INTO [dbo].[Cards]
	(
		[CardName],				
		[ImageID],						
		[CardDescription],	    
		[CardColorID],		    
		[CardConvertedManaCost],    
		[CardTypeID],		    
		[CardRarityID],
		[HasSecondaryCard],
		[CardSecondaryName],		
		[SecondaryImageID],			
		[CardSecondaryDescription],		
		[CardSecondaryColorID],	
		[CardSecondaryConvertedManaCost],
		[CardSecondaryTypeID],	
		[CardSecondaryRarityID]	
	)
VALUES 
	("Nadier, Agent of the Duskenel", 100000, "Whenever a token you control leaves the battlefield, put a +1/+1 counter on Nadier, Agent of the Duskenel. When Nadier leaves the battlefield, create a number of 1/1 green Elf Warrior creature tokens equal to its power. Partner (You can have two commanders if both have partner.)", "Black", 6, "Legendary Creature", "Rare", 0, null, null, null, null, null, null, null),
	("Prava of the Steel Legion", 100001, "As long as it's your turn, creature tokens you control get +1/+4. 3+White: Create a 1/1 white Soldier creature token. Partner (You can have two commanders if both have partner.)", "White", 3, "Legendary Creature", "Uncommon", 0, null, null, null, null, null, null, null),
	("Plains", 100002, "", "Colorless", 0, "Land", "Common", 0, null, null, null, null, null, null, null),
	("Akki Coalflinger", 100003, "First strike. Red, Tap: Attacking creatures gain first strike until end of turn.", "Red", 3, "Creature", "Uncommon", 0, null, null, null, null, null, null, null),
	("Ramunap Ruins", 100004, "Tap: Add Colorless. Tap, Pay 1 life: Add Red. 2+Red+Red, Tap, Sacrifice a Desert: Ramunap Ruins deals 2 damage to each opponent.", "Colorless", 0, "Land", "Uncommon", 0, null, null, null, null, null, null, null),
	("Trudge Garden", 100005, "Whenever you gain life, you may pay 2. If you do, create a 4/4 green Fungus Beast creature token with trample.", "Green", 3, "Enchantment", "Rare", 0, null, null, null, null, null, null, null),
	("Forging the Tyrite Sword", 100006, "As this Saga enters and after your draw step, add a lore counter. Sacrifice after III.) I, II — Create a Treasure token. III — Search your library for a card named Halvar, God of Battle or an Equipment card, reveal it, put it into your hand, then shuffle.", "Multi-colored", 3, "Enchantment", "Uncommon", 0, null, null, null, null, null, null, null),
	("Heirs of Stromkirk", 100007, "Intimidate (This creature can't be blocked except by artifact creatures and/or creatures that share a color with it.) Whenever Heirs of Stromkirk deals combat damage to a player, put a +1/+1 counter on it.", "Red", 4, "Creature", "Common", 0, null, null, null, null, null, null, null),
	("Read the Bones", 100008, "Scry 2, then draw two cards. You lose 2 life.", "Black", 3, "Sorcery", "Common", 0, null, null, null, null, null, null, null),
	("Knight of the White Orchid", 100009, "First strike. When Knight of the White Orchid enters the battlefield, if an opponent controls more lands than you, you may search your library for a Plains card, put it onto the battlefield, then shuffle.", "White", 2, "Creature", "Rare", 0, null, null, null, null, null, null, null),
	("Victimize", 100010, "Choose two target creature cards in your graveyard. Sacrifice a creature. If you do, return the chosen cards to the battlefield tapped.", "Black", 3, "Sorcery", "Uncommon", 0, null, null, null, null, null, null, null),
	("Dark Impostor", 100011, "4+Black+Black: Exile target creature and put a +1/+1 counter on Dark Impostor. Dark Impostor has all activated abilities of all creature cards exiled with it.", "Black", 3, "Creature", "Rare", 0, null, null, null, null, null, null, null),
	("Sun Ce, Young Conquerer", 100012, "Horsemanship (This creature can't be blocked except by creatures with horsemanship.) When Sun Ce, Young Conquerer enters the battlefield, you may return target creature to its owner's hand.", "Blue", 5, "Creature", "Uncommon", 0, null, null, null, null, null, null, null),
	("Rousing Refrain", 100013, "Add Red for each card in target opponent's hand. Until end of turn, you don't lose this mana as steps and phases end. Exile Rousing Refrain with three time counters on it. Suspend 3—1Red (Rather than cast this card from your hand, you may pay Red and exile it with three time counters on it. At the beginning of your upkeep, remove a time counter. When the last is removed, cast it without paying its mana cost.)", "Red", 5, "Sorcery", "Rare", 0, null, null, null, null, null, null, null),
	("Orcish Artillery", 100014, "Tap: Orcish Artillery deals 2 damage to any target and 3 damage to you.", "Red", 3, "Creature", "Uncommon", 0, null, null, null, null, null, null, null),
	("Feral Prowler", 100015, "When Feral Prowler dies, draw a card.", "Green", 2, "Creature", "Common", 0, null, null, null, null, null, null, null),
	("Swamp", 100016, "", "Colorless", 0, "Land", "Common", 0, null, null, null, null, null, null, null),
	("Fireborn Knight", 100017, "Double strike. Red or White, Red or White, Red or White, Red or White: Fireborn Knight gets +1/+1 until end of turn.", "Multi-colored", 4, "Creature", "Uncommon", 0, null, null, null, null, null, null, null),
	("Shady Traveler", 100018, "Menace (This creature can't be blocked except by two or more creatures.) Daybound (If a player casts no spells during their own turn, it becomes night next turn.)", "Black", 3, "Creature", "Common", 1, "Stalking Predator", 100019, "Menace (This creature can't be blocked except by two or more creatures.) Nightbound (If a player casts at least two spells during their own turn, it becomes day next turn.)", "Black", 0, "Creature", "Common"),
	("Booby Trap", 100020, "As Booby Trap enters the battlefield, choose an opponent and a card name other than a basic land card name. The chosen player reveals each card they draw. When the chosen player draws a card with the chosen name, sacrifice Booby Trap. If you do, Booby Trap deals 10 damage to that player.", "Colorless", 6, "Artifact", "Rare", 0, null, null, null, null, null, null, null),
	("Gratuitous Violence", 100021, "If a creature you control would deal damage to a permanent or player, it deals double that damage to that permanent or player instead.", "Red", 5, "Enchantment", "Rare", 0, null, null, null, null, null, null, null),
	("Disappear", 100022, "Enchant creature. Blue: Return enchanted creature and Disappear to their owners' hands.", "Blue", 4, "Enchantment", "Uncommon", 0, null, null, null, null, null, null, null),
	("Dismal Backwater", 100023, "Dismal Backwater enters the battlefield tapped. When Dismal Backwater enters the battlefield, you gain 1 life. Tap: Add Blue or Black.", "Colorless", 0, "Land", "Common", 0, null, null, null, null, null, null, null),
	("Warded Battlements", 100024, "Defender (This creature can't attack.) Attacking creatures you control get +1/+0.", "White", 3, "Creature", "Common", 0, null, null, null, null, null, null, null),
	("Cabal Ritual", 100025, "Add Black+Black+Black. Threshold — Add Black+Black+Black+Black+Black instead if seven or more cards are in your graveyard.", "Black", 2, "Instant", "Common", 0, null, null, null, null, null, null, null),
	("Bandage", 100026, "Prevent the next 1 damage that would be dealt to any target this turn. Draw a card.", "White", 1, "Instant", "Common", 0, null, null, null, null, null, null, null),
	("Dark Confidant", 100027, "At the beginning of your upkeep, reveal the top card of your library and put that card into your hand. You lose life equal to its mana value.", "Black", 2, "Creature", "Mythic Rare", 0, null, null, null, null, null, null, null),
	("Big Game Hunter", 100028, "When Big Game Hunter enters the battlefield, destroy target creature with power 4 or greater. It can't be regenerated. Madness Black (If you discard this card, discard it into exile. When you do, cast it for its madness cost or put it into your graveyard.)", "Black", 3, "Creature", "Common", 0, null, null, null, null, null, null, null),
	("Oracle en-Vec", 100029, "Tap: Target opponent chooses any number of creatures they control. During that player's next turn, the chosen creatures attack if able, and other creatures can't attack. At the beginning of that turn's end step, destroy each of the chosen creatures that didn't attack this turn. Activate only during your turn.", "White", 2, "Creature", "Rare", 0, null, null, null, null, null, null, null),
	("Scryb Sprites", 100030, "Flying", "Green", 1, "Creature", "Common", 0, null, null, null, null, null, null, null)
	
GO

/* Users */
print '' print '*** adding Users records'
GO
INSERT INTO [dbo].[Users]
	(
	[Username],
	[Email],
	[PasswordHash],
	[Active]
	
	)
VALUES 
	/*("austintimmerman20@gmail.com", "austintimmerman20@gmail.com", "AD5ZJjSp/16enIGUvDIV+6m2Xz0Zuxcq84XWamvBVpXCPe+CWIwGzADidjiypBibVw==", 1),*/
	("austintimmerman20@gmail.com", "austintimmerman20@gmail.com", "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8", 1),
	
	("jim.glasgow@kirkwood.edu", "jim.glasgow@kirkwood.edu", "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8", 1)
GO

/* Decks */
print '' print '*** adding Decks records'
GO
INSERT INTO [dbo].[Decks]
	(
		[DeckName],
		[UserID],
		[IsPublic]
	)
VALUES 
	("Token Deck", "100000", 1),
	("Spawn Deck", "100000", 0),
	("Curse Deck", "100000", 1)
GO

/* DeckCards */
print '' print '*** adding DeckCards records'
GO
INSERT INTO [dbo].[DeckCards]
	(
		[DeckID],
		[CardID],
		[CardCount]
	)
VALUES 
	(100000, 100000, 1),
	(100000, 100002, 2),
	(100000, 100005, 5),
	(100001, 100006, 4)
GO

/* Matches */
print '' print '*** adding Matches records'
GO
INSERT INTO [dbo].[Matches]
	(
		[MatchName],
		[UserID],
		[IsPublic]
	)
VALUES 
	("Match 1", 100000, 1),
	("Bad Match", 100000, 0)
GO

/* MatchDecks */
print '' print '*** adding MatchDecks records'
GO
INSERT INTO [dbo].[MatchDecks]
	(
		[MatchID],
		[DeckID],
		[Winner]
	)
VALUES 
	(100000, 100000, 1),
	(100000, 100001, 0)
GO

/* UserCards */
print '' print '*** adding UserCards records'
GO
INSERT INTO [dbo].[UserCards]
	(
		[CardID],
		[UserID],
		[IsOwned],
		[IsWishlisted]
	)
VALUES 
	(100000, 100000, 0, 1)
GO

/* Roles */
print '' print '*** adding Roles records'
GO
INSERT INTO [dbo].[Roles]
	(
		[RoleID],
		[RoleDescription]
	)
VALUES 
	("Not logged in", "User is not logged in"),
	("Logged in", "User is logged in"),
	("Admin", "User is an admin")
GO

/* UserRoles */
print '' print '*** adding UserRoles records'
GO
INSERT INTO [dbo].[UserRoles]
	(
		[UserID],
		[RoleID]
	)
VALUES 
	(100000, "Admin")
GO


/* --------------------------------------------------------------------------------------------------- */
/* 												Stored Procedures */
/* --------------------------------------------------------------------------------------------------- */


print '' print '*** Creating sp_select_all_roles'
GO
CREATE PROCEDURE [sp_select_all_roles]
AS
BEGIN
	SELECT [RoleID]
	FROM [dbo].[Roles]
	ORDER BY [RoleID]
END
GO

print '' print '*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS 
	BEGIN 
		SELECT COUNT([UserID]) AS 'Authenticated'
		FROM [Users]
		WHERE @Email = [Email]
			AND @PasswordHash = [PasswordHash]
			AND [Active] = 1
	END
GO

print '' print '*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email			[nvarchar](100)
)
AS 
	BEGIN 
		SELECT 	[UserID], [Username], [Email], [Active]
		FROM 	[Users]
		WHERE 	@Email = [Email]
	END
GO

print '' print '*** creating sp_select_user_roles_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_roles_by_userID]
(
	@UserID			[int]
)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM 	[UserRoles]
		WHERE 	@UserID = [UserID]
	END
GO

print '' print '*** creating sp_insert_user ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user]
(
	@Username 		[nvarchar](50),
	@Email			[nvarchar](100),
	@PasswordHash	[nvarchar](100)
)
AS
	BEGIN
		INSERT INTO	[dbo].[Users]
		(
			[Username],
			[Email],
			[PasswordHash]
		)
		VALUES
		(
			@Username,
			@Email,
			@PasswordHash
		)
	END
GO

print '' print '*** creating sp_authenticate_username ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_username]
(
	@Username		[nvarchar](50)
)
AS
	BEGIN
		SELECT 
			COUNT([Username]) AS 'Total Usernames'
		FROM [dbo].[Users]
		WHERE [Username] = @Username AND Active = 1
	END
GO

print '' print '*** creating sp_authenticate_email ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_email]
(
	@Email		[nvarchar](100)
)
AS
	BEGIN
		SELECT 
			COUNT([Email]) AS 'Total Emails'
		FROM [dbo].[Users]
		WHERE [Email] = @Email AND Active = 1
	END
GO

print '' print '*** creating sp_select_all_cards ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_cards]
(
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 
			[Cards].[CardID],			    
			[CardName],				
			[ImageID],					
			[CardDescription],	    
			[CardColorID],		    
			[CardConvertedManaCost],     
			[CardTypeID],		    
			[CardRarityID],      
			[HasSecondaryCard],			
			[CardSecondaryName],		
			[SecondaryImageID],				
			[CardSecondaryDescription],	
			[CardSecondaryColorID],	
			[CardSecondaryConvertedManaCost],
			[CardSecondaryTypeID],	
			[CardSecondaryRarityID],
			[IsOwned],
			[IsWishlisted]
		FROM [dbo].[Cards]
		LEFT OUTER JOIN [UserCards] ON [Cards].[CardID] = [UserCards].[CardID] AND @UserID = [UserCards].[UserID]
		
	END
GO

print '' print '*** creating sp_select_cards_by_page ***'
GO
CREATE PROCEDURE [dbo].[sp_select_cards_by_page]
(
	@PageNumber		int
)
AS
	BEGIN
		SELECT 
			[CardID],			    
			[CardName],				
			[ImageID],					
			[CardDescription],	    
			[CardColorID],		    
			[CardConvertedManaCost],     
			[CardTypeID],		    
			[CardRarityID],      
			[HasSecondaryCard],			
			[CardSecondaryName],		
			[SecondaryImageID],				
			[CardSecondaryDescription],	
			[CardSecondaryColorID],	
			[CardSecondaryConvertedManaCost],
			[CardSecondaryTypeID],	
			[CardSecondaryRarityID]
		FROM [dbo].[Cards]
		ORDER BY [CardName]
		OFFSET (@PageNumber-1)*20 ROWS
		FETCH NEXT 20 ROWS ONLY
	END
GO

print '' print '*** creating sp_select_card_by_cardID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_card_by_cardID]
(
	@CardID 	[int],
	@UserID 	[int]
)
AS
	BEGIN
		SELECT 		    
			[CardName],				
			[ImageID],					
			[CardDescription],	    
			[CardColorID],		    
			[CardConvertedManaCost],     
			[CardTypeID],		    
			[CardRarityID],      
			[HasSecondaryCard],			
			[CardSecondaryName],		
			[SecondaryImageID],				
			[CardSecondaryDescription],	
			[CardSecondaryColorID],	
			[CardSecondaryConvertedManaCost],
			[CardSecondaryTypeID],	
			[CardSecondaryRarityID],
			[IsOwned],
			[IsWishlisted]
		FROM [dbo].[Cards]
		LEFT OUTER JOIN [UserCards] ON [Cards].[CardID] = [UserCards].[CardID] AND @UserID = [UserCards].[UserID]
		WHERE [Cards].[CardID] = @CardID
		
	END
GO

print '' print '*** creating sp_select_all_decks ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_decks]
AS
	BEGIN
		SELECT 
			[DeckID],
			[DeckName], 				
			[Email],				
			[IsPublic]    
		FROM [dbo].[Decks]
		LEFT OUTER JOIN [Users] ON [Users].[UserID] = [Decks].[UserID]
		WHERE [IsPublic] = 1
			ORDER BY [DeckName]
	END
GO

print '' print '*** creating sp_select_decks_by_page ***'
GO
CREATE PROCEDURE [dbo].[sp_select_decks_by_page]
(
	@PageNumber		int
)
AS
	BEGIN
		SELECT 
			[DeckID],
			[DeckName], 				
			[UserID],				
			[IsPublic]    
		FROM [dbo].[Decks]
		WHERE [IsPublic] = 1
			ORDER BY [DeckName]
			OFFSET (@PageNumber-1)*20 ROWS
			FETCH NEXT 20 ROWS ONLY
	END
GO

print '' print '*** creating sp_select_deck_by_deckID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_deck_by_deckID]
(
	@DeckID		int
)
AS
	BEGIN
		SELECT 
			[DeckName], 				
			[UserID],				
			[IsPublic]    
		FROM [dbo].[Decks]
		WHERE @DeckID = [DeckID]
	END
GO

print '' print '*** creating sp_select_deck_cards_by_deckID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_deck_cards_by_deckID]
(
	@DeckID			int
)
AS
	BEGIN
		SELECT 
			/*[DeckCards].[DeckID],*/
			[Cards].[CardID],
			[DeckCards].[CardCount],
			[Cards].[CardName],				
			[Cards].[ImageID],					
			[Cards].[CardDescription],	    
			[Cards].[CardColorID],		    
			[Cards].[CardConvertedManaCost],     
			[Cards].[CardTypeID],		    
			[Cards].[CardRarityID],      
			[Cards].[HasSecondaryCard],			
			[Cards].[CardSecondaryName],		
			[Cards].[SecondaryImageID],				
			[Cards].[CardSecondaryDescription],	
			[Cards].[CardSecondaryColorID],	
			[Cards].[CardSecondaryConvertedManaCost],
			[Cards].[CardSecondaryTypeID],	
			[Cards].[CardSecondaryRarityID]
		FROM [dbo].[DeckCards]
		JOIN [Cards] ON [DeckCards].[CardID] = [Cards].[CardID]
		WHERE @DeckID = [DeckID]
		ORDER BY [DeckCards].[CardID]
	END
GO

print '' print '*** creating sp_select_all_matches ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_matches]
AS
	BEGIN
		SELECT 
			[MatchID],
			[MatchName],          
			[Email],				
			[IsPublic]
		FROM [dbo].[Matches]
		LEFT OUTER JOIN [Users] ON [Users].[UserID] = [Matches].[UserID]
		WHERE [IsPublic] = 1
			ORDER BY [MatchName]
	END
GO

print '' print '*** creating sp_select_matches_by_page ***'
GO
CREATE PROCEDURE [dbo].[sp_select_matches_by_page]
(
	@PageNumber		int
)
AS
	BEGIN
		SELECT 
			[MatchID],
			[MatchName],          
			[UserID],				
			[IsPublic]
		FROM [dbo].[Matches]
		WHERE [IsPublic] = 1
			ORDER BY [MatchName]
			OFFSET (@PageNumber-1)*20 ROWS
			FETCH NEXT 20 ROWS ONLY
	END
GO

print '' print '*** creating sp_select_match_by_matchID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_match_by_matchID]
(
	@MatchID		int
)
AS
	BEGIN
		SELECT 
			[MatchName],          
			[UserID],				
			[IsPublic]
		FROM [dbo].[Matches]
		WHERE @MatchID = [MatchID]
	END
GO

print '' print '*** creating sp_select_match_decks_by_matchID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_match_decks_by_matchID]
(
	@MatchID		int
)
AS
	BEGIN
		SELECT 
			[MatchDecks].[DeckID],
			[Decks].[DeckName],
			[MatchDecks].[Winner]          
		FROM [dbo].[MatchDecks] JOIN [Decks] ON [MatchDecks].[DeckID] = [Decks].[DeckID]
		WHERE [MatchID] = @MatchID
		ORDER BY [Winner]
	END
GO

print '' print '*** creating sp_select_user_cards_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_cards_by_userID]
(
	@UserID			int,
	@PageNumber		int
)
AS
	BEGIN
		IF(@PageNumber > 0)
		SELECT 
			[Cards].[CardID],
			[Cards].[CardName],				
			[Cards].[ImageID],					
			[Cards].[CardDescription],	    
			[Cards].[CardColorID],		    
			[Cards].[CardConvertedManaCost],     
			[Cards].[CardTypeID],		    
			[Cards].[CardRarityID],      
			[Cards].[HasSecondaryCard],			
			[Cards].[CardSecondaryName],		
			[Cards].[SecondaryImageID],				
			[Cards].[CardSecondaryDescription],	
			[Cards].[CardSecondaryColorID],	
			[Cards].[CardSecondaryConvertedManaCost],
			[Cards].[CardSecondaryTypeID],	
			[Cards].[CardSecondaryRarityID],		
			[UserCards].[IsOwned],              
			[UserCards].[IsWishlisted]		
		FROM [UserCards] 
		JOIN [Cards] ON [Cards].[CardID] = [UserCards].[CardID]
		WHERE [UserID] = @UserID
		ORDER BY [CardName]
			OFFSET (@PageNumber-1)*20 ROWS
			FETCH NEXT 20 ROWS ONLY
		ELSE
		SELECT
			[Cards].[CardID],
			[Cards].[CardName],				
			[Cards].[ImageID],					
			[Cards].[CardDescription],	    
			[Cards].[CardColorID],		    
			[Cards].[CardConvertedManaCost],     
			[Cards].[CardTypeID],		    
			[Cards].[CardRarityID],      
			[Cards].[HasSecondaryCard],			
			[Cards].[CardSecondaryName],		
			[Cards].[SecondaryImageID],				
			[Cards].[CardSecondaryDescription],	
			[Cards].[CardSecondaryColorID],	
			[Cards].[CardSecondaryConvertedManaCost],
			[Cards].[CardSecondaryTypeID],	
			[Cards].[CardSecondaryRarityID],		
			[UserCards].[IsOwned],              
			[UserCards].[IsWishlisted]
		FROM [UserCards] 
		JOIN [Cards] ON [Cards].[CardID] = [UserCards].[CardID]
		WHERE [UserID] = @UserID
		ORDER BY [CardName]
	END
GO

print '' print '*** creating sp_select_user_decks_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_decks_by_userID]
(
	@UserID			int,
	@PageNumber		int
)
AS
	BEGIN
		SELECT 
			[DeckID],
			[DeckName],
			[IsPublic]    
		FROM [dbo].[Decks]
		WHERE @UserID = [UserID]
		ORDER BY [DeckName]
			OFFSET (@PageNumber-1)*20 ROWS
			FETCH NEXT 20 ROWS ONLY
	END
GO

print '' print '*** creating sp_select_user_matches_by_userID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_matches_by_userID]
(
	@UserID			int,
	@PageNumber		int
)
AS
	BEGIN
		SELECT 
			[MatchID],
			[MatchName],          
			[UserID],				
			[IsPublic]  
		FROM [dbo].[Matches]
		WHERE @UserID = [UserID]
		ORDER BY [MatchName]
			OFFSET (@PageNumber-1)*20 ROWS
			FETCH NEXT 20 ROWS ONLY
	END
GO

print '' print '*** creating sp_insert_user_card ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_user_card]
(
	@CardID			int,
	@UserID			int,
	@IsOwned		bit,
	@IsWishlisted	bit
)
AS
	BEGIN
		INSERT INTO [UserCards]
		(
			[CardID],
			[UserID],
			[IsOwned],
			[IsWishlisted]
		)
		VALUES
		(
			@CardID,
			@UserID,
			@IsOwned,
			@IsWishlisted
		)
	END
GO

print '' print '*** creating sp_update_user_card ***'
GO
CREATE PROCEDURE [dbo].[sp_update_user_card]
(
	@CardID				int,
	@UserID				int,
	@OldIsOwned			bit,
	@OldIsWishlisted	bit,
	
	@NewIsOwned			bit,
	@NewIsWishlisted	bit
)
AS
	BEGIN
		UPDATE [UserCards]
		SET
			[IsOwned] = @NewIsOwned,
			[IsWishlisted] = @NewIsWishlisted
		WHERE 
			[UserID] = @UserID 
		AND [CardID] = @CardID 
		AND [IsWishlisted] = @OldIsWishlisted
		AND [IsOwned] = @OldIsOwned
	END
GO

print '' print '*** creating sp_delete_user_card ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_user_card]
(
	@CardID				int,
	@UserID				int
)
AS
	BEGIN
		DELETE FROM [UserCards]
		WHERE 
			[UserID] = @UserID 
		AND [CardID] = @CardID 
	END
GO		

print '' print '*** creating sp_select_image_by_imageID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_image_by_imageID]
(
	@ImageID			int
)
AS
	BEGIN
		SELECT [ImageName]
		FROM [Images]
		WHERE [ImageID] = @ImageID
	END
GO	

print '' print '*** creating sp_insert_deck ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_deck]
(
	@DeckName		[nvarchar](50),
	@UserID			[int],
	@IsPublic		[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[Decks]
		(
			[DeckName],
			[UserID],
			[IsPublic]
		)
		VALUES
		(
			@DeckName, 
			@UserID, 
			@IsPublic
		)
	END
GO

print '' print '*** creating sp_update_deck ***'
GO
CREATE PROCEDURE [dbo].[sp_update_deck]
(
	@DeckID			[int],
	
	@OldDeckName	[nvarchar](50),
	@OldIsPublic	[bit],
	@NewDeckName	[nvarchar](50),
	@NewIsPublic	[bit]
)
AS
	BEGIN
		UPDATE [dbo].[Decks]
		SET
			[DeckName] = @NewDeckName,
			[IsPublic] = @NewIsPublic
		WHERE 
			[DeckID] = @DeckID
		AND [DeckName] = @OldDeckName
		AND [IsPublic] = @OldIsPublic
	END
GO

print '' print '*** creating sp_delete_deck ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_deck]
(
	@DeckID			[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[Decks]
		WHERE [DeckID] = @DeckID
	END
GO

print '' print '*** creating sp_insert_deck_card ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_deck_card]
(
	@DeckID			[int],
	@CardID			[int],
	@CardCount		[int]
)
AS
	BEGIN
		INSERT INTO [dbo].[DeckCards]
		(
			[DeckID],
			[CardID],
			[CardCount]
		)
		VALUES
		(
			@DeckID, 
			@CardID,
			@CardCount
		)
	END
GO

print '' print '*** creating sp_update_deck_card ***'
GO
CREATE PROCEDURE [dbo].[sp_update_deck_card]
(
	@DeckID			[int],
	@CardID			[int],
	@OldCardCount	[int],
	@NewCardCount 	[int]
)
AS
	BEGIN
		UPDATE [dbo].[DeckCards]
		SET
			[CardCount] = @NewCardCount
		WHERE
			[DeckID] = @DeckID
		AND [CardID] = @CardID
		AND [CardCount] = @OldCardCount
	END
GO

print '' print '*** creating sp_delete_deck_card ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_deck_card]
(
	@DeckID			[int],
	@CardID			[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[DeckCards]
		WHERE [DeckID] = @DeckID AND [CardID] = @CardID
	END
GO

print '' print '*** creating sp_insert_match ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_match]
(
	@MatchName		[nvarchar](100),
	@UserID			[int],
	@IsPublic		[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[Matches]
		(
			[MatchName],
			[UserID],
			[IsPublic]
		)
		VALUES
		(
			@MatchName, 
			@UserID,
			@IsPublic
		)
	END
GO

print '' print '*** creating sp_update_match ***'
GO
CREATE PROCEDURE [dbo].[sp_update_match]
(
	@MatchID		[int],
	@UserID			[int],
	@OldMatchName	[nvarchar](100),
	@NewMatchName	[nvarchar](100),
	@OldIsPublic	[bit],
	@NewIsPublic	[bit]
)
AS
	BEGIN
		UPDATE [dbo].[Matches]
		SET
			[MatchName] = @NewMatchName,
			[IsPublic] = @NewIsPublic
		WHERE
			[MatchID] = @MatchID
		AND [UserID] = @UserID
		AND [MatchName] = @OldMatchName
		AND [IsPublic] = @OldIsPublic
	END
GO

print '' print '*** creating sp_delete_match ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_match]
(
	@MatchID		[int]
)
AS
	BEGIN
		DELETE FROM [dbo].[Matches]
		WHERE [MatchID] = @MatchID
	END
GO

print '' print '*** creating sp_insert_match_deck ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_match_deck]
(
	@MatchID 		[int], 
	@DeckID			[int],       			
    @Winner			[bit]
)
AS
	BEGIN
		INSERT INTO [dbo].[MatchDecks]
		(
			[MatchID],
			[DeckID],
			[Winner]
		)
		VALUES
		(
			@MatchID, 
			@DeckID,
			@Winner
		)
	END
GO

print '' print '*** creating sp_delete_match_deck ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_match_deck]
(
	@MatchID 		[int], 
	@DeckID			[int],
	@Winner			[bit]
)
AS
	BEGIN
		DELETE FROM [dbo].[MatchDecks]
		WHERE [MatchID] = @MatchID AND [DeckID] = @DeckID AND [Winner] = @Winner
	END
GO




















