using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.ENTITIES;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
namespace ProjectF.Controllers
{
    public class VoteRightsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;

        public VoteRightsController(IMapper mapper, IUserRepository userRepository, IVoteRepository voteRightsRepository)
        {

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _voteRepository = voteRightsRepository ??
                throw new ArgumentNullException(nameof(voteRightsRepository));
          
        }


       
        
        public IActionResult VoteRights([FromQuery] int userId)
         {
            var users = _userRepository.GetUsers();
           
              var  Votes = _voteRepository.GetUserVoteRights(userId);
           
            var votesmodel = _mapper.Map<IList<VoteRightsEntityDto>>(Votes);

            var Usersmodel = _mapper.Map<IList<UserEntityDto>>(users);

            var voteRightsviewmodel = new VoteRightsViewModel()
            {
                UsersDtos = Usersmodel,
                VoteRightsDtos = votesmodel
            };
            return View(voteRightsviewmodel);
        
          }

        public JsonResult VoteRegistration(int idUserChosen , int idVote , int UserId)
        {
            _voteRepository.CreateVoteHistory(idUserChosen, idVote, UserId);
            return Json(_voteRepository.GetVoteHistory(UserId));
        }

        


    }
}