using OnAuction.Filters;
using OnAuction.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnAuction.LifeCycle;

namespace OnAuction.Controllers
{
    public class LotController : Controller
    {
        DataContext db = new DataContext();

        [User(Roles = "seller")]
        public ActionResult Create()
        {
            SelectList categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LotCreateModel model)
        {
            if(ModelState.IsValid)
            {
                DateTime Date = DateTime.Now;
                string imagePath = null;
                
                if(model.Image != null)
                {
                    imagePath = "~/Images/" + Date.ToString("yyyyMMdd_HHmmssfff") + Path.GetExtension(model.Image.FileName);
                    model.Image.SaveAs(Server.MapPath(imagePath));
                }

                if(model.InstantPrice < model.StartPrice)
                {
                    model.InstantPrice = model.StartPrice;
                }
                
                using (DataContext db = new DataContext())
                {
                    int userId = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name).Id;

                    db.Lots.Add(new Lot {
                        Title = model.Title,
                        Description = model.Description,
                        ImagePath = imagePath,
                        StartPrice = model.StartPrice,
                        InstantPrice = model.InstantPrice,
                        BetStep = model.BetStep,
                        StartTime = Date,
                        FinishTime = Date.AddDays(1),
                        IsFinish = false,
                        UserId = userId,
                        CategoryId = model.CategoryId });

                    db.SaveChanges();
                }

                return RedirectToAction("List", "Lot");
            }
            SelectList categories = new SelectList(db.Categories, "Id", "Name");
            ViewBag.Categories = categories;
            return View(model);
        }

        public ActionResult List(int? category)
        {
            LifeCycleManager.CheckLotsForFinish();

            var Lots = db.Lots.AsQueryable();

            if (category != null)
            {
                Lots = Lots.Where(x => x.CategoryId == category);
            }

            var BestBets = db.Bets
                .GroupBy(bet => bet.LotId)
                .Select(grp => grp.OrderByDescending(bet => bet.Price).FirstOrDefault());

            var LotAndBestBet = Lots 
                .Where(lot => !lot.IsFinish)
                .GroupJoin(BestBets, lot => lot.Id, bet => bet.LotId, (Lot, Bets) => new { Lot, Bets })
                .SelectMany(grp => grp.Bets.DefaultIfEmpty(), (grp, BestBet) => new LotAndBetVM { Lot = grp.Lot, Bet = BestBet })
                .OrderByDescending(l => l.Lot.FinishTime);

            ViewBag.LotAndBestBet = LotAndBestBet;
            ViewBag.Categories = db.Categories;

            return View();
        }

        public ActionResult Info(int LotId)
        {
            LifeCycleManager.CheckLotsForFinish();

            Lot Lot = db.Lots
                .Include(lot => lot.Category)
                .FirstOrDefault(lot => lot.Id == LotId);

            if (Lot == null)
            {
                return HttpNotFound();
            }

            Bet BestBet = db.Bets
                .Where(bet => bet.LotId == LotId)
                .OrderByDescending(bet => bet.Price)
                .FirstOrDefault();

            ViewBag.BetsCount = db.Bets.Where(bet => bet.LotId == LotId).Count();

            return View(new LotAndBetVM { Lot = Lot, Bet = BestBet});
        }

        [HttpPost]
        [User(Roles = "buyer")]
        [ValidateAntiForgeryToken]
        public void MakeBet(int LotId, int Price)
        {
            LifeCycleManager.CheckLotsForFinish();

            DateTime CurrentDate = DateTime.Now;

            Lot Lot = db.Lots.FirstOrDefault(lot => lot.Id == LotId && !lot.IsFinish && lot.FinishTime > CurrentDate);

            if(Lot == null)
            {
                Response.Redirect("Info?LotId=" + LotId.ToString(), false);
                return;
            }

            if(!((Price - Lot.StartPrice) >= 0 && (Price - Lot.StartPrice) % Lot.BetStep == 0))
            {
                string errorMessage = "Вартість ставки не відповідає вимогам";
                Response.Write("<script language='javascript'>window.alert('" + errorMessage + "');window.location.href='Info?LotId=" + LotId.ToString() + "';</script>");
                return;
            }

            Bet BestBet = db.Bets
                .Where(bet => bet.LotId == LotId)
                .OrderByDescending(bet => bet.Price)
                .FirstOrDefault();

            if(BestBet != null)
            {
                if(Price <= BestBet.Price)
                {
                    string errorMessage = "Вартість поточної ставки має бути більше ніж попередня";
                    Response.Write("<script language='javascript'>window.alert('" + errorMessage + "');window.location.href='Info?LotId=" + LotId.ToString() + "';</script>");
                    return;
                }
            }

            if(Price >= Lot.InstantPrice)
            {
                InstantBet(LotId, CurrentDate);
                return;
            }

            using (DataContext db = new DataContext())
            {
                int userId = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name).Id;

                db.Bets.Add(new Bet {
                    Price = Price,
                    Time = CurrentDate,
                    LotId = LotId,
                    UserId = userId });

                db.SaveChanges();
            }

            Response.Redirect("Info?LotId=" + LotId.ToString(), false);
            return;
        }

        [HttpPost]
        [User(Roles = "buyer")]
        [ValidateAntiForgeryToken]
        public void MakeInstantBet(int LotId)
        {
            LifeCycleManager.CheckLotsForFinish();

            InstantBet(LotId, DateTime.Now);
        }
        
        private void InstantBet(int LotId, DateTime CurrentDate)
        {
            Lot Lot = db.Lots.FirstOrDefault(lot => lot.Id == LotId && !lot.IsFinish && lot.FinishTime > CurrentDate);

            if (Lot == null)
            {
                Response.Redirect("Info?LotId=" + LotId.ToString(), false);
                return;
            }

            Lot.IsFinish = true;
            Lot.FinishTime = CurrentDate;

            using (DataContext db = new DataContext())
            {
                int userId = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name).Id;

                db.Bets.Add(new Bet
                {
                    Price = Lot.InstantPrice,
                    Time = CurrentDate,
                    LotId = LotId,
                    UserId = userId
                });

                db.Wins.Add(new Win{
                    Price = Lot.InstantPrice,
                    Time = CurrentDate,
                    LotId = LotId,
                    UserId = userId});

                db.Lots.Attach(Lot);
                db.Entry(Lot).Property(lot => lot.IsFinish).IsModified = true;
                db.Entry(Lot).Property(lot => lot.FinishTime).IsModified = true;

                db.SaveChanges();
            }

            Response.Redirect("Info?LotId=" + LotId.ToString(), false);
            return;
        }

        public ActionResult Bets(int LotId)
        {
            LifeCycleManager.CheckLotsForFinish();

            Lot Lot = db.Lots.FirstOrDefault(lot => lot.Id == LotId);

            List<Bet> Bets = db.Bets.Where(bet => bet.LotId == LotId).Include(bet => bet.User).OrderBy(bet => bet.Price).ToList();

            if(Lot == null)
            {
                return HttpNotFound();
            }

            ViewBag.Lot = Lot;

            return View(Bets);
        }

        [User(Roles="buyer")]
        public ActionResult MyBetsActive()
        {
            LifeCycleManager.CheckLotsForFinish();

            int userId = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name).Id;

            var MyBets = db.Bets
                                .Include(bet => bet.Lot)
                                .Where(bet => bet.UserId == userId && !bet.Lot.IsFinish)
                                .GroupBy(bet => bet.LotId)
                                .Select(grp => grp.OrderByDescending(bet => bet.Price).FirstOrDefault());

            var BestBets = db.Bets
                                .Where(bet => !bet.Lot.IsFinish)
                                .GroupBy(bet => bet.LotId)
                                .Select(grp => grp.OrderByDescending(bet => bet.Price).FirstOrDefault());

            List<LotAndBetAndBestBetVM> MyBetAndBestBet = MyBets
                                .Join(BestBets, bet => bet.LotId, bet => bet.LotId, (bet, bestbet) => new { Bet = bet, BestBet = bestbet })
                                .OrderByDescending(vm => vm.Bet.Lot.FinishTime)
                                .Join(db.Lots, bets => bets.Bet.LotId, lot => lot.Id, (bets, lot) => new LotAndBetAndBestBetVM { Lot = lot, Bet = bets.Bet, BestBet = bets.BestBet})
                                .ToList();

            return View(MyBetAndBestBet);
        }

        [User(Roles = "buyer")]
        public ActionResult MyBetsHistory()
        {
            LifeCycleManager.CheckLotsForFinish();

            int userId = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name).Id;

            var MyBets = db.Bets
                                .Include(bet => bet.Lot)
                                .Where(bet => bet.UserId == userId && bet.Lot.IsFinish)
                                .GroupBy(bet => bet.LotId)
                                .Select(grp => grp.OrderByDescending(bet => bet.Price).FirstOrDefault());

            var WinBets = db.Wins.Where(win => win.Lot.IsFinish);

            List<LotAndBetAndWinVM> MyBetAndWin = MyBets
                                .Join(WinBets, bet => bet.LotId, win => win.LotId, (bet, win) => new { Bet = bet, Win = win })
                                .OrderByDescending(vm => vm.Bet.Lot.FinishTime)
                                .Join(db.Lots, bets => bets.Bet.LotId, lot => lot.Id, (bets, lot) => new LotAndBetAndWinVM { Lot = lot, Bet = bets.Bet, Win = bets.Win })
                                .ToList();

            return View(MyBetAndWin);
        }

        [User(Roles = "seller")]
        public ActionResult MyLots()
        {
            LifeCycleManager.CheckLotsForFinish();

            LifeCycleManager.CheckLotsForFinish();

            int userId = db.Users.FirstOrDefault(x => x.Username == User.Identity.Name).Id;

            var MyLots = db.Lots.Where(lot => lot.UserId == userId);

            var BestBets = db.Bets
                                .Include(bet => bet.Lot)
                                .Where(bet => bet.Lot.UserId == userId)
                                .GroupBy(bet => bet.LotId)
                                .Select(grp => grp.OrderByDescending(bet => bet.Price).FirstOrDefault());

            var Wins = db.Wins
                        .Include(win => win.User)
                        .Include(win => win.Lot)
                        .Where(win => win.Lot.UserId == userId);

            List<LotAndBetAndWinAndUserVM> MyLotsAndWins = MyLots
                .GroupJoin(Wins, lot => lot.Id, win => win.LotId, (lot, win) => new { lot, win })
                .SelectMany(grp => grp.win.DefaultIfEmpty(), (grp, win) => new { grp.lot, win, win.User })
                .GroupJoin(BestBets, grp => grp.lot.Id, bet => bet.LotId, (grp, bet) => new { grp.lot, bet, grp.win })
                .SelectMany(grp => grp.bet.DefaultIfEmpty(), (grp, bet) => new LotAndBetAndWinAndUserVM { Lot = grp.lot, Bet = bet, Win = grp.win, Winner = grp.win.User})
                .OrderByDescending(grp => grp.Lot.FinishTime)
                .ToList();

            return View(MyLotsAndWins);
        }
    }
}
