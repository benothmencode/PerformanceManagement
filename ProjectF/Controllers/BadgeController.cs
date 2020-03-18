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

namespace ProjectF.Controllers
{
    public class BadgeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBadgeRepository _BadgeRepository;
        private readonly IUserRepository _userRepository;
        public BadgeController(IBadgeRepository badgeRepository, IUserRepository userRepository,IMapper mapper, IConfiguration configuration)
        {
            

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _BadgeRepository = badgeRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));

            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));


        }

     

     
     
        public IActionResult Index(int? idUser)
        {


            var badges = _BadgeRepository.GetAll();
            var userbadges = _BadgeRepository.GetUserBadge(idUser);
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
                ViewBag.BadgeMessage = $"{user.Username} has no Badges yet ";
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

            //Badge badge = badges.FirstOrDefault(b =>b.Id ==idBadge); 
            


            return View(/*badge*/);

        }





    }
}