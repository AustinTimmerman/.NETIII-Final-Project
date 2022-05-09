using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    public class Match
    {
        public int MatchID { get; set; }
        [Display(Name = "Match Name")]
        [Required(ErrorMessage = "Please enter a match name")]
        public String MatchName { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Is Public")]
        public bool IsPublic { get; set; }
    }

    public class MatchVM : Match
    {
        public String Username { get; set; }
    }
}
