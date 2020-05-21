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

        public UsersController(IWebHostEnvironment webHostEnvironment , IMapper mapper, 
            IUserRepository userRepository,
            UserManager<User> userManager)
        {
            _WebHostEnvironment = webHostEnvironment;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _userManager = userManager ??
             throw new ArgumentNullException(nameof(userManager));

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

        [HttpGet]
        public async Task<IActionResult> Employees(string Empsearch)
        {
            ViewData["GetEmployeedetails"] = Empsearch;
            var empquery = await _userRepository.GetUserByUsername(Empsearch);
            var modell = _mapper.Map<IList<UserEntityDto>>(empquery);
            return View(modell);

        }

        

        // GET: Users/Details/5
        public IActionResult Profile(int? idUser)
        {
            if (idUser == null)
            {
                return NotFound();
            }
            var user = _userRepository.GetUserById(idUser);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UserEntityDto>(user);
            var Badges = _userRepository.GetAllUserbadgesForAuser(idUser);
            if (Badges.Count() <= 0)
            {
                //ViewBag.BadgeMessage = $"{user.Username} has no Badges yet ";
            }
            var model2 = _mapper.Map<IList<BadgeEntityDto>>(Badges);

            var userProfileviewModel = new UserProfileViewModel()
            {
                user = model,
                badges = model2
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
            if (vm != null)
            {
                var user = new User
                {
                    Id = vm.Id,
                    Description = vm.Description,
                    FirstName = vm.FirstName,
                    Job = vm.Job,
                    LastName = vm.LastName,
                    Location = vm.Location,
                    Skills = vm.Skills,
                    Userimage = stringFileName,
                };
                _userRepository.Edit(user);
                    return RedirectToAction("Profile");
               
             
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