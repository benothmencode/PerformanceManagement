
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectF.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace ProjectF.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    var users = new List<User>();
        //    users.Add(new User() { Id = 1, Username = "Racha" });
        //    users.Add(new User() { Id = 2, Username = "Wijden" });
        //    var events = new List<Event>();
        //    var DayEvents = new List<DayEvent>();
        //    var DayEvents2 = new List<DayEvent>();
        //    DayEvents.Add(new DayEvent
        //    {
        //        Id = 2,
        //        Title = "first",
        //        Description = "this is a description",
        //        User = users.FirstOrDefault(user => user.Id == 1),
        //        Action = "Commented your photo",
        //        TimeLeft = new System.TimeSpan()

        //    });
        //    DayEvents.Add(new DayEvent
        //    {
        //        Id = 1,
        //        Title = "Second",
        //        Description = "this is a description",
        //        User = users.FirstOrDefault(user => user.Id == 2),
        //        Action = "Commented your photo"
        //    });


        //    events.Add(new Event
        //    {

        //        Date = new System.DateTime(2020, 05, 03).ToString("dd/MM/yyyy"),
        //        DayEvents = DayEvents

        //    });

        //    DayEvents2.Add(new DayEvent
        //    {
        //        Id = 3,
        //        Title = "second",
        //        Description = "this is a description",
        //        User = users.FirstOrDefault(user => user.Id == 1),
        //        Action = "Commented your photo",
        //        TimeLeft = new System.TimeSpan()

        //    });
        //    events.Add(new Event
        //    {

        //        Date = new System.DateTime(2030, 10,5).ToString("dd/MM/yyyy"),
        //        DayEvents = DayEvents2

        //    });


        //    return View(events);
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
