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

namespace ProjectF.Controllers
{
    public class BadgeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBadgeRepository _BadgeRepository;
        public BadgeController(IBadgeRepository badgeRepository, IMapper mapper, IConfiguration configuration)
        {
            

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _BadgeRepository = badgeRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));

            
        }

        //public static List<Badge> badges = new List<Badge>()
        //{

        //        new Badge
        //        {
        //            Id=1,

        //           Challenge= "50 Commit",
        //         Title = "100Commit",
        //          UserProgression = 50,
        //           StartedAt = new DateTime(2020, 12, 05).ToString("dd/MM/yyyy"),
        //           BadgeDeadline = new DateTime(2021, 02, 06).ToString("dd/MM/yyyy"),
        //         ObtainDate = new DateTime(2020, 01, 06).ToString("dd/MM/yyyy"),
        //            Description="The challenge is simple , " +
        //            "for the month of january and february," +
        //            "By using Gitlab you need to commit 1000 times !Don't miss it work hard to earn this badge !",
        //            BadgeCriteria=1000,
        //            System="GitLab"
        //       },
        //        new Badge
        //      {   Id=2,
        //            Challenge = "30 Todos",
        //           Title="First Commit",
        //           UserProgression = 70,
        //          StartedAt = new DateTime(2020, 01, 05).ToString("dd/MM/yyyy"),
        //            BadgeDeadline = new DateTime(2020, 02, 06).ToString("dd/MM/yyyy"),
        //            ObtainDate = new DateTime(2020, 07, 09).ToString("dd/MM/yyyy"),
        //            Description="The challenge is simple , for the month of january and february,By using Gitlab you need to commit 1000 times !Don't miss it work hard to earn this badge !",
        //            BadgeCriteria=1000,
        //            System="Redmine"

        //      },



        //};



        [HttpGet()]
        [ActionName("GetAll")]
        public IActionResult GetAll()
        {
            var badges = _BadgeRepository.GetAll();
            var model = _mapper.Map<IList<BadgeEntityDto>>(badges);
            return Ok(model);
        }

        public IActionResult Index()
        {

            return View(/*badges*/);
        }



        public IActionResult Details(int? idBadge)
        {

            //Badge badge = badges.FirstOrDefault(b =>b.Id ==idBadge); 
            


            return View(/*badge*/);

        }





    }
}