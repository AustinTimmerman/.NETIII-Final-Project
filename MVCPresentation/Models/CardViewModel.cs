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
    }
}