using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;

namespace MVCPresentation.Controllers
{
    public class CardsController : Controller
    {
        ICardManager _cardManager = new CardManager();
        // GET: Cards
        public ActionResult ViewAllCards()
        {
            //List<UserCard> displayCards = new List<UserCard>();
            List<Cards> cards = new List<Cards>();


            cards = _cardManager.RetrieveAllCards(100000);
            //List<Cards> cards = _cardManager.(pageNumber);

            //foreach (Cards card in cards)
            //{
            //    for (int i = 0; i <= userCards.Count; i++)
            //    {
            //        if (i == userCards.Count)
            //        {
            //            displayCards.Add(new UserCard()
            //            {
            //                UserID = _user.UserID,
            //                CardID = card.CardID,
            //                CardName = card.CardName,
            //                ImageID = card.ImageID,
            //                CardDescription = card.CardDescription,
            //                CardColorID = card.CardColorID,
            //                CardConvertedManaCost = card.CardConvertedManaCost,
            //                CardRarityID = card.CardRarityID,
            //                CardTypeID = card.CardTypeID,
            //                HasSecondaryCard = card.HasSecondaryCard,
            //                CardSecondaryName = card.CardSecondaryName,
            //                SecondaryImageID = card.SecondaryImageID,
            //                CardSecondaryDescription = card.CardSecondaryDescription,
            //                CardSecondaryColorID = card.CardSecondaryColorID,
            //                CardSecondaryConvertedManaCost = card.CardSecondaryConvertedManaCost,
            //                CardSecondaryRarityID = card.CardSecondaryRarityID,
            //                CardSecondaryTypeID = card.CardSecondaryTypeID,
            //                OwnedCard = false,
            //                Wishlisted = false
            //            });
            //            break;
            //        }
            //        if (userCards[i].CardID == card.CardID)
            //        {
            //            displayCards.Add(userCards[i]);
            //            break;
            //        }
            //    }
            //}

            
            return View(cards);
        }

        // GET: Cards/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
