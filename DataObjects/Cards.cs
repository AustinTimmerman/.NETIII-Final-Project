using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Cards
    {
        public int CardID {get; set;}
        public string CardName { get; set; }
        public int ImageID { get; set; }
        public string ImageName { get; set; }
        [Display(Name = "Description")]
        public string CardDescription { get; set; }
        [Display(Name = "Color")]
        public string CardColorID { get; set; }
        [Display(Name = "Converted Mana Cost")]
        public int CardConvertedManaCost { get; set; }
        [Display(Name = "Card Type")]
        public string CardTypeID { get; set; }
        [Display(Name = "Card Rarity")]
        public string CardRarityID { get; set; }
        public bool HasSecondaryCard { get; set; }
        public string CardSecondaryName { get; set; }
        public int SecondaryImageID { get; set; }
        public String SecondaryImageName { get; set; }
        [Display(Name = "Description")]
        public string CardSecondaryDescription { get; set; }
        [Display(Name = "Color")]
        public string CardSecondaryColorID { get; set; }
        [Display(Name = "Converted Mana Cost")]
        public int CardSecondaryConvertedManaCost { get; set; }
        [Display(Name = "Card Type")]
        public string CardSecondaryTypeID { get; set; }
        [Display(Name = "Card Rarity")]
        public string CardSecondaryRarityID { get; set; }
        [Display(Name = "Owned")]
        public bool IsOwned { get; set; }
        [Display(Name = "Wishlisted")]
        public bool IsWishlisted { get; set; }
    }

    public class DeckCard : Cards
    {
        public int DeckID { get; set; }
        [Required(ErrorMessage = "Please enter the card count")]
        [Range(1, 100, ErrorMessage = "Please enter a value between 1 and 100.")]
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
