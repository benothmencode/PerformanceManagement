﻿using AutoMapper;
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
using ProjectF.Helpers;
using Microsoft.AspNetCore.Http;

namespace ProjectF.Controllers
{
 
    public class BadgeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBadgeRepository _BadgeRepository;
        private readonly IUserRepository _UserRepository;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly ISystemeRepository _systemeRepository;
        private readonly IVoteRepository _VoteRepository;

        private readonly UserManager<User> _userManager;

        public BadgeController(IBadgeRepository badgeRepository,IMapper mapper, IVoteRepository VoteRepository,
            IUserRepository userRepository , ISystemeRepository systemeRepository ,IWebHostEnvironment webHostEnvironment , UserManager<User> userManager)
        {
            

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _BadgeRepository = badgeRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));
            _systemeRepository = systemeRepository ??
                throw new ArgumentNullException(nameof(systemeRepository));
            _UserRepository = userRepository;
            _WebHostEnvironment = webHostEnvironment;
            _VoteRepository = VoteRepository;
            _userManager = userManager;

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
        public IActionResult Listofbadges()
        {
            var badges = _BadgeRepository.GetAll().ToList();
            
            return View(badges);
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
        [HttpPost]

        [Route("/Admin/Badge/Create")]
        public IActionResult Create(int SystemeID, BadgeForCreationDto badgeForCreation)
        {
            var stringFileName = UploadFile(badgeForCreation.Icon);
            var statusCode = ValidateBadge(SystemeID, badgeForCreation);
            if (!ModelState.IsValid)
                return StatusCode(statusCode.StatusCode);
            if (ModelState.IsValid)
            {
                var badge = _mapper.Map<Badge>(badgeForCreation);
                badge.Icon = stringFileName;
                if (!_BadgeRepository.Create(SystemeID, badge,badge.TypeVoteId))
                {
                    ModelState.AddModelError("", $"Something went wrong saving the badge " +
                                                $"{badgeForCreation.Title}");
                    return StatusCode(500, ModelState);
                }

                return RedirectToAction("Listofbadges");
            }

            return View(badgeForCreation);
        }



        public IActionResult AddOrEditBadgeVote(int id = 0)
        {
            var VoteType = _VoteRepository.GetTypeVotes();
            var voteTypeList = new TypeVotesList(VoteType);
            if (id == 0)
                
                return View(new BadgesForVotes() { TypeVote = voteTypeList.GetvoteTypeList()});
            else
            {
                var BadgeForVote = _BadgeRepository.GetBadgeById(id);
                if (BadgeForVote == null)
                {
                    return NotFound();
                }
                return View(BadgeForVote);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEditBadgeVote(int id, int? TypeVoteId, [Bind("Id,Title,Icon,BadgeCriteria,Description,Periodicity,ValueOfPeriodicity,TypeVoteId,TypeVote")] BadgesForVotes badgeForVotes)
        {
            if (ModelState.IsValid)
            {
                var VoteRight = new VoteRights();
                var stringFileName = UploadFile(badgeForVotes.Icon);
                //Insert
                if (id == 0)
                {
                    var badge = _mapper.Map<Badge>(badgeForVotes);
                    badge.Icon = stringFileName;
                    _BadgeRepository.Create(badge.SystemeId, badge , TypeVoteId);
                    VoteRight.Quantity = badge.BadgeCriteria;
                    VoteRight.TypeVote = badge.TypeVote;
                    VoteRight.UserId = Int32.Parse(_userManager.GetUserId(User));
                    VoteRight.Update = badge.Created;
                    _VoteRepository.AddOrUpdateVoteRights(VoteRight.Id , VoteRight);

                }
                return RedirectToAction("Listofbadges");
            }
            return View(badgeForVotes);
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
        private string UploadFile(IFormFile badgeIcon)
        {
            string fileName = null;
            if (badgeIcon != null)
            {
                string uploadDir = Path.Combine(_WebHostEnvironment.WebRootPath, "theme/dist/img");
                fileName = Guid.NewGuid().ToString() + "-" + badgeIcon.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    badgeIcon.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public IActionResult CreateTypeVote()
        {
            return View(new TypeVote());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTypeVote([Bind("Id,Libellé")] TypeVote typeVote)
        {
            if (ModelState.IsValid)
            {
                _VoteRepository.CreateTypeVote(typeVote);
                return RedirectToAction(nameof(Listofbadges));
            }
            return View(typeVote);
        }




    }
}