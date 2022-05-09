using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Deck
    {
        public int DeckID { get; set; }
        [Display(Name = "Deck Name")]
        [Required(ErrorMessage = "Please enter a deck name")]
        public String DeckName { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Is Public")]
        public bool IsPublic { get; set; }
    }

    public class DeckVM : Deck
    {
        public String Username { get; set; }
    }

    public class MatchDeck
    {
        public int MatchID { get; set; }
        public int DeckID { get; set; }
        public string DeckName { get; set; }
        public bool Winner { get; set; }
    }
}
