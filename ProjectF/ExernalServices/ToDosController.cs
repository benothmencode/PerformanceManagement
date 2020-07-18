using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redmine.Net.Api.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redmine.Net.Api;
using Xunit;
using System.Net;
using System.Collections.Specialized;
using Redmine.Net.Api.Async;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.ENTITIES;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using Type = PerformanceManagement.ENTITIES.Type;
using User = Redmine.Net.Api.Types.User;

namespace ProjectF.ExernalServices
{
    
    [Route("api/todos")]
    [ApiController]

    public class ToDosController : ControllerBase, IToDosController

    {
        private static RedmineManager _redmineClient;
        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IUserBadgeRepository _UserbadgeRepository;
        private readonly IEventRepository _eventRepository;
        public string host = "http://localhost/redmine/"; // Si vous voulez utiliser le serveur de la société il faut changer le host et le apikey de l admin
        public string apiKey = "01f1b7962f14cfd0475d03b136efc32e14ef159e";
        public String login = "Racha";





        public ToDosController(IEventRepository eventRepository,IUserRepository userRepository, IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository)
        {

            _redmineClient = new RedmineManager(host,apiKey);
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
            _eventRepository = eventRepository;
            _redmineClient.ImpersonateUser = login;
        }



        [HttpGet("GetProjects")]
        public async Task<List<Project>> GetPorjects()
        {


            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var response = await _redmineClient.GetObjectsAsync<Project>(parameters);

            return (response);
        }


        [HttpGet("GetTimeEntries")]
        public List<Issue> GetTimeEntries()
        {

            var parameters = new NameValueCollection { /*{ RedmineKeys.STATUS_ID, RedmineKeys.ALL }*/ };
            var response = _redmineClient.GetObjects<Issue>(parameters);
            //var issue = new Issue.SpentHours;

            return (response);

        }

        //[HttpGet("IssueStatus")]
        //public async Task<IEnumerable<IdentifiableName>> IssueStatus()
        //{
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

        //    var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
        //    var rep = response.Select(T => T.Status);

        //    return rep;
        //}

        [HttpGet("GetIssueStatus")]
        public async Task<String> GetIssueStatus()
        {
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);

            return response.Last().Status.Name;

        }
        [HttpGet("IssueProgression")]

        public async Task<float?>  IssueProgression()
        {
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL }};
            
            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
            var rep3 = response.Select(T => T.DoneRatio);
            float? rep1 = null;
            foreach (var prog in rep3)
            {
                rep1 = prog;
                
            }

            return rep1;

        }
        [HttpGet("dayevent")]
        public async Task dayevent()
        {

            var Badge = _badgeRepository.GetBadgeByTitle("the first featured");

            var userBadges = _UserbadgeRepository.GetUsersBadge(Badge);
            foreach (var ub in userBadges)
            {
                if (ub.UserProgression == 10)
                {
                    var evente = _eventRepository.GetAll().Where(e => e.Date == DateTime.Today).FirstOrDefault();
                    if (evente != null)
                    {
                        DayEvent ListEvent = new DayEvent()
                        {
                            Action = "Progression",
                            Date = DateTime.Today,
                            Description = ub.User.UserName + " Started a new bagde which is " + ub.Badge.Title,
                            UserId = ub.UserId,
                            EventId = evente.Id,
                           Type= Type.Badge
                         
                        };
                        var result = _eventRepository.CreateDayEvent(ListEvent);
                        

                    }
                }
            }
        }


        [HttpGet("TodosBadge")]
        public async Task TodosBadge()
        {

            var Badge = _badgeRepository.GetBadgeByTitle("the first featured");

            var userBadges = _UserbadgeRepository.GetUsersBadge(Badge);


            var status = await GetIssueStatus();

            float? progression = await IssueProgression();

            foreach (var ub in userBadges) { 
             

                if (ub.UserProgression <= ub.Badge.BadgeCriteria && ub.State != "Done" && DateTime.Now <= ub.BadgeDeadline)
                {

                    ub.UserProgression = (int)progression;
                    ub.State = status;
                    _UserbadgeRepository.UpdateUserbadge(ub);
                  
                };
               

            } 


        }


        [HttpGet("GetIssuePerProject")]
        public async Task<IdentifiableName> GetIssuePerProject()
        {

            
            IdentifiableName parentname =null;
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
            
            foreach (var rep in response)
            {
              parentname=  rep.Project;


            }

            return parentname;
            
            //foreach(var r in )
            //{

            //   var re= response2.
            //}
        }





        //[HttpGet("issues")]
        //public async Task<ActionResult<List<Issue>>> GetIssues()
        //{

        //    /*string issueId = "<issue-id>";*/
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

        //    //parameter - fetch issues for a date range
        //    //parameters.Add(RedmineKeys.CREATED_ON, "><2012-03-01|2012-03-07");

        //    var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);

        //    return Ok(response);

        //}


        [HttpGet("returnUser")]
        public  async Task<List<User>> returnUser()
        {
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var users = await _redmineClient.GetObjectsAsync<User>(parameters);
            return (users);

        }

        //[HttpGet("returnApiKey")]
        //public String returnApiKey()
        //{
        //    User _CurrentUser = _redmineClient.GetCurrentUser();
        //    return _CurrentUser.ApiKey;
        //}







    }
}