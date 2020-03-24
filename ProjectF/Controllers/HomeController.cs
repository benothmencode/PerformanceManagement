
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IHomeRepository _HomeRepository;
      
        public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository, IMapper mapper, IConfiguration configuration)
        {
         _logger = logger;

            _mapper = mapper ??
              throw new ArgumentNullException(nameof(mapper));

            _HomeRepository = homeRepository ??
                throw new ArgumentNullException(nameof(homeRepository));

        }


       

        public IActionResult Index()
        {

            var events = _HomeRepository.GetAll();
            var dayevents = _HomeRepository.GetAlldayevents();
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
    }
}
