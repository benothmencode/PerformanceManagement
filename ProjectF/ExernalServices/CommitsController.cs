using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitLabApiClient;
using GitLabApiClient.Models;
using GitLabApiClient.Models.Commits.Responses;
using GitLabApiClient.Models.Projects.Responses;
using GitLabApiClient.Models.Users.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.ENTITIES;

namespace ProjectF.ExernalServices
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommitsController : ControllerBase , ICommitsController
    {
        private readonly GitLabClient _gitLabClient;
        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;


        public CommitsController(IUserRepository userRepository , IBadgeRepository badgeRepository )
        {
            _gitLabClient = new GitLabClient("http://10.10.10.104/", "-NVEYtWgMfhuKTjGDNzr");
            //_gitLabClient = new GitLabClient("http://192.168.1.16", "3WjDVGLxxbT6kx3fcF_3");
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            
        }
        [ActionName("LoadProjects")]
        public async Task<IList<Project>> LoadProjects()
        {
            return await _gitLabClient.Projects.GetAsync();
        }

        [ActionName("VerifyIdUser")]
        public async Task<int> VerifyIdUser(int userId)
        {
            
            int idUserGitlab = _userRepository.GetIdUserGitlab(userId);
            var users = await _gitLabClient.Users.GetAsync();
            var user = users.Where(u => u.Id == idUserGitlab).FirstOrDefault();
             if (user == null)
             {
                throw new Exception("user not found");
             }
               
            
            return idUserGitlab;
        }


        [ActionName("LoadProjectsperUser")]
        public async Task<IList<Project>> ListProjectsPerUser(int userId)
        {
            int IdsUserGitlab = await VerifyIdUser(userId);
            IList<Project> AllProjects = await LoadProjects();
            return AllProjects.Where(p => p.CreatorId == IdsUserGitlab).ToList();
        }

        public async Task<GitLabApiClient.Models.Users.Responses.User> ListCommiterName(int projectId , int UserId)
        {
            var users = await _gitLabClient.Projects.GetUsersAsync(projectId);
            var user = users.Where(u => u.Id == UserId).FirstOrDefault();
            return user;

        }


        [ActionName("LoadprojectsMemberofperUser")]
        public async Task<int> ListProjectsUserMemberof(int userId , int idBadge)
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            int IdsUserGitlab = await VerifyIdUser(userId);
            IList<Project> userprojects = await LoadProjects();
            var UserBadge = _userRepository.GetUserBadge(userId, idBadge);
            foreach (var p in userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(p.Id, c => c.Since = UserBadge.LastUpdate);
                var Committer = await ListCommiterName(p.Id , IdsUserGitlab);
                if (Committer != null)
                {
                    var UserCommits = commits.Where(c => c.CommitterName == Committer.Name).ToList();
                    nbrCommit += UserCommits.Count();
                }
                else
                {
                    nbrCommit += 0;
                }
            }
            if (UserBadge.UserProgression <= UserBadge.Badge.BadgeCriteria)
            {
                UserBadge.UserProgression += nbrCommit;
                UserBadge.LastUpdate = DateTime.Today;
                var result = _userRepository.UpdateUserProgression(UserBadge);
            }
            return nbrCommit;
        }

        [ActionName("CountCommits")]
        public async Task<int> CountC(int iduser)
        
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
           
            var userprojects = await ListProjectsPerUser(iduser);
            foreach (var project in userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(project.Id);
                nbrCommit += commits.Count();
            };
            return nbrCommit;
        }


        [ActionName("LoadCommits")]
        public async Task CountCommitsUser(int idBadge, int iduser)
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            var UserBadge = _userRepository.GetUserBadge(iduser, idBadge);
            var Userprojects = await ListProjectsPerUser(iduser);
            foreach (var project in Userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(project.Id , c => c.Since = UserBadge.LastUpdate);
                nbrCommit = nbrCommit + commits.Count();
            };
            UserBadge.UserProgression += nbrCommit;
            UserBadge.LastUpdate = DateTime.Today;
            var result = _userRepository.UpdateUserProgression(UserBadge);
        }



        [ActionName("FirstCommit")]
        public async Task FirstCommiter()
        {
            var Badge = _badgeRepository.GetBadgeByTitle("FirstCommiter");
            var userBadges = _badgeRepository.GetUsersBadge(Badge);
            foreach(var ub in userBadges)
            {
                await CountCommitsUser(ub.BadgeId, ub.UserId);
                if (ub.UserProgression >= 1 && ub.State != "Done")
                {
                    ub.ObtainedAt = DateTime.Today;
                    ub.State = "Done";
                    _userRepository.UpdateUserProgression(ub);
                }
            }

        } 




    }
}