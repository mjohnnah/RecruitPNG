using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruitPNG.Models;
using RecruitPNG.Services;
using RecruitPNG.Web.Models;
using RecruitPNG.Web.ViewModels;

namespace RecruitPNG.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IJobService jobService;
        private readonly ISubscriptionService subscriptionService;
        public HomeController(IJobService jobService, ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
            this.jobService = jobService;
        }
        public IActionResult Index(string search = "")
        {

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                ViewBag.FeaturedJobs = jobService.Search(search);
            }
            else
            {
                ViewBag.FeaturedJobs = jobService.GetFeaturedJobs();
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AddSubscription(string email)
        {
            var subscription = new Subscription() { Email = email, IsSubscribed = true };
            subscriptionService.Insert(subscription);
            return RedirectToAction("Index", new { subscribtion = "ok" });
        }
    }
}