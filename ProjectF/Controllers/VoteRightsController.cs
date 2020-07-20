using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PerformanceManagement.DATA.DbContexts;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.Helpers;
using ProjectF.ModelsDTOS;
using ProjectF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.Controllers
{
    public class VoteRightsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IBadgeRepository _BadgeRepository;

        public VoteRightsController(IMapper mapper, IUserRepository userRepository, IVoteRepository voteRightsRepository , IBadgeRepository badgeRepository)
        {

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _voteRepository = voteRightsRepository ??
                throw new ArgumentNullException(nameof(voteRightsRepository));
            _BadgeRepository = badgeRepository ??
                throw new ArgumentNullException(nameof(badgeRepository));

        }


       
        
        public IActionResult VoteRights([FromQuery] int userId)
         {
            var users = _userRepository.GetUsers().Where(u => u.Id != userId);
           
            var  Votes = _voteRepository.GetUserVoteRights(userId).Where(vr => vr.BadgeDisabled == false);
           
            var votesmodel = _mapper.Map<IList<VoteRightsEntityDto>>(Votes);

            var Usersmodel = _mapper.Map<IList<UserEntityDto>>(users);

            var voteRightsviewmodel = new VoteRightsViewModel()
            {
                UsersDtos = Usersmodel,
                VoteRightsDtos = votesmodel
            };
            return View(voteRightsviewmodel);
        
          }

        [Authorize]
        public JsonResult VoteRegistration(int idUserChosen , int idVote , int UserId)
        {
            var vote = _voteRepository.GetVoteRights(idVote);

            if (vote.Quantity != 0)
            {
                _voteRepository.CreateVoteHistory(idUserChosen, vote.TypeVoteId, UserId);
                vote.Quantity -= 1;
                _voteRepository.AddOrUpdateVoteRights(vote.Id, vote);
                return Json(new
                 {
                     success = true,
                     responseText = _voteRepository.GetVoteHistory(UserId)
                 });
            }
            else 
            {
                return Json(new
                {
                    success = false,
                    responseText = "you Used All your VoteRights ! "
                });
            }
            
        }

      




    }
}