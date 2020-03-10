using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Models;

namespace ProjectF.Controllers
{
    public class BadgeController : Controller
    {
        public IActionResult Index()
        {

            var badges = new List<Badge>();
            
            badges.Add(new Badge{ 
            Title="50 Commit",
            UserProgression=50,
                StartedAt = new DateTime(2020, 12, 05).ToString("dd/MM/yyyy"),
            BadgeDeadline=new DateTime(2021,02,06).ToString("dd/MM/yyyy")
            });

            badges.Add(new Badge
            {
                Title = "30 Todos",
                UserProgression = 70,
                StartedAt = new DateTime(2020, 01, 05).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 02, 06).ToString("dd/MM/yyyy")
            });


            return View(badges);
        }
    }
}