using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataObjects;
namespace MVCPresentation.Models
{
    public class MatchViewModel
    {
        public IEnumerable<MatchVM> Matches { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}