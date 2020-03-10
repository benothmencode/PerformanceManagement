using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerformanceManagement.ENTITIES;
using ProjectF.Models;

namespace ProjectF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var d = new DateTime(2020,10,9);
            //ViewBag.date = d;
            //var c = d.Minute;
            //ViewBag.time =c;
            
            //var sender = new User();
            //var username = sender.Username = "Racha";
            //ViewBag.sender = username;

            //var action = "Send you an email";
            //ViewBag.action = action;


            //var description = "this is the description of the action";
            //ViewBag.description = description;

            

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
    }
}
