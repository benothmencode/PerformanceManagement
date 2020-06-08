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

namespace ProjectF.ExernalServices
{
    [Route("api/todos")]
    [ApiController]
    public class ToDosController : ControllerBase, IToDosController
    {
        private static RedmineManager _redmineClient;
        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;
        public string host = "http://localhost/redmine/";
        public string apiKey = "11b9a54a46850052e067141f514a96830b882399";
        //public String login = "Hassen";


        public ToDosController(IUserRepository userRepository, IBadgeRepository badgeRepository)
        {

            _redmineClient = new RedmineManager(host, apiKey/*,login*/);
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;

            //_redmineClient.ImpersonateUser = login;
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

        [HttpGet("IssueStatus")]
        public async Task<IEnumerable<IdentifiableName>> IssueStatus()
        {
            var parameters = new NameValueCollection { /*{ RedmineKeys.STATUS_ID, RedmineKeys.ALL }*/ };

            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
            var rep2 = response.Select(T => T.Status);
            return rep2;
        }

        [HttpGet("IssueProgression")]

        public async Task<IEnumerable<float?>> IssueProgression()
        {
            var parameters = new NameValueCollection { /*{ RedmineKeys.STATUS_ID, RedmineKeys.ALL }*/ };

            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
            var rep2 = response.TakeWhile(T => T.DoneRatio >= 0).Select(T => T.DoneRatio);

            return rep2;


        }




        public async Task TodosBadge()
        {

            var Badge = _badgeRepository.GetBadgeByTitle("the first featured");

            var userBadges = _badgeRepository.GetUsersBadge(Badge);



            var status = IssueStatus().ToString();
            foreach (var ub in userBadges)
            {

                ub.UserProgression = IssueProgression();
                ub.ObtainedAt = DateTime.Today;
                ub.State = status;
                _userRepository.UpdateUserProgression(ub);

            }
            if (status == "Resolved")
            {

            }

        }

        //public async Task<Issue> GetIssuePerProject(int idProject )
        //{

        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
        //    var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
        //   var  reponse2 =   _redmineClient.GetObjects<Project>(parameters);

        //    foreach(var r in )
        //    {

        //        var re= response2.
        //    }






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


        //[HttpGet("returnUser")]
        //public  async Task<List<User>> returnUser()
        //{
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
        //    var users = await _redmineClient.GetObjectsAsync<GitLabApiClient.Models.Users.Responses.User>(parameters);
        //    return (users);

        //}

        //[HttpGet("returnApiKey")]
        //public String returnApiKey()
        //{
        //    User _CurrentUser = _redmineClient.GetCurrentUser();
        //    return _CurrentUser.ApiKey;
        //}







    }
}