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

        public ActionResult ViewMyDeckDetails(int deckID = 0, string deckName = "")
        {
            if (deckID == 0)
            {
                return RedirectToAction("ViewMyDecks");
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            List<DeckCard> deckCards = new List<DeckCard>();
            deckCards = _deckManager.RetrieveDeckCards(deckID);

            _deckModel = new DeckViewModel
            {
                Cards = deckCards
                            .OrderBy(p => p.CardName)
            };
            

            ViewBag.Title = deckName;
            return View(_deckModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditCard(int cardID = 0, int deckID = 0, int amount = 0)
        {
            if(cardID == 0)
            {
                return RedirectToAction("ViewMyDecks");
            }
            DeckCard card = new DeckCard();
            card.CardID = cardID;
            card.DeckID = deckID;
            card.CardCount = amount;


            ViewBag.Title = _deckManager.RetrieveDeckByDeckID(deckID).DeckName;
            return View(card);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCard(DeckCard card)
        {
            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewMyDeckDetails", new { deckID = card.DeckID, deckName = Request.Form["title"] });
            }

            int cardCount = int.Parse(Request.Form["count"]);
            DeckCard oldCard = new DeckCard();

            oldCard.CardCount = cardCount;
            oldCard.CardID = card.CardID;
            oldCard.DeckID = card.DeckID;

            if (_deckManager.EditDeckCard(oldCard, card))
            {
                TempData["Message"] = "Card count successfully edited.";
            }
            else
            {
                TempData["Message"] = "Could not edit card count. Please try again.";
            }
                

            return RedirectToAction("ViewMyDeckDetails", new { deckID = card.DeckID, deckName = Request.Form["title"] });
        }

        [Authorize]
        public ActionResult DeleteCard(int cardID = 0, int deckID = 0, string deckName = "")
        {
            DeckCard card = new DeckCard();
            card.CardID = cardID;
            card.DeckID = deckID;
            if (_deckManager.RemoveDeckCard(card))
            {
                TempData["Message"] = "Card successfully removed from deck.";
            }
            else
            {
                TempData["Message"] = "Could not remove card from deck. Please try again.";
            }
            return RedirectToAction("ViewMyDeckDetails", new { deckID = deckID, deckName = deckName });
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateDeck()
        {
            Deck deck = new Deck();
            return View(deck);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateDeck(Deck deck)
        {
            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewMyDecks");
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            if(_deckManager.CreateDeck(deck.DeckName, userID, deck.IsPublic))
            {
                TempData["Message"] = "Deck successfully created.";
            }
            else
            {
                TempData["Message"] = "Deck could not be created. Please try again.";
            }

            return RedirectToAction("ViewMyDecks");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditMyDeck(int deckID = 0)
        {
            if(deckID == 0)
            {
                return RedirectToAction("ViewMyDecks");
            }
            Deck deck = new Deck();
            deck = _deckManager.RetrieveDeckByDeckID(deckID);            
            return View(deck);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditMyDeck(Deck deck)
        {
            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewMyDecks");
            }

            Deck oldDeck = new Deck();
            oldDeck = _deckManager.RetrieveDeckByDeckID(deck.DeckID);

            if(_deckManager.EditDeck(oldDeck, deck))
            {
                TempData["Message"] = "Deck successfully edited.";
            }
            else
            {
                TempData["Message"] = "Deck could not be edited. Please try again.";
            }

            return RedirectToAction("ViewMyDecks");
        }

        [Authorize]
        public ActionResult DeleteDeck(int deckID)
        {
            Deck deck = new Deck();
            deck = _deckManager.RetrieveDeckByDeckID(deckID);
            if (_deckManager.RemoveDeck(deck)) {
                TempData["Message"] = "Deck successfully deleted.";
            }
            else
            {
                TempData["Message"] = "Could not delete deck. Please try again.";
            }
            return RedirectToAction("ViewMyDecks");
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateMatch()
        {
            Match match = new Match();
            return View(match);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateMatch(Match match)
        {
            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewMyMatches");
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            if (_matchManager.CreateMatch(match.MatchName, userID, match.IsPublic))
            {
                TempData["Message"] = "Match successfully created.";
            }
            else
            {
                TempData["Message"] = "Match could not be created. Please try again.";
            }

            return RedirectToAction("ViewMyMatches");
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditMyMatch(int matchID = 0)
        {
            if (matchID == 0)
            {
                return RedirectToAction("ViewMyMatches");
            }
            Match match = new Match();
            match = _matchManager.RetrieveMatchByMatchID(matchID);
            return View(match);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditMyMatch(Match match)
        {
            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewMyMatches");
            }

            Match oldMatch = new Match();
            oldMatch = _matchManager.RetrieveMatchByMatchID(match.MatchID);

            if (_matchManager.EditMatch(oldMatch, match))
            {
                TempData["Message"] = "Match successfully edited.";
            }
            else
            {
                TempData["Message"] = "Match could not be edited. Please try again.";
            }

            return RedirectToAction("ViewMyMatches");
        }

        [Authorize]
        public ActionResult DeleteMatch(int matchID)
        {
            Match match = new Match();
            match = _matchManager.RetrieveMatchByMatchID(matchID);
            if (_matchManager.RemoveMatch(match))
            {
                TempData["Message"] = "Match successfully deleted.";
            }
            else
            {
                TempData["Message"] = "Could not delete match. Please try again.";
            }
            return RedirectToAction("ViewMyMatches");
        }
    }
}