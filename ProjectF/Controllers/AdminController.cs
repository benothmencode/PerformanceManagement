using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.ExernalServices;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;
using Vereyon.Web;

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

        private readonly IFlashMessage _flashMessage;

        public AdminController(UserManager<User> userManager, RoleManager<AppRole> roleManager, ISystemeRepository systemeRepository
            , IBadgeRepository badgeRepository, IUserRepository userRepository, IMapper mapper, IFlashMessage flashMessage)
        {
            _userManager = userManager ??
              throw new ArgumentNullException(nameof(userManager));

            _roleManager = roleManager ??
             throw new ArgumentNullException(nameof(roleManager));
            _SystemeRepository = systemeRepository ??
             throw new ArgumentNullException(nameof(systemeRepository));
            _BadgeRepository = badgeRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _flashMessage = flashMessage;

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
               
                return RedirectToAction("CreateUsersSystemForNewSysteme",systeme);
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
                _userRepository.DesactivateOrActivateUser(UserId, result);
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
                _userRepository.DesactivateOrActivateUser(UserId, result);
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
        public IActionResult EditUserAccount(int UserId)
        {
            var user = _userRepository.GetUserById(UserId);
            var systemes = _SystemeRepository.GetSystemes(user.Id);
            var userViewModel = new EditUserForAdmin()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                systemesUser = user.SystemeUsers,
                 Systems = systemes.Select(x => new SelectListItem()
                 {
                     Text = x.SystemName,
                     Value = x.Id.ToString()
                 }).ToList()
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
                    if(uservm.systemesUser.Count() != 0)
                    {
                        foreach(var us in uservm.systemesUser)
                        {
                            _SystemeRepository.updatesystemUser(us);
                        }
                    }
                   
                    if (result.Succeeded) return RedirectToAction("UserManagement");
                }
            }
            ModelState.AddModelError("", "User not updated , something went wrong");
            return View(uservm);


        }

        [ActionName("GetSystemesIds")]
        public List<Redmine.Net.Api.Types.User> getUserSystemesIds()
        {
            var MyList = JsonConvert.DeserializeObject<List<Redmine.Net.Api.Types.User>>(System.IO.File.ReadAllText("C:\\Users\\PC HIMY\\source\\repos\\PerformanceManagement\\ProjectF\\ExernalServices\\RedmineUsers.json"));
            return MyList;
        }

        [HttpGet]
        public IActionResult CreateUserSystem(User user)
        {
            var SystemesList = _SystemeRepository.GetSystemes();
            var redmineUsers = getUserSystemesIds();

            if (SystemesList.Count() != 0)
            {

                var sysListItem = SystemesList.Select(x => new SelectListItem()
                {
                    Text = x.SystemName,
                    Value = x.Id.ToString()
                }).ToList();

                var IdUserSystListItem = redmineUsers.Select(x => new SelectListItem()
                {
                    Text = x.FirstName,
                    Value = x.Id.ToString()
                }).ToList();

                var vm = new SystemUser()
                {
                    Systemes = sysListItem,
                    userId = user.Id,
                    UserSystemesIds = IdUserSystListItem
                    
                };
                return View(vm);
            }
            _flashMessage.Warning("You need To Add a System First");
            return RedirectToAction("CreateSysteme", "Admin");

        }

        [HttpPost]
        public IActionResult CreateUserSystem(SystemUser vm)
        {
            bool result = true;
           if(vm != null)
            {
                if(vm.systemeUsers.Count() != 0)
                {
                    foreach(var Su in vm.systemeUsers)
                    {
                        result = _SystemeRepository.CreateSystemUser(Su);
                    }
                    if (result == false) return View(vm);
                }
            }
            return RedirectToAction("UserManagement");
        }

        [HttpGet]
        public IActionResult CreateUsersSystemForNewSysteme(Systeme systeme)
        {
            var Users = _userRepository.GetUsers();
            if (Users.Count() != 0)
            {

                var UserListItem = Users.Select(x => new SelectListItem()
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                }).ToList();

                var vm = new UserSystemForNewSysteme()
                {
                    Users = UserListItem,
                    SystemeId = systeme.Id,

                };
                return View(vm);
            }
            _flashMessage.Warning("You need To Add a User First");
            return RedirectToPage("Register", "Identity");

        }

        [HttpPost]
        public IActionResult CreateUsersSystemForNewSysteme(UserSystemForNewSysteme vm)
        {
            bool result = true;
            if (vm != null)
            {
                if (vm.systemeUsers.Count() != 0)
                {
                    foreach (var Su in vm.systemeUsers)
                    {
                        result = _SystemeRepository.CreateSystemUser(Su);
                    }
                    if (result == false) return View(vm);
                }
            }
            return RedirectToAction("SystemManagement");
        }



    }
}