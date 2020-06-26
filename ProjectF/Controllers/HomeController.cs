
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.HomeRepository;
using ProjectF.Models;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
namespace ProjectF.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IHomeRepository _HomeRepository;
        private readonly IEventRepository _eventRepository;
        public HomeController(IEventRepository eventRepository,ILogger<HomeController> logger, IHomeRepository homeRepository, IUserRepository userRepository, IBadgeRepository badgeRepository, IMapper mapper, IConfiguration configuration)
        {
         _logger = logger;

            _mapper = mapper ??
              throw new ArgumentNullException(nameof(mapper));

            _HomeRepository = homeRepository ??
                throw new ArgumentNullException(nameof(homeRepository));
            _eventRepository = eventRepository;
        }
      


        public IActionResult Index()
        {

            //_eventRepository.createeventeveryday();
            var dayevents = _eventRepository.getAllDayEventsForToday();
            var events = _eventRepository.GetAll().OrderByDescending(d=>d.Date);
            var model = _mapper.Map< IList<EventEntityDto>>(events);
            var model2 = _mapper.Map<IList<DayEventEntityDto>>(dayevents);
            var eventviewModel = new EventViewModel()
            {
                events = model,
                dayevents = model2
            };

            return View(eventviewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    
        //public bool NewBadgeisAdded()
        //{
        //    int nbreBadge = _badgeRepository.numberOfBadges();
            

        //    if (nbreBadge == nbreBadge + 1)
        //    {
        //        return true;
        //    }
        //    else return false;
        //}

    }
}
