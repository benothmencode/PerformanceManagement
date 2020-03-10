using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Models;

namespace ProjectF.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Profile()
        {
            
            var badges = new List<Badge>();

            badges.Add(new Badge()
            {
                Id = 1,
                Icon = "BadgeImage",
                Title = "50 commits",
                BadgeCriteria = 50,
                UserProgression = 25,
                StartedAt = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 05, 06).ToString("dd/MM/yyyy")
            });
            badges.Add(new Badge()
            {
                Id = 2,
                Icon = "BadgeImage",
                Title = "30 Todos",
                BadgeCriteria = 30,
                UserProgression = 70,
                StartedAt = new DateTime(2029, 04, 06).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy")
            });
            var user = new User()
            {
                Id = 1,
                Username = "Wijden",
                Userimage = "user4-128x128.jpg",
                Job = "Software Engineer",
                Description = "Hi i'm a Member of IyaSoft company",
                Location = "Manouba,Tunisia",
                Skills = "UI Design Coding Javascript PHP Node.js",
                Badges=badges
            };
           
          
            return View(user);
        }

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