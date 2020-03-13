using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;

namespace ProjectF.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UsersController(IMapper mapper ,IUserRepository userRepository)
        {

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository)); ;
        }

        // GET: Users
        public IActionResult Employees()
        {
            var users =  _userRepository.GetUsers();
            if(users.Count() <= 0)
            {
                ViewBag.Message = "there was a prob retrieving users";
            }
            var model = _mapper.Map<IList<UserEntityDto>>(users);
            return  View(model);
           
        }

        // GET: Users/Details/5
        public IActionResult Profile(int? idUser)
        {
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
            return View(model);
        }

       

    }
}
