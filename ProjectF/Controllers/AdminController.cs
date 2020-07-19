using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private readonly IWebHostEnvironment _WebHostEnvironment;

        public AdminController(UserManager<User> userManager, RoleManager<AppRole> roleManager, ISystemeRepository systemeRepository
            , IBadgeRepository badgeRepository, IUserRepository userRepository, IMapper mapper, IFlashMessage flashMessage , IWebHostEnvironment webHostEnvironment )
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
            _WebHostEnvironment = webHostEnvironment;

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
                string contentRootPath = _WebHostEnvironment.ContentRootPath;
               systeme.path = contentRootPath + "\\ExernalServices\\" + systeme.SystemName+".json" ;

                _SystemeRepository.CreateSysteme(systeme);
               
                return RedirectToAction("CreateUsersSystemForNewSysteme",systeme);
            }
            return View(systeme);
        }

        
        public IActionResult updateSystem(int systemeId)
        {
            var System = _SystemeRepository.GetSystemeById(systemeId);
            return View(System);

        }

        [HttpPost]
        public IActionResult updateSystem(Systeme systeme)
        {
            if (ModelState.IsValid)
            {
                
                systeme.Created = DateTime.Now;

                _SystemeRepository.UpdateSysteme(systeme);

                return RedirectToAction("SystemManagement", systeme);
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
            List<SystemUserViewModel> systemUserViews = new List<SystemUserViewModel>();
            var paths = systemes.Select(s => s.path).ToList();
            foreach (var path in paths)
            {
                var listitem = getUserSystemesIds(path);
                systemUserViews.Add(new SystemUserViewModel()
                {
                    UserSystemesIds = listitem,
                });
            }
            var userViewModel = new EditUserForAdmin()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                systemUserViewModels = systemUserViews,
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
                    if (uservm.systemUserViewModels.Count() != 0)
                    {
                        foreach (var Su in uservm.systemUserViewModels)
                        {
                            Su.systemeUser.Identifier = Su.IdUsersystemselected;
                            _SystemeRepository.updatesystemUser(Su.systemeUser);
                        }
                    }
                  
                    if (result.Succeeded) return RedirectToAction("UserManagement");
                }
            }
            ModelState.AddModelError("", "User not updated , something went wrong");
            return View(uservm);


        }

      
        public List<SelectListItem> getUserSystemesIds(string path)
        {
            //JObject u = JObject.Parse(System.IO.File.ReadAllText(@path));
            using (StreamReader file = System.IO.File.OpenText(@path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o1 = (JObject)JToken.ReadFrom(reader);

                var users = from u in o1["Users"]
                 select new { userName = (string)u["username"], id = (int)u["id"] };
                var IdUserSystListItem = users.Select(x => new SelectListItem()
                {
                    Text = x.userName,
                    Value = x.id.ToString()
                }).ToList();

                return IdUserSystListItem;
            }
         
        }


        [HttpGet]
        public IActionResult CreateUserSystem(User user)
        {
            List<SystemUserViewModel> systemUserViews = new List<SystemUserViewModel>();
            var SystemesList = _SystemeRepository.GetSystemes();
            var paths = SystemesList.Select(s => s.path).ToList();
            foreach(var path in paths)
            {
               var listitem = getUserSystemesIds(path);
                systemUserViews.Add(new SystemUserViewModel()
                {
                  UserSystemesIds = listitem,
                });
            }
            if (SystemesList.Count() != 0)
            {
                var sysListItem = SystemesList.Select(x => new SelectListItem()
                {
                    Text = x.SystemName,
                    Value = x.Id.ToString()
                }).ToList();
                var vm = new SystemUser()
                {
                    Systemes = sysListItem,
                    systemUserViewModels = systemUserViews,
                    userId = user.Id
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
            if (vm != null)
            {
                if (vm.systemUserViewModels.Count() != 0)
                {
                    foreach (var Su in vm.systemUserViewModels)
                    {
                        Su.systemeUser.Identifier = Su.IdUsersystemselected;
                        result = _SystemeRepository.CreateSystemUser(Su.systemeUser);
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
            List<SystemUserViewModel> systemUserViews = new List<SystemUserViewModel>();

            if (Users.Count() != 0)
            {
                for (int i = 0 ; i < Users.Count() ; i++)
                {
                    var path = systeme.path;
                    var listitem = getUserSystemesIds(path);
                    systemUserViews.Add(new SystemUserViewModel()
                    {
                        UserSystemesIds = listitem,
                    });

                }

                var UserListItem = Users.Select(x => new SelectListItem()
                {
                    Text = x.UserName,
                    Value = x.Id.ToString()
                }).ToList();

                var vm = new UserSystemForNewSysteme()
                {
                    Users = UserListItem,
                    SystemeId = systeme.Id,
                    systemUserViewModels = systemUserViews
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
                if (vm.systemUserViewModels.Count() != 0)
                {
                    foreach (var Su in vm.systemUserViewModels)
                    {
                        Su.systemeUser.Identifier = Su.IdUsersystemselected;
                        result = _SystemeRepository.CreateSystemUser(Su.systemeUser);
                    }
                    if (result == false) return View(vm);
                }
            }
            return RedirectToAction("SystemManagement");
        }

        [HttpPost]
        public IActionResult disabelSysteme(int SystemId)
        {
            bool result = false;
            var system = _SystemeRepository.GetSystemeById(SystemId);
            if (system != null && system.SystemIsArchieved == false )
            {

                 result = _SystemeRepository.DisableSystem(SystemId);
                return Json(new
                {
                    success = result,
                    responseText = system.SystemName + "disabled Successfully!! "
                });
            }
            return Json(new
            {
                success = result,
                responseText = "Something went wrong !! "
            });

        }

    }
}