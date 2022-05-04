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
    public class DecksController : Controller
    {
        IDeckManager _deckManager = new DeckManager();
        public int PageSize = 12;
        DeckViewModel _model = null;

        
        [Authorize]
        public ActionResult ViewAllDecks(int page = 1)
        {
            //var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

            //var appUser = userManager.FindById(User.Identity.GetUserId());
            //int userID = (int)appUser.UserID;


            List<DeckVM> decks = new List<DeckVM>();
            decks = _deckManager.RetrieveAllDecks();

            _model = new DeckViewModel
            {
                Decks = decks
                            .OrderBy(p => p.DeckName)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = decks.Count()
                }
            };




            return View(_model);
        }

        public ActionResult ViewDeckDetails(int deckID = 0, string deckName = "")
        {
            if (deckID == 0)
            {
                return RedirectToAction("ViewAllDecks");
            }

            List<DeckCard> deckCards = new List<DeckCard>();
            deckCards = _deckManager.RetrieveDeckCards(deckID);

            _model = new DeckViewModel
            {
                Cards = deckCards
                            .OrderBy(p => p.CardName)
            };

            ViewBag.Title = deckName;
            return View(_model);
        }
    }
}