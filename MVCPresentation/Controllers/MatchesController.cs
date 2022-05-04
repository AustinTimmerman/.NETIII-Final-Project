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
    public class MatchesController : Controller
    {
        IMatchManager _matchManager = new MatchManager();
        public int PageSize = 12;
        MatchViewModel _model = null;

        [Authorize]
        public ActionResult ViewAllMatches(int page = 1)
        {
            


            List<MatchVM> matches = new List<MatchVM>();
            matches = _matchManager.RetrieveAllMatches();

            _model = new MatchViewModel
            {
                Matches = matches
                            .OrderBy(p => p.MatchName)
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = matches.Count()
                }
            };




            return View(_model);
        }

        public ActionResult ViewMatchDetails(int matchID = 0, string matchName = "")
        {
            if (matchID == 0)
            {
                return RedirectToAction("ViewAllMatches");
            }

            List<MatchDeck> matchDecks = _matchManager.RetrieveMatchDecksByMatchID(matchID);

            _model = new MatchViewModel
            {
                Decks = matchDecks
                            .OrderBy(p => p.DeckName)
            };

            ViewBag.Title = matchName;

            return View(_model);
        }
    }
}