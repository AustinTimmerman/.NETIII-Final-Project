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
    public class CardsController : Controller
    {
        ICardManager _cardManager = new CardManager();
        IDeckManager _deckManager = new DeckManager();
        public int PageSize = 12;
        CardViewModel _model = null;
        

        [Authorize]
        public ActionResult ViewAllCards(int page = 1)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            
            List<Cards> cards = new List<Cards>();
            cards = _cardManager.RetrieveAllCards(userID);

            _model = new CardViewModel
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

            return View(_model);
        }

        [Authorize]
        // GET: Cards/Details/5
        public ActionResult ViewCardDetails(int cardID = 0)
        {
            if(cardID == 0)
            {
                return RedirectToAction("ViewAllCards");
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            Cards card = _cardManager.RetrieveCardByCardID(cardID, userID);
            card.ImageName = _cardManager.RetrieveImageByImageID(card.ImageID);
            card.SecondaryImageName = _cardManager.RetrieveImageByImageID(card.SecondaryImageID);

            return View(card);
        }
        

        [HttpPost]
        public ActionResult ViewCardDetails(Cards cards)
        {
            if(Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewCardDetails", new { cardID = cards.CardID });
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            Cards _card = _cardManager.RetrieveCardByCardID(cards.CardID, userID);

            UserCard oldCard = new UserCard()
            {
                CardID = _card.CardID,
                CardName = _card.CardName,
                UserID = userID,
                ImageID = _card.ImageID,
                CardDescription = _card.CardDescription,
                CardColorID = _card.CardColorID,
                CardConvertedManaCost = _card.CardConvertedManaCost,
                CardRarityID = _card.CardRarityID,
                CardTypeID = _card.CardTypeID,
                HasSecondaryCard = _card.HasSecondaryCard,
                CardSecondaryName = _card.CardSecondaryName,
                SecondaryImageID = _card.SecondaryImageID,
                CardSecondaryDescription = _card.CardSecondaryDescription,
                CardSecondaryColorID = _card.CardSecondaryColorID,
                CardSecondaryConvertedManaCost = _card.CardSecondaryConvertedManaCost,
                CardSecondaryRarityID = _card.CardSecondaryRarityID,
                CardSecondaryTypeID = _card.CardSecondaryTypeID,
                OwnedCard = _card.IsOwned,
                Wishlisted = _card.IsWishlisted
            };

            UserCard newCard = new UserCard()
            {
                CardID = cards.CardID,
                CardName = _card.CardName,
                UserID = userID,
                ImageID = _card.ImageID,
                CardDescription = _card.CardDescription,
                CardColorID = _card.CardColorID,
                CardConvertedManaCost = _card.CardConvertedManaCost,
                CardRarityID = _card.CardRarityID,
                CardTypeID = _card.CardTypeID,
                HasSecondaryCard = _card.HasSecondaryCard,
                CardSecondaryName = _card.CardSecondaryName,
                SecondaryImageID = _card.SecondaryImageID,
                CardSecondaryDescription = _card.CardSecondaryDescription,
                CardSecondaryColorID = _card.CardSecondaryColorID,
                CardSecondaryConvertedManaCost = _card.CardSecondaryConvertedManaCost,
                CardSecondaryRarityID = _card.CardSecondaryRarityID,
                CardSecondaryTypeID = _card.CardSecondaryTypeID,
                OwnedCard = cards.IsOwned,
                Wishlisted = cards.IsWishlisted
            };

            if(!cards.IsOwned && !cards.IsWishlisted)
            {
                _cardManager.RemoveUserCard(newCard);
            }
            else
            {
                bool result = _cardManager.EditUserCard(oldCard, newCard);
                if (!result)
                {
                    _cardManager.CreateUserCard(newCard);
                }
            }

            return RedirectToAction("ViewCardDetails", new { cardID = cards.CardID });
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCardToDeck(int cardID = 0)
        {
            if(cardID == 0)
            {
                return RedirectToAction("ViewAllCards");
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            Cards card = _cardManager.RetrieveCardByCardID(cardID, userID);

            _model = new CardViewModel
            {
                Card = card,
                Decks = _deckManager.RetrieveUserDecksByUserID(userID)
            };
            return View(_model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCardToDeck(FormCollection collection)
        {
            int cardID = int.Parse(collection["Card.CardID"]);
            int deckID = int.Parse(collection["SelectedDeck"]);
            int amount = int.Parse(collection["Amount"]);

            if (Request.Form["cancel"] != null)
            {
                return RedirectToAction("ViewCardDetails", new { cardID = cardID });
            }

            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var appUser = userManager.FindById(User.Identity.GetUserId());
            int userID = (int)appUser.UserID;

            DeckCard card = new DeckCard()
            {
                DeckID = deckID,
                CardID = cardID,
                CardCount = amount
            };
            try
            {
                _deckManager.CreateDeckCard(card);
                TempData["Message"] = "Successfully added to deck.";
            }
            catch(Exception)
            {
                TempData["Message"] = "Card is already in this deck.";
                return RedirectToAction("AddCardToDeck", new { cardID = cardID });
            }
            return RedirectToAction("AddCardToDeck", new { cardID = cardID });
        }
    }
}
