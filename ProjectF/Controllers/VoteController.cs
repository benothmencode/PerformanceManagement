using Microsoft.AspNetCore.Mvc;
using ProjectF.Models;
using ProjectF.ViewModel;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
namespace ProjectF.Controllers
{
    public class VoteController : Controller
    {
       public static VoteViewModel vm ;
        public static IList<AwardViewModel> ListAm = new List<AwardViewModel>();
        public static IList<Vote> votes = new List<Vote>() { new Vote() { Id = 1, Title = "helpful", Quantity = 3 },
            new Vote() { Id = 2, Title = "Smart", Quantity = 1 } };

        public IActionResult Index()
        {
            vm = new VoteViewModel();
            vm.Users = UserController.users;
            vm.Votes = votes;
            return View(vm);
        }

        [HttpGet]
        public IActionResult Awards([FromQuery] int idVote , [FromQuery] int idUser )
        {
            
            ProjectF.Models.User user = UserController.users.FirstOrDefault(u => u.Id == idUser);
            Vote vote = votes.FirstOrDefault(v => v.Id == idVote);
            string voteT = vote.Title;
            if (user != null && vote != null)
            {
                vote.Quantity = vote.Quantity - 1;
                AwardViewModel Am = new AwardViewModel(user, voteT);
                
                ListAm.Add(Am);
                return View(ListAm);
            }

            return RedirectToAction("index");
            
        }

        //dynamic mymodel = new ExpandoObject();
        //mymodel = votes ;
        //    IList<Vote> Awardss = new List<Vote>();
        //IList<User> users = new List<User>();
        //var Votee = votes.FirstOrDefault(v => v.Id == idVote);
        //Awardss.Add(Votee);
        //    mymodel = Awardss;
        //    User user = UserController.users.FirstOrDefault(u => u.Id == idUserr);
        //users.Add(user);
        //    mymodel = users;
        
        
    }
}