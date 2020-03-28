using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories.SystemeRepository;
using ProjectF.Components;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;

namespace ProjectF.Controllers
{
    public class SystemeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISystemeRepository _SystemeRepository;
       
        public SystemeController(IMapper mapper, ISystemeRepository systemeRepository)
        {

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _SystemeRepository = systemeRepository ??
                throw new ArgumentNullException(nameof(systemeRepository));
        }


        public IActionResult HelpCenter()
        {
            var Systemes = _SystemeRepository.GetSystemes();
            var SystemeModel = _mapper.Map<IList<SystemeEntityDto>>(Systemes);
            var systemeList = new SystemesList(SystemeModel.ToList());
            var systemViewModel = new SystemeViewModel
            {
                SystemeSelectListItems = systemeList.GetSystemesList()
            };

            return View(systemViewModel);
        }

        public JsonResult SystemDetail(int SystemId)
        {
            var Systeme = _SystemeRepository.GetSystemeById(SystemId);
            return Json(Systeme);
        }
    }
}