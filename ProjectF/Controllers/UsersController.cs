using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;

namespace ProjectF.Controllers
{
    public class UsersController : Controller
    {
       
        private readonly IMapper _mapper;

        private readonly IWebHostEnvironment _WebHostEnvironment;

        private UserManager<User> _userManager;

        private readonly IUserRepository _userRepository;
        private readonly IUserBadgeRepository _userBadgeRepository ;

        public UsersController(IWebHostEnvironment webHostEnvironment, IMapper mapper,
            IUserRepository userRepository,
            UserManager<User> userManager,
            IUserBadgeRepository userBadgeRepository)
        {
            _WebHostEnvironment = webHostEnvironment;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _userManager = userManager ??
             throw new ArgumentNullException(nameof(userManager));
            _userBadgeRepository = userBadgeRepository;

        }

        // GET: Users
        public IActionResult Employees()
        {
            var users = _userRepository.GetUsers();
            if (users.Count() <= 0)
            {
                ViewBag.Message = "there was a prob retrieving users";
            }
            var model = _mapper.Map<IList<UserEntityDto>>(users);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EmployeesSearch(string Empsearch)
        {
            ViewData["GetEmployeedetails"] = Empsearch;
            var empquery = await _userRepository.GetUserByUsername(Empsearch);
            var modell = _mapper.Map<IList<UserEntityDto>>(empquery);
            return View(modell);

        }

        

        // GET: Users/Details/5
        public IActionResult Profile(int idUser)
        {
            var user = _userRepository.GetUserById(idUser);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UserEntityDto>(user);
            var Badgesobtained = _userBadgeRepository.GetUsersBadge(idUser).Where(b => b.State == "Done").ToList();
            var BadgesInProgress = _userBadgeRepository.GetUsersBadge(idUser).Where(b => b.State == "In progress").ToList();
            var voteHistories = _userRepository.TotalVotes(idUser);
            var userProfileviewModel = new UserProfileViewModel()
            {
                user = model,
                badgesInProgress = BadgesInProgress ,
                badgesObtained = Badgesobtained,
               VoteHistories = voteHistories
            };
            return View(userProfileviewModel);
        }


        [HttpGet]
        public async Task<IActionResult> EditProfile(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Description = user.Description,
                FirstName = user.FirstName,
                Job = user.Job,
                LastName = user.LastName,
                Location = user.Location,
                Skills = user.Skills,
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel vm)
        {
            
            var stringFileName = UploadFile(vm);
            
                var user = await _userManager.FindByIdAsync(vm.Id.ToString());
                if (user != null)
                {
                user.Description = vm.Description;
                user.FirstName = vm.FirstName;
                user.Job = vm.Job;
                user.LastName = vm.LastName;
                user.Location = vm.Location;
                user.Skills = vm.Skills;
                user.Email = vm.Email;
               if (stringFileName != null)
                {
                    user.Userimage = stringFileName;
                }
               var result = await _userManager.UpdateAsync(user);
                   if(result.Succeeded) return RedirectToAction("Profile","Users", routeValues: new { idUser = user.Id });

                ModelState.AddModelError("", "User not updated , something went wrong");
                return View(user);
                }


            return RedirectToAction("Profile");

        }

        private string UploadFile(UserViewModel vm)
        {
            string fileName = null;
            if (vm.Userimage != null)
            {
                string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath , "theme/dist/img");
                fileName = Guid.NewGuid().ToString() + "-" + vm.Userimage.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Userimage.CopyTo(fileStream);
                }
            }
            return fileName;
        }


    }
}