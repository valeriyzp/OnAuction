using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnAuction.Models
{
    public class Bet
    {
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int LotId { get; set; }
        public Lot Lot { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
