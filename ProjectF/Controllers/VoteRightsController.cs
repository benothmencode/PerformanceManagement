using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IVoteRightsRepository _voteRightsRepository;
        private readonly PerformanceManagementDBContext _context;

        public VoteRightsController(IMapper mapper, IUserRepository userRepository, IVoteRightsRepository voteRightsRepository , PerformanceManagementDBContext context)
        {

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
            _voteRightsRepository = voteRightsRepository ??
                throw new ArgumentNullException(nameof(voteRightsRepository));
            _context = context;
        }

        //public static VoteViewModel vm ;
        // public static IList<AwardViewModel> ListAm = new List<AwardViewModel>();
        // public static IList<Vote> votes = new List<Vote>() { new Vote() { Id = 1, Title = "helpful", Quantity = 3 },
        //     new Vote() { Id = 2, Title = "Smart", Quantity = 1 } };

        [Route("votes/User/")]
        [HttpGet("{userId}")]
        public IActionResult VoteRights([FromQuery] int userId)
         {
            var users = _userRepository.GetUsers();
           
              var  Votes = _voteRightsRepository.GetUserVoteRights(userId);
           
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
            VoteHistory voteHistory = new VoteHistory()
            {
                UserOwnerId = UserId,
                UserChosenId = idUserChosen,
                VoteRightsId = idVote
            };
            voteHistory.UserOwner = _userRepository.GetUserById(UserId);
            voteHistory.UserChosen = _userRepository.GetUserById(idUserChosen);
            voteHistory.DateOfVote = DateTime.UtcNow.ToString("MM-dd-yyyy");
            _context.VoteHistories.Add(voteHistory);
            _context.SaveChanges();
            List<VoteHistory> Vlist = _context.VoteHistories.ToList();
            return Json(Vlist);
        }

        // [HttpGet]
        // public IActionResult Awards([FromQuery] int idVote , [FromQuery] int idUser )
        // {

        //     ProjectF.Models.User user = UserController.users.FirstOrDefault(u => u.Id == idUser);
        //     Vote vote = votes.FirstOrDefault(v => v.Id == idVote);
        //     string voteT = vote.Title;
        //     if (user != null && vote != null)
        //     {
        //         vote.Quantity = vote.Quantity - 1;
        //         AwardViewModel Am = new AwardViewModel(user, voteT);

        //         ListAm.Add(Am);
        //         return View(ListAm);
        //     }

        //     return RedirectToAction("index");

        // }



    }
}