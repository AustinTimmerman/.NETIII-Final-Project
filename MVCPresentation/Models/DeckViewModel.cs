using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class DeckViewModel
    {
        public IEnumerable<DeckVM> Decks { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<DeckCard> Cards { get; set; }
        public bool CanEdit { get; set; }
        public Deck Deck { get; set; }
        public Match SelectedMatch { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public bool Winner { get; set; }
    }
}