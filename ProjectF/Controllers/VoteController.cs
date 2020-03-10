using Microsoft.AspNetCore.Mvc;
using ProjectF.Models;
using System.Collections.Generic;

namespace ProjectF.Controllers
{
    public class VoteController : Controller
    {
        public IActionResult Index()
        {
            var votes = new List<Vote>();
            votes.Add(new Vote() { Id = 1, Title = "helpful", Quantity = 3 });
            votes.Add(new Vote() { Id = 1, Title = "Smart", Quantity = 1 });

            return View(votes);
        }



        //public User offerVote(int idVote)
        //{

        //}
    }
}