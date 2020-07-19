using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using Redmine.Net.Api;
using Redmine.Net.Api.Async;
using Redmine.Net.Api.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using User = Redmine.Net.Api.Types.User;

namespace ProjectF.ExernalServices
{

    [Route("api/[controller]/[action]")]
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


  
    

        //[HttpGet("GetIssueStatus")]
        //public async Task<String> GetIssueStatus()
        //{
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

        //    var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);

        //    return response.Last().Status.Name;

        //}

        //[ActionName("getissues")]
        //public async Task<List<Issue>> GetIssues()
        //{
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

        //    var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);

        //    return response;

        //}

        //public async Task<int?> VerifyIdUser(int userId)
        //{

        //    int? idUserRedmine = 6;
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

        //    var users = await _redmineClient.GetObjectsAsync<User>(parameters);
        //    var user = users.Where(u => u.Id == idUserRedmine).FirstOrDefault();
        //    if (user == null)
        //    {
        //        throw new Exception("user not found");
        //    }


        //    return idUserRedmine;
        //}


        [ActionName("getissues")]
        public async Task<List<Issue>> GetIssuesperuser()
        {
            //exemple
            int id = 5;
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };

            var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);

            var listissuesPeruser = response.Where(x => x.Author.Id == id).ToList();

            return listissuesPeruser;

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

        //public async Task<int> progressionforuser()
        //{
        //    var listofidis = new List<int>();
        //    listofidis.Add(1);
        //    listofidis.Add(2);
        //   var l= listofusersidinredmine();
        //   var users = Getusers();
            
        //    var user = users.Where(u => u.Id == idUserGitlab).FirstOrDefault();


        //}

        


        //public async Task<List<int>> getissueidauthorAsync()
        //{
        //    var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
        //    var l =new List<int> ();
        //    var response = await _redmineClient.GetObjectsAsync<Issue>(parameters);
        //    foreach(var r in response)
        //    {
        //     var id=  r.Author.Id;
        //        l.Add(id);
        //    }

        //    return l;

        //}
       

        [HttpGet("TodosBadge")]
        public async Task TodosBadge()
        {

            var Badge = _badgeRepository.GetBadgeByTitle("the first featured");

            var userBadges = _UserbadgeRepository.GetUsersBadge(Badge);

            //int? IdsUserRedmine = await verifyidredmine(userId);
            //var status = await GetIssueStatus();

            float? progression = await IssueProgression();

            foreach (var ub in userBadges) { 
             

                if (ub.UserProgression <= ub.Badge.BadgeCriteria && ub.State != "Done" && DateTime.Now <= ub.BadgeDeadline)
                {

                    ub.UserProgression = (int)progression;
                    _UserbadgeRepository.UpdateUserbadge(ub);
                  
                };
               

            } 


        }


    





        [ActionName("returnUser")]
        public async Task<IActionResult> returnUser()
        {

            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var users = await _redmineClient.GetObjectsAsync<User>(parameters);
            dynamic Redmine = new JObject();
            Redmine.Users = new JArray() as dynamic;
            foreach (var user in users)
            {
                dynamic redmineUser = new JObject();
                redmineUser.username = user.FirstName;
                redmineUser.id = user.Id;
                Redmine.Users.Add(redmineUser);
                //Serialize 
                var Temp = JsonConvert.SerializeObject(users);
                System.IO.File.WriteAllText(@"C:\\Users\\PC HIMY\\source\\repos\\PerformanceManagement\\ProjectF\\ExernalServices\\RedmineUsers.json", Redmine.ToString());

                
            }
            return Ok(Redmine);
        }


        [ActionName("GetSystemesIds")]
        public List<User> getUserSystemesIds()
        {
            var MyList = JsonConvert.DeserializeObject<List<User>>(System.IO.File.ReadAllText("C:\\Users\\PC HIMY\\source\\repos\\PerformanceManagement\\ProjectF\\ExernalServices\\RedmineUsers.json"));
            return MyList;
        }


        [ActionName("listusersredmine")]
        public async Task<List<int>> listofusersidinredmine()
        {
            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var users = await _redmineClient.GetObjectsAsync<User>(parameters);
            var l = new List<int>();
            foreach (var user in users){
                var u = user.Id;
                l.Add(u);
               
            }
            return l.ToList();
            
        }

      




        public async Task<int?> verifyidredmine(int userId)
        {

            int? idUserredmine = _userRepository.GetIdUserRedmine(userId);

            var parameters = new NameValueCollection { { RedmineKeys.STATUS_ID, RedmineKeys.ALL } };
            var users = await _redmineClient.GetObjectsAsync<User>(parameters);
            var user = users.Where(u => u.Id == idUserredmine).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("user not found");
            }


            return idUserredmine;
        }






        //[HttpGet("returnApiKey")]
        //public String returnApiKey()
        //{
        //    User _CurrentUser = _redmineClient.GetCurrentUser();
        //    return _CurrentUser.ApiKey;
        //}







    }
}