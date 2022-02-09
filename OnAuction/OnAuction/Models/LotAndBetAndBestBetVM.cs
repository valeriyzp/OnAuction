using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class LotAndBetAndBestBetVM
    {
        public Lot Lot { get; set; }
        public Bet Bet { get; set; }
        public Bet BestBet { get; set; }
    }
}
