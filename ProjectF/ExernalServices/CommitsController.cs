using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitLabApiClient;
using GitLabApiClient.Models.Commits.Responses;
using GitLabApiClient.Models.Projects.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.ENTITIES;

namespace ProjectF.ExernalServices
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommitsController : ControllerBase
    {
        private readonly GitLabClient _gitLabClient;
        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;


        public CommitsController(IUserRepository userRepository , IBadgeRepository badgeRepository )
        {
            _gitLabClient = new GitLabClient("http://192.168.1.16", "3WjDVGLxxbT6kx3fcF_3");
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            
        }
        [ActionName("LoadProjects")]
        public async Task<IList<Project>> LoadProjects()
        {
            return await _gitLabClient.Projects.GetAsync();
        }

        [ActionName("VerifyIdUser")]
        public async Task<int> VerifyIdUser(User userdb)
        {
            
            int idUserGitlab = _userRepository.GetIdUserGitlab(userdb);
            var users = await _gitLabClient.Users.GetAsync();
            var user = users.Where(u => u.Id == idUserGitlab).FirstOrDefault();
             if (user == null)
             {
                throw new Exception("user not found");
             }
               
            
            return idUserGitlab;
        }


        [ActionName("LoadProjectsperUser")]
        public async Task<IList<Project>> ListProjectsPerUser(User user)
        {
            int IdsUserGitlab = await VerifyIdUser(user);
            IList<Project> AllProjects = await LoadProjects();
            return AllProjects.Where(p => p.CreatorId == IdsUserGitlab).ToList();
        }

       
        [ActionName("LoadCommits")]
        public async Task<int> CountCommitsUser(int idBadge , int iduser)
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            var badge = _badgeRepository.GetBadgeById(idBadge);
            var user = _userRepository.GetUserById(iduser);
            var UserBadge = _userRepository.GetUserBadge(iduser, idBadge);
            var Userprojects = await ListProjectsPerUser(user);
            foreach (var project in Userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(project.Id, o => o.Since = UserBadge.LastUpdate );
                nbrCommit = nbrCommit + commits.Count();
           };
            UserBadge.UserProgression += nbrCommit;
            UserBadge.LastUpdate = DateTime.Today;
           var result = _userRepository.UpdateUserProgression(UserBadge);
            if (result == true)
            {
                return nbrCommit;
            }
            return 0;
           

        }




    }
}