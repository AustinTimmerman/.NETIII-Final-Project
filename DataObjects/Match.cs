using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Match
    {
        public int MatchID { get; set; }
        public String MatchName { get; set; }
        public int UserID { get; set; }
        public bool IsPublic { get; set; }
    }
}
