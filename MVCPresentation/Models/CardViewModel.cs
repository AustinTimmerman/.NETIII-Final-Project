using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class CardViewModel
    {
        public IEnumerable<Cards> Cards { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Deck> Decks { get; set; }
        public Cards Card { get; set; }
        public Deck SelectedDeck { get; set; }
        public int Amount { get; set; }
    }
}