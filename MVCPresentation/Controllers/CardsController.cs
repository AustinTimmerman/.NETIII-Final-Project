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

        

        // GET: Cards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cards/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("ViewAllCards");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cards/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cards/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("ViewAllCards");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cards/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cards/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("ViewAllCards");
            }
            catch
            {
                return View();
            }
        }
    }
}
