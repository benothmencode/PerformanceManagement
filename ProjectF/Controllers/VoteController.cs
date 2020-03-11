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
        public static IList<Vote> votes = new List<Vote>() { new Vote() { Id = 1, Title = "helpful", Quantity = 3 },
            new Vote() { Id = 2, Title = "Smart", Quantity = 1 } };

        public IActionResult Index(VoteViewModel vm)
        {
            vm.Users = UserController.users;
            vm.Votes = votes;
            return View(vm);
        }

        [HttpGet]
        public IActionResult Awards([FromQuery] int idVote , [FromQuery] int idUserr)
        {
            var user = UserController.users.FirstOrDefault(u => u.Id == idUserr);
            var vote = votes.FirstOrDefault(v => v.Id == idVote);
            vote.user = user;
            return View("index",vote);
           
            
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