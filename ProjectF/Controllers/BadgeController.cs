﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using Vereyon.Web;
using static ProjectF.Helpers.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectF.BadgeJobs;

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
        private readonly IUserBadgeRepository _UserBadgeRepository;
        private readonly IFlashMessage _flashMessage;
        private IJobService _jobService;

        private readonly UserManager<User> _userManager;

        public BadgeController(IBadgeRepository badgeRepository,IMapper mapper, IVoteRepository VoteRepository,
            IUserRepository userRepository , ISystemeRepository systemeRepository ,IWebHostEnvironment webHostEnvironment ,
            UserManager<User> userManager , IUserBadgeRepository userBadgeRepository , IFlashMessage  flashMessage,
            IJobService jobService)
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

            _UserBadgeRepository = userBadgeRepository;
            _flashMessage = flashMessage;

            _jobService = jobService;
        }


        public IActionResult Index(int idUser)
        {
            var user = _UserRepository.GetUserById(idUser);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UserEntityDto>(user);
            var UserBadges = _UserBadgeRepository.GetUsersBadge(idUser).Where(ub => ub.Badge.IsArchieved != true);
            if (UserBadges.Count() <= 0)
            {
                ViewBag.BadgeMessage = $"{user.UserName} has no Badges yet ";
            }
            var ListBadgesObtained = UserBadges.Where(ub => ub.State == "Done").ToList();
            var ListBadgesInProgress= UserBadges.Where(ub => ub.State == "In progress").ToList();
            var BadgesListes = new BadgesListesViewModel()
            {
                BadgesObtained = ListBadgesObtained,
                BadgesInProgress = ListBadgesInProgress,
            };
            return View(BadgesListes);

           
        }

        public IActionResult Details(int? idBadge)
        {
            var badge = _BadgeRepository.GetBadgeById(idBadge);
            var model = _mapper.Map <BadgeEntityDto>(badge);
            return View(model);

        }

       
        
        [Authorize(Roles ="Administrator")]
        public IActionResult Listofbadges()
        {
            var badges = _BadgeRepository.GetAll().Where(b => b.SystemIsArchieved != true).ToList();
            
            return View(badges);
        }



        [Authorize(Roles = "Administrator")]
        [Route("/Admin/Badge/Create")]
        public IActionResult Create()
        {
            var Systemes = _systemeRepository.GetSystemes().Where(sy => sy.SystemIsArchieved == false);
            if (Systemes.Count() != 0)
            {
                var SystemeModel = _mapper.Map<IList<SystemeEntityDto>>(Systemes);
                var systemeList = new SystemesList(SystemeModel.ToList());
                BadgeForCreationDto BadgeForCreationDto = new BadgeForCreationDto
                {
                    systemes = systemeList.GetSystemesList(),
                    JobIds = _jobService.getJobIds().Select(s => new SelectListItem { Text = s, Value = s }).ToList()
                };
                return View(BadgeForCreationDto);
            }
            else
            {
                _flashMessage.Warning("You need To Add a System First");
                return RedirectToAction("CreateSysteme", "Admin");
            }

        }



        [Authorize(Roles = "Administrator")]
        [HttpPost]

        [Route("/Admin/Badge/Create")]
        public IActionResult Create(int SystemeID, BadgeForCreationDto badgeForCreation)
        {
            var stringFileName = UploadFile(badgeForCreation.Icon);
            var statusCode = ValidateBadge(SystemeID, badgeForCreation);
            
             
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

                _jobService.startJob(badge.jobId);
                return RedirectToAction("Listofbadges");
            }

            return RedirectToAction("Create", badgeForCreation);
        }



        [Authorize(Roles = "Administrator")]
        public IActionResult updateBadgeSystem(int idBadge)
        {
            var Systemes = _systemeRepository.GetSystemes().Where(sy => sy.SystemIsArchieved == false);
            var badge = _BadgeRepository.GetBadgeById(idBadge);
            if (Systemes.Count() != 0 && badge != null)
            {
                var SystemeModel = _mapper.Map<IList<SystemeEntityDto>>(Systemes);
                var systemeList = new SystemesList(SystemeModel.ToList());
                BadgeForCreationDto BadgeForCreationDto = new BadgeForCreationDto
                {
                    Id = badge.Id,
                    Periodicity = badge.periodicity,
                    Description = badge.Description,
                    BadgeCriteria = badge.BadgeCriteria,
                    Title = badge.Title,
                    ValueOfPeriodicity = badge.ValueOfPeriodicity,
                    Created = badge.Created,
                    systemes = systemeList.GetSystemesList(),
                    JobIds = _jobService.getJobIds().Select(s => new SelectListItem { Text = s, Value = s }).ToList()
                };
                return View(BadgeForCreationDto);
            }
            else
            {
                _flashMessage.Warning("You need To Add a System First");
                return RedirectToAction("CreateSysteme", "Admin");
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult updateBadgeSystem(BadgeForCreationDto badgeForCreation)
        {
            var stringFileName = UploadFile(badgeForCreation.Icon);


            if (ModelState.IsValid)
            {
                var badge = _mapper.Map<Badge>(badgeForCreation);
                badge.Icon = stringFileName;
                if (!_BadgeRepository.update(badge))
                {
                    ModelState.AddModelError("", $"Something went wrong saving the badge " +
                                                $"{badgeForCreation.Title}");
                    return StatusCode(500, ModelState);
                }

                _jobService.startJob(badge.jobId);
                return RedirectToAction("Listofbadges");
            }

            return RedirectToAction("Create", badgeForCreation);

        }


        [NoDirectAccess]
        public IActionResult AddOrEditBadgeVote(int id = 0)
        {
            var VoteType = _VoteRepository.GetTypeVotes();
            if (VoteType.Count() != 0)
            {
                var voteTypeList = new TypeVotesList(VoteType);
                if (id == 0)
                    return View(new BadgesForVotes() { TypeVote = voteTypeList.GetvoteTypeList() });
                else
                {
                    var BadgeForVote = _BadgeRepository.GetBadgeById(id);

                    if (BadgeForVote != null)
                    {
                        var badgedto = new BadgesForVotes()
                        {
                            Id = BadgeForVote.Id,
                            Periodicity = BadgeForVote.periodicity,
                            Description = BadgeForVote.Description,
                            BadgeCriteria = BadgeForVote.BadgeCriteria,
                            Title = BadgeForVote.Title,
                            ValueOfPeriodicity = BadgeForVote.ValueOfPeriodicity,
                            TypeVote = voteTypeList.GetvoteTypeList(),
                            TypeVoteId = (int)BadgeForVote.TypeVoteId,
                            Created = BadgeForVote.Created,
                           
                        };
                        return View(badgedto);
                    }
                    return RedirectToAction("Listofbadges");
                }
            }
            else
            {

                _flashMessage.Warning("You need To Add a Type Vote First");
                return RedirectToAction("CreateTypeVote");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEditBadgeVote(int id, int? TypeVoteId, [Bind("Id,Title,Icon,BadgeCriteria,Description,Periodicity,ValueOfPeriodicity,TypeVoteId,TypeVote,Created")] BadgesForVotes badgeForVotes)
        {
            if (ModelState.IsValid)
            {
                
                var stringFileName = UploadFile(badgeForVotes.Icon);
                //Insert
                if (id == 0)
                {
                    var badge = _mapper.Map<Badge>(badgeForVotes);
                    badge.Icon = stringFileName;
                    _BadgeRepository.Create(badge.SystemeId, badge , TypeVoteId);
                    var users = _UserRepository.GetUsers();
                    foreach (var user in users)
                    {
                        var VoteRight = new VoteRights();
                        VoteRight.Quantity = badge.BadgeCriteria;
                        VoteRight.TypeVote = badge.TypeVote;
                        VoteRight.UserId = user.Id;
                        VoteRight.Update = badge.Created;
                        _VoteRepository.AddOrUpdateVoteRights(VoteRight.Id, VoteRight);
                    }

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Listofbadges", _BadgeRepository.GetAll().ToList()) });
                }
                else if(id != 0)
                {
                    var badge = _mapper.Map<Badge>(badgeForVotes);
                    badge.Icon = stringFileName;

                    _BadgeRepository.update(badge);
                    var users = _UserBadgeRepository.GetUsersBadge(badge).Select(ub => ub.User).ToList();
                    foreach (var user in users)
                    {
                        var voteRights = _BadgeRepository.GetVoteRights(user, badge.Created);
                        voteRights.Quantity = badge.BadgeCriteria;
                        voteRights.TypeVoteId = (int)TypeVoteId;
                        _VoteRepository.AddOrUpdateVoteRights(voteRights.Id, voteRights);
                    }

                    return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "Listofbadges", _BadgeRepository.GetAll().ToList()) });

                }
            }

      
            return RedirectToAction("AddOrEditBadgeVote", badgeForVotes);
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



        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult disableBadge(int idBadge)
        {
           
            var badge = _BadgeRepository.GetBadgeById(idBadge);
            if (badge != null && badge.IsArchieved == false)
            {
                _BadgeRepository.DesactivateBadge(idBadge,!badge.IsArchieved);
                var users = _UserBadgeRepository.GetUsersBadge(badge).Select(ub => ub.User).ToList();
                foreach (var user in users)
                {
                    var voteRights = _BadgeRepository.GetVoteRights(user, badge.Created);
                    voteRights.BadgeDisabled = true;
                    _VoteRepository.AddOrUpdateVoteRights(voteRights.Id, voteRights);
                }

                return Json(new
               {
                   success = true,
                   responseText = badge.Title + " disabled Successfully!! "
               });
            }
            return Json(new
            {
                success = false,
                responseText = "Something went wrong !! "
            });
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult enableBadge(int idBadge)
        {
            var badge = _BadgeRepository.GetBadgeById(idBadge);
            if(badge != null)
            {
                _BadgeRepository.DesactivateBadge(idBadge, !badge.IsArchieved);
                var users = _UserBadgeRepository.GetUsersBadge(badge).Select(ub => ub.User).ToList();
                foreach (var user in users)
                {
                    var voteRights = _BadgeRepository.GetVoteRights(user, badge.Created);
                    voteRights.BadgeDisabled = false;
                    _VoteRepository.AddOrUpdateVoteRights(voteRights.Id, voteRights);
                }
                return Json(new
                {
                    success = true,
                    responseText = badge.Title + " disabled Successfully!! "
                });
            }
            return Json(new
            {
                success = false,
                responseText = "Something went wrong !! "
            });

         }

    }

}

