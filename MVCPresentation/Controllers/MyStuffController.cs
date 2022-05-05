using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MVCPresentation.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace MVCPresentation.Controllers
{
    public class MyStuffController : Controller
    {
        ICardManager _cardManager = new CardManager();
        IDeckManager _deckManager = new DeckManager();
        IMatchManager _matchManager = new MatchManager();
        public int PageSize = 12;
        CardViewModel _cardModel = null;
        DeckViewModel _deckModel = null;
        MatchViewModel _matchModel = null;


        [Authorize]
        public ActionResult ViewMyCards(int page = 1)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            List<Cards> cards = new List<Cards>();
            List<UserCard> userCards = new List<UserCard>();
            userCards = _cardManager.RetrieveUserCardsByUserID(userID);
            foreach(UserCard userCard in userCards)
            {
                cards.Add(new Cards()
                {
                    CardID = userCard.CardID,
                    CardName = userCard.CardName,
                    ImageID = userCard.ImageID,
                    CardDescription = userCard.CardDescription,
                    CardColorID = userCard.CardColorID,
                    CardConvertedManaCost = userCard.CardConvertedManaCost,
                    CardRarityID = userCard.CardRarityID,
                    CardTypeID = userCard.CardTypeID,
                    HasSecondaryCard = userCard.HasSecondaryCard,
                    CardSecondaryName = userCard.CardSecondaryName,
                    SecondaryImageID = userCard.SecondaryImageID,
                    CardSecondaryDescription = userCard.CardSecondaryDescription,
                    CardSecondaryColorID = userCard.CardSecondaryColorID,
                    CardSecondaryConvertedManaCost = userCard.CardSecondaryConvertedManaCost,
                    CardSecondaryRarityID = userCard.CardSecondaryRarityID,
                    CardSecondaryTypeID = userCard.CardSecondaryTypeID,
                    IsOwned = userCard.OwnedCard,
                    IsWishlisted = userCard.Wishlisted
                });
            }


            _cardModel = new CardViewModel
            {
                Cards = cards
                            .OrderBy(p => p.CardName)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = cards.Count()
                }
            };

            return View(_cardModel);
        }

        [Authorize]
        public ActionResult ViewMyDecks(int page = 1)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            List<Deck> decks = new List<Deck>();
            List<DeckVM> userDecks = new List<DeckVM>();
            decks = _deckManager.RetrieveUserDecksByUserID(userID);
            foreach(Deck deck in decks)
            {
                userDecks.Add(new DeckVM()
                {
                    DeckID = deck.DeckID,
                    DeckName = deck.DeckName,
                    UserID = deck.UserID,
                    IsPublic = deck.IsPublic
                });
            }


            _deckModel = new DeckViewModel
            {
                Decks = userDecks
                            .OrderBy(p => p.DeckName)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = userDecks.Count()
                }
            };

            return View(_deckModel);
        }

        [Authorize]
        public ActionResult ViewMyMatches(int page = 1)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            List<Match> matches = new List<Match>();
            List<MatchVM> userMatches = new List<MatchVM>();
            matches = _matchManager.RetrieveUserMatchesByUserID(userID);
            foreach (Match match in matches)
            {
                userMatches.Add(new MatchVM()
                {
                    MatchID = match.MatchID,
                    MatchName = match.MatchName,
                    IsPublic = match.IsPublic
                });
            }


            _matchModel = new MatchViewModel
            {
                Matches = userMatches
                            .OrderBy(p => p.MatchName)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = userMatches.Count()
                }
            };

            return View(_matchModel);
        }
    }
}