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

namespace ProjectF.ExernalServices
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private static  RedmineManager _redmineClient;

       public string host = "http://localhost:3000";
        public string apiKey = "11b9a54a46850052e067141f514a96830b882399";
        string login = "racha";
        IWebProxy webProxy = WebRequest.GetSystemWebProxy();
       
        public ToDosController()
        {

            _redmineClient = new RedmineManager(host, apiKey);
           
            //_redmineClient.ImpersonateUser = login;
        }

        public static async Task<List<Issue>> GetIssues()
        {

            /*string issueId = "<issue-id>";*/
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

            //parameter - fetch issues for a date range
            parameters.Add(RedmineKeys.CREATED_ON, "><2012-03-01|2012-03-07");

            return await _redmineClient.GetObjectsAsync<Issue>(parameters);

        }


        [ActionName("returnUser")]
        public User returnUser()
        { 
           return  _redmineClient.GetCurrentUser();
            //return _CurrentUser.ApiKey;
        }

        [HttpGet]
        [ActionName("essai")]
        public   IActionResult  essai()
        {
            String ch = "it works ";
            return Ok(ch);
        }

        [Fact]
        [HttpGet]
        [ActionName("Should_Get_CurrentUser")]
        public void Should_Get_CurrentUser()
        {
            var currentUser = _redmineClient.GetCurrentUser();
            Assert.NotNull(currentUser);
        }



    }
}