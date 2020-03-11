using Microsoft.AspNetCore.Mvc;
using ProjectF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectF.Controllers
{
    public class BadgeController : Controller
    {


        public static List<Badge> badges = new List<Badge>()
        {

            new Badge
            {
                Id=1,

               Challenge= "50 Commit",
             Title = "100Commit",
              UserProgression = 50,
               StartedAt = new DateTime(2020, 12, 05).ToString("dd/MM/yyyy"),
               BadgeDeadline = new DateTime(2021, 02, 06).ToString("dd/MM/yyyy"),
             ObtainDate = new DateTime(2020, 01, 06).ToString("dd/MM/yyyy"),
                Description="The challenge is simple , " +
                "for the month of january and february," +
                "By using Gitlab you need to commit 1000 times !Don't miss it work hard to earn this badge !",
                BadgeCriteria=1000,
                System="GitLab"
           },
            new Badge
          {   Id=2,
                Challenge = "30 Todos",
               Title="First Commit",
               UserProgression = 70,
              StartedAt = new DateTime(2020, 01, 05).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 02, 06).ToString("dd/MM/yyyy"),
                ObtainDate = new DateTime(2020, 07, 09).ToString("dd/MM/yyyy"),
                Description="The challenge is simple , for the month of january and february,By using Gitlab you need to commit 1000 times !Don't miss it work hard to earn this badge !",
                BadgeCriteria=1000,
                System="Redmine"

          },



    };
       
       


    
        public IActionResult Index()
        {

            return View(badges);
        }



        public IActionResult Details(int? idBadge)
        {

            Badge badge = badges.FirstOrDefault(b =>b.Id ==idBadge); 
            


            return View(badge);

        }





    }
}