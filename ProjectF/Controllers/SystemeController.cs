using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectF.Models;

namespace ProjectF.Controllers
{
    public class SystemeController : Controller
    {
        static IList<Systeme> Systemes = new List<Systeme>()
        { new Systeme()
        {
             Id=1,
             Description="System Description",
             SystemName="GitLab",
             urlUserSystemAccount="https://gitlab.com/users/sign_in",
             Badges = new List<Badge>(){
                new Badge() {
                Id = 1,
                Icon = "BadgeImage",
                Title = "50 commits",
                BadgeCriteria = 50,
                UserProgression = 25,
                StartedAt = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 05, 06).ToString("dd/MM/yyyy")},
                new Badge() {
                Id = 2,
                Icon = "BadgeImage",
                Title = "30 Todos",
                BadgeCriteria = 30,
                UserProgression = 70,
                StartedAt = new DateTime(2029, 04, 06).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy")} }
        },
             new Systeme()
        {
             Id=1,
             Description="System Description",
             SystemName="Redmine",
             urlUserSystemAccount="https://www.redmine.org/login",
             Badges = new List<Badge>(){
                new Badge() {
                Id = 1,
                Icon = "BadgeImage",
                Title = "RedmineBadge1",
                BadgeCriteria = 50,
                UserProgression = 25,
                StartedAt = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 05, 06).ToString("dd/MM/yyyy")},
                new Badge() {
                Id = 2,
                Icon = "BadgeImage",
                Title = "RedmineBadge1",
                BadgeCriteria = 30,
                UserProgression = 70,
                StartedAt = new DateTime(2029, 04, 06).ToString("dd/MM/yyyy"),
                BadgeDeadline = new DateTime(2020, 03, 06).ToString("dd/MM/yyyy")} }


        }};
        public IActionResult HelpCenter()
        {
            return View(Systemes);
        }
    }
}