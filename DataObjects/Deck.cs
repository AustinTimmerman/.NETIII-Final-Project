using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Deck
    {
        public int DeckID { get; set; }
        public String DeckName { get; set; }
        public int UserID { get; set; }
        public bool IsPublic { get; set; }
    }

    public class MatchDeck
    {
        public int MatchID { get; set; }
        public int DeckID { get; set; }
        public string DeckName { get; set; }
        public bool Winner { get; set; }
    }
}
