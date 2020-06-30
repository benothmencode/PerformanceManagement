using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories.PBIRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;

namespace ProjectF.Controllers
{
    public class PBIController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        
        private readonly IUserBadgeRepository _UserBadgeRepository;
        private readonly IPBIRepository _PBIRepository;


        public PBIController( IMapper mapper,  IWebHostEnvironment webHostEnvironment,
            IUserBadgeRepository userBadgeRepository, IPBIRepository pBIRepository)
        {


            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _PBIRepository = pBIRepository;

            _UserBadgeRepository = userBadgeRepository;
           
        }


        public  void create()
        {
            _PBIRepository.CreatePBI();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
