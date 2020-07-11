using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.ExernalServices;
using ProjectF.ModelsDTOS;
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
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminController(UserManager<User> userManager , RoleManager<AppRole> roleManager , ISystemeRepository systemeRepository 
            , IBadgeRepository badgeRepository , IUserRepository userRepository , IMapper mapper)
        {
            _userManager =userManager ??
              throw new ArgumentNullException(nameof(userManager));

            _roleManager = roleManager ??
             throw new ArgumentNullException(nameof(roleManager));
            _SystemeRepository = systemeRepository ??
             throw new ArgumentNullException(nameof(systemeRepository));
            _BadgeRepository = badgeRepository;
            _userRepository = userRepository;
            _mapper = mapper;

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

        [HttpPost]
        public async Task<IActionResult> DesactivateUser(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user != null && user.Active)
            {
                bool result = false;
                _userRepository.DesactivateOrActivateUser(UserId,result);
                return Json(new
                {
                    success = true,
                    responseText = "Employee disabled Successfully!! "
                });
            }
            return Json(new
            {
                success = false,
                responseText = "Something went wrong !! "
            });
        }


   
        [HttpPost]
        public async Task<IActionResult> activateUser(int UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user != null && !user.Active)
            {
                bool result = true;
                _userRepository.DesactivateOrActivateUser(UserId , result);
                return Json(new
                {
                    success = true,
                    responseText = "Employee enabled Successfully!! "
                });
            }
            return Json(new
            {
                success = false,
                responseText = "Something went wrong !! "
            });

        }



         [HttpGet]
        public  IActionResult EditUserAccount(int UserId)
        {
            var user = _userRepository.GetUserById(UserId);
            var system = _SystemeRepository.GetGitlab("Gitlab");
            var Gitlab = user.SystemeUsers.Where(s => s.SystemeId == system.Id).FirstOrDefault();
            var userViewModel = new EditUserForAdmin()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
               UserName = user.UserName,
             systemeUser = Gitlab
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserAccount(EditUserForAdmin uservm)
        {
           
                if (ModelState.IsValid)
                {
                var user = await _userManager.FindByIdAsync(uservm.Id.ToString());
                if (user != null)
                {
                    var systemuser = uservm.systemeUser;
                    user.FirstName = uservm.FirstName;
                    user.LastName = uservm.LastName;
                    user.UserName = uservm.UserName;
                    if (uservm.Password != null)
                    {
                        // Use passwordHash to add new password
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, uservm.Password);
                        //update user password
                    }
                    var result = await _userManager.UpdateAsync(user);

                    if(systemuser != null)
                    {
                        systemuser.UserId = uservm.Id;
                        _SystemeRepository.updatesystemUser(systemuser);
                    }

                    if (result.Succeeded) return RedirectToAction("UserManagement");
                }
            }
            ModelState.AddModelError("", "User not updated , something went wrong");
            return View(uservm);
        

        }
    }
}