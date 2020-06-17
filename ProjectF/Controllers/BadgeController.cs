using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectF.ViewModels;
using PerformanceManagement.DATA.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using ProjectF.Components;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ProjectF.ExernalServices;
using Hangfire;

namespace ProjectF.Controllers
{
 
    public class BadgeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBadgeRepository _BadgeRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly ISystemeRepository _systemeRepository;
        private readonly ICommitsController _CommitsController;

        public BadgeController(IBadgeRepository badgeRepository,IMapper mapper, ICommitsController  commitsController,
            IUserRepository userRepository , ISystemeRepository systemeRepository ,IWebHostEnvironment webHostEnvironment)
        {
            

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _BadgeRepository = badgeRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));
            _systemeRepository = systemeRepository ??
                throw new ArgumentNullException(nameof(systemeRepository));
            _UserRepository = userRepository;
            _WebHostEnvironment = webHostEnvironment;
            _CommitsController = commitsController;

        }

        
        public IActionResult Index(int idUser)
        {
            var user = _UserRepository.GetUserById(idUser);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UserEntityDto>(user);
            var Badges = _BadgeRepository.GetUserBadge(idUser);
            if (Badges.Count() <= 0)
            {
                ViewBag.BadgeMessage = $"{user.UserName} has no Badges yet ";
            }
            var model2 = _mapper.Map<IList<BadgeEntityDto>>(Badges);

            var userProfileviewModel = new UserProfileViewModel()
            {
                user = model,
                badges = model2
            };
            return View(userProfileviewModel);

           
        }

        public IActionResult Details(int? idBadge)
        {

            var badges = _BadgeRepository.GetAll();
            var model = _mapper.Map <IList<BadgeEntityDto>>(badges);
            BadgeEntityDto badge = model.FirstOrDefault(b =>b.Id ==idBadge); 
            


            return View(badge);

        }

        [Authorize(Roles ="Administrator")]
        [Route("/Admin/Badges")]
        public IActionResult Listofbadges()
        {
            var badges = _BadgeRepository.GetAll();
            var model = _mapper.Map<IList<BadgeEntityDto>>(badges);
            return View(model);
        }



        [Authorize(Roles = "Administrator")]
        [Route("/Admin/Badge/Create")]
        public IActionResult Create()
        {
            var Systemes = _systemeRepository.GetSystemes();
            var SystemeModel = _mapper.Map<IList<SystemeEntityDto>>(Systemes);
            var systemeList = new SystemesList(SystemeModel.ToList());
            BadgeForCreationDto BadgeForCreationDto = new BadgeForCreationDto { systemes = systemeList.GetSystemesList() };
            return View(BadgeForCreationDto);
        }

        [Authorize(Roles = "Administrator")]
        [Route("/Admin/Badge/Count")]
        public int Countbadges()
        {
           return _BadgeRepository.numberOfBadges();
        }


        [Authorize(Roles = "Administrator")]
        [HttpPost]

        [Route("/Admin/Badge/Create")]
        public IActionResult Create(int SystemeID, BadgeForCreationDto badgeForCreation)
        {
            var stringFileName = UploadFile(badgeForCreation);
            var statusCode = ValidateBadge(SystemeID, badgeForCreation);
            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);
            if (ModelState.IsValid)
            {
                var badge = _mapper.Map<Badge>(badgeForCreation);
                badge.Icon = stringFileName;
                if (!_BadgeRepository.Create(SystemeID, badge))
                {
                    ModelState.AddModelError("", $"Something went wrong saving the badge " +
                                                $"{badgeForCreation.Title}");
                    return StatusCode(500, ModelState);
                }

                return RedirectToAction("Listofbadges");
            }

            return View(badgeForCreation);
        }

        private StatusCodeResult ValidateBadge(int SystemId, BadgeForCreationDto badge)
        {
            if (badge == null || SystemId <= 0)
            {
                ModelState.AddModelError("", "Missing badge, user, or system");
                return BadRequest();
            }
           if (!_systemeRepository.SystemeExists(SystemId))
           {
                    ModelState.AddModelError("", "Systeme Not Found");
                    return StatusCode(404);
           }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Critical Error");
                return BadRequest();
            }

            return NoContent();
        }
        private string UploadFile(BadgeForCreationDto badge)
        {
            string fileName = null;
            if (badge.Icon != null)
            {
                string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "theme/dist/img");
                fileName = Guid.NewGuid().ToString() + "-" + badge.Icon.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    badge.Icon.CopyTo(fileStream);
                }
            }
            return fileName;
        }

       


    }
}