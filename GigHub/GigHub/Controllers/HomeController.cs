﻿using GigHub.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using System;
using GigHub.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext context;

        public HomeController()
        {
            this.context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingGigs = this.context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs"
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}