using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;

namespace ProjectF.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public UsersController(IMapper mapper ,IUserRepository userRepository , IWebHostEnvironment webHostEnvironment )
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

        //private string UploadFile(UserEntityDto userEntity)
        //{
        //    string fileName = null;
        //    if (userEntity.Userimage != null)
        //    {
        //        string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "theme/dist/img");
        //        fileName = Guid.NewGuid().ToString() + "-" + userEntity.Userimage.FileName;
        //        string filePath = Path.Combine(uploadDir, fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            userEntity.Userimage.CopyTo(fileStream);
        //        }
        //    }
        //    return fileName;
        //}

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

       

    }
}
