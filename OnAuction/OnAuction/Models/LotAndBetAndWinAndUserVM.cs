using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class LotAndBetAndWinAndUserVM
    {
        public Lot Lot { get; set; }
        public Bet Bet { get; set; }
        public Win Win { get; set; }
        public User Winner { get; set; }
    }
}
