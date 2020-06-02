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
    [Route("api/todos")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        private static  RedmineManager _redmineClient;

       public string host = "http://10.10.10.105/redmine";
        public string apiKey = "a33a925bb2ac3c04c507178bc25286d60516f38a";

        //public String login = "Hassen";
        public ToDosController()
        {

            _redmineClient = new RedmineManager(host, apiKey/*,login*/);
           

            //_redmineClient.ImpersonateUser = login;
        }


        [HttpGet("issues")]
        public async Task<ActionResult<List<Issue>>> GetIssues()
        {

            /*string issueId = "<issue-id>";*/
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

            //parameter - fetch issues for a date range
            //parameters.Add(RedmineKeys.CREATED_ON, "><2012-03-01|2012-03-07");

            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);

            return Ok(response);

        }


        [HttpGet("returnUser")]
        public async Task<List<User>> returnUser()
        {
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var users = await _redmineClient.GetObjectsAsync<User>(parameters);
            return users;
        
        }

        [HttpGet("returnApiKey")]
        public String returnApiKey()
        {
            User _CurrentUser = _redmineClient.GetCurrentUser();
            return _CurrentUser.ApiKey;
        }



      



    }
}