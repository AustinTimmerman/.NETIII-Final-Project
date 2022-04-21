using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataObjects;
using LogicLayer;
using MVCPresentation.Models;

namespace MVCPresentation.Controllers
{
    public class CardsController : Controller
    {
        ICardManager _cardManager = new CardManager();
        public int PageSize = 12;
        CardViewModel _model = null;
        
        public ActionResult ViewAllCards(int page = 1)
        {
            List<Cards> cards = new List<Cards>();
            cards = _cardManager.RetrieveAllCards(100000);

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
        public ActionResult ViewCardDetails(int cardID)
        {
            if(cardID == 0)
            {
                return RedirectToAction("ViewAllCards");
            }
            
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
