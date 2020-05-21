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

namespace ProjectF.Controllers
{
 
    public class BadgeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBadgeRepository _BadgeRepository;
        private readonly IUserRepository _UserRepository;

        public BadgeController(IBadgeRepository badgeRepository,IMapper mapper, IConfiguration configuration, IUserRepository userRepository)
        {
            

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _BadgeRepository = badgeRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));
            _UserRepository = userRepository;


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
            var badges = _BadgeRepository.GetAll();
            var model = _mapper.Map<IList<BadgeEntityDto>>(badges);

            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create(Badge badge)
        {
            _BadgeRepository.Create(badge);
           

            return View();
        }


    }
}