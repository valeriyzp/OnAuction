using OnAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnAuction.LifeCycle
{
    public static class LifeCycleManager
    {
        public static void CheckLotsForFinish()
        {
            using (DataContext db = new DataContext())
            {
                List<Lot> Lots = db.Lots.Where(lot => !lot.IsFinish && lot.FinishTime <= DateTime.Now).ToList();

                foreach (Lot Lot in Lots)
                {
                    Bet BestBet = db.Bets
                        .Where(bet => bet.LotId == Lot.Id)
                        .OrderByDescending(bet => bet.Price)
                        .FirstOrDefault();

                    if (BestBet != null)
                    {
                        db.Wins.Add(new Win
                        {
                            Price = BestBet.Price,
                            Time = Lot.FinishTime,
                            LotId = Lot.Id,
                            UserId = BestBet.UserId
                        });
                    }

                    Lot.IsFinish = true;

                    db.Lots.Attach(Lot);
                    db.Entry(Lot).Property(lot => lot.IsFinish).IsModified = true;

                    db.SaveChanges();
                }
            }
        }
    }
}
