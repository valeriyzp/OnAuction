using OnAuction.Filters;
using OnAuction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnAuction.LifeCycle;

namespace OnAuction.Controllers
{
    public class AdminController : Controller
    {
        [User(Roles = "admin")]
        public ActionResult Bets()
        {
            LifeCycleManager.CheckLotsForFinish();

            List<Bet> Bets;
            using(DataContext db = new DataContext())
            {
                Bets = db.Bets.Include(x => x.Lot).Include(x => x.User).OrderByDescending(x => x.Time).ToList();
            }

            return View(Bets);
        }

        [User(Roles = "admin")]
        public ActionResult Wins()
        {
            LifeCycleManager.CheckLotsForFinish();

            List<Win> Wins;
            using (DataContext db = new DataContext())
            {
                Wins = db.Wins.Include(x => x.Lot).Include(x => x.User).OrderByDescending(x => x.Time).ToList();
            }

            return View(Wins);
        }
    }
}
