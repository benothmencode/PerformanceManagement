using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.ENTITIES;
using ProjectF.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectF.Controllers
{
    public class UserController : Controller
    {
        //static IList<Vote> Votess = VoteController.votes;
        //static IList<Badge> badges = new List<Badge>(){
        //    new Badge()
        //    {
        //        Id = 1,
        //        Icon = "BadgeImage",
        //        Title = "50 commits",
        //        BadgeCriteria = 50,
        //        UserProgression = 25,
        //        StartedAt = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy"),
        //        BadgeDeadline = new DateTime(2020, 05, 06).ToString("dd/MM/yyyy")
        //    },
        //    new Badge()
        //     {
        //    Id = 2,
        //        Icon = "BadgeImage",
        //        Title = "30 Todos",
        //        BadgeCriteria = 30,
        //        UserProgression = 70,
        //        StartedAt = new DateTime(2029, 04, 06).ToString("dd/MM/yyyy"),
        //        BadgeDeadline = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy")
        //    }
        //};
        
        //public static IList<User> users = new List<User>()
        //{new User()
        //    {
        //        Id = 1,
        //        Username = "Wijden",
        //        Userimage = "user4-128x128.jpg",
        //        Job = "Software Engineer",
        //        Description = "Hi i'm a Member of IyaSoft company",
        //        Location = "Manouba,Tunisia",
        //        Skills = "UI Design Coding Javascript PHP Node.js",
        //        Badges=badges,
                
        //    },
        //    new User() { Id = 2,
        //        Username = "racha",
        //        Userimage ="user8-128x128.jpg",
        //        Job = "Software Engineer",
        //        Description = "Hi i'm a Member of IyaSoft company",
        //        Skills = "UI Design Coding Javascript PHP Node.js",
        //        Location = "Manouba-Tunisia" ,
        //        Badges=badges,
               
        //    },
        //    new User() { Id = 3,
        //        Username = "Hassen",
        //        Userimage ="user1-128x128.jpg",
        //        Job = "Software Engineer",
        //        Description = "Hi i'm a Member of IyaSoft company",
        //        Skills = "UI Design Coding Javascript PHP Node.js",
        //        Location = "Tunisia",
        //        Badges=badges,
               
        //    },
        //    new User() { Id = 4, Username = "jihed",
        //        Userimage ="user2-160x160.jpg",
        //        Job = "Software Engineer",
        //        Description = "Hi i'm a Member of IyaSoft company",
        //        Skills = "UI Design Coding Javascript PHP Node.js",
        //        Location = "Manouba-Tunisia",
        //        Badges=badges,
                
        //    }
        //};
        
        
        //// GET: User
        //public ActionResult Profile(int idUser)
        //{
        //    User user = users.FirstOrDefault(u => u.Id == idUser);
        //    return View(user);
        //}


        //public ActionResult ListEmployees()
        //{
        //    return View("Employees", users);
        //}

        //[HttpGet]
        //public IActionResult ListEmployees([FromQuery] int idVote)
        //{
        //    ViewData["idvote"] = idVote;
        //    return View("Employees", users);
        //}

        //    // GET: User/Details/5
        //    public ActionResult Details(int id)
        //    {
        //        return View();
        //    }

        //    // GET: User/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: User/Create
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add insert logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: User/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: User/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add update logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: User/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: User/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            // TODO: Add delete logic here

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
        //}
    }
}