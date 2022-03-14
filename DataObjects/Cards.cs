using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Cards
    {
        public int CardID {get; set;}
        public string CardName { get; set; }
        public int ImageID { get; set; }
        public string CardDescription { get; set; }
        public string CardColorID { get; set; }
        public int CardConvertedManaCost { get; set; }
        public string CardTypeID { get; set; }
        public string CardRarityID { get; set; }
        public bool HasSecondaryCard { get; set; }
        public string CardSecondaryName { get; set; }
        public int SecondaryImageID { get; set; }
        public string CardSecondaryDescription { get; set; }
        public string CardSecondaryColorID { get; set; }
        public int CardSecondaryConvertedManaCost { get; set; }
        public string CardSecondaryTypeID { get; set; }
        public string CardSecondaryRarityID { get; set; }
    }

    public class DeckCard : Cards
    {
        public int DeckID { get; set; }
        public int CardCount { get; set; }
    }

    public class UserCard : Cards
    {
        public int UserID { get; set; }
        public bool OwnedCard { get; set; }
        public bool Wishlisted { get; set; }
        public int CardCount { get; set; }
    }
}
