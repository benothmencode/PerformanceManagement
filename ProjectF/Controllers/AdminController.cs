using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.ExernalServices;
using ProjectF.ViewModels;

namespace ProjectF.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ISystemeRepository _SystemeRepository;
        private readonly IBadgeRepository _BadgeRepository;

        private readonly ICommitsController _CommitsController;

        public AdminController(UserManager<User> userManager , RoleManager<AppRole> roleManager , ISystemeRepository systemeRepository 
            , IBadgeRepository badgeRepository , ICommitsController commitsController)
        {
            _userManager =userManager ??
              throw new ArgumentNullException(nameof(userManager));

            _roleManager = roleManager ??
             throw new ArgumentNullException(nameof(roleManager));
            _SystemeRepository = systemeRepository ??
             throw new ArgumentNullException(nameof(systemeRepository));
            _BadgeRepository = badgeRepository;
            _CommitsController = commitsController;

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserManagement()
        {
           return View(new UsersForAdmin(
            (from user in await _userManager.Users.ToListAsync()
             select new UsersForAdmin(user, GetUserRoles(user).Result)).ToList()));

        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            var resultifnull = new List<string>();
             var result = new List<string>(await _userManager.GetRolesAsync(user));
            if (result.Count != 0)
            {
                return result;
            }

            resultifnull.Add("No Roles Added");
            return resultifnull;
                
         }

        public IActionResult SystemManagement()
        {
            var Systemes = _SystemeRepository.GetSystemes();
            return View(Systemes);
        }

        public IActionResult CreateSysteme()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSysteme(Systeme systeme)
        {
            if (ModelState.IsValid)
            {
                _SystemeRepository.CreateSysteme(systeme);
                return RedirectToAction(nameof(SystemManagement));
            }
            return View(systeme);
        }

       

    }
}