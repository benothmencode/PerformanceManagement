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
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;

namespace ProjectF.ExernalServices
{
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    public class CommitsController : ICommitsController
    {
        private readonly GitLabClient _gitLabClient;
        private readonly IUserRepository _userRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IUserBadgeRepository _UserbadgeRepository;


        public CommitsController(IUserRepository userRepository , IBadgeRepository badgeRepository , IUserBadgeRepository userBadgeRepository )
        {
            //_gitLabClient = new GitLabClient("http://10.10.10.104/", "-NVEYtWgMfhuKTjGDNzr");
            _gitLabClient = new GitLabClient("http://192.168.1.108", "3WjDVGLxxbT6kx3fcF_3");
            _userRepository = userRepository;
            _badgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;


        }
        //[ActionName("LoadProjects")]
        public async Task<IList<Project>> LoadProjects()
        {
            return await _gitLabClient.Projects.GetAsync();
        }

        //[ActionName("VerifyIdUser")]
        public async Task<int?> VerifyIdUser(int userId)
        {
            
            int? idUserGitlab = _userRepository.GetIdUserGitlab(userId);
            var users = await _gitLabClient.Users.GetAsync();
            var user = users.Where(u => u.Id == idUserGitlab).FirstOrDefault();
             if (user == null)
             {
                throw new Exception("user not found");
             }
               
            
            return idUserGitlab;
        }


        //[ActionName("LoadProjectsperUser")]
        public async Task<IList<Project>> ListProjectsPerUser(int userId)
        {
            int? IdsUserGitlab = await VerifyIdUser(userId);
            IList<Project> AllProjects = await LoadProjects();
            return AllProjects.Where(p => p.CreatorId == IdsUserGitlab).ToList();
        }

        public async Task<GitLabApiClient.Models.Users.Responses.User> ListCommiterName(int projectId , int? UserId)
        {
            var users = await _gitLabClient.Projects.GetUsersAsync(projectId);
            var user = users.Where(u => u.Id == UserId).FirstOrDefault();
            return user;

        }


        //[ActionName("LoadprojectsMemberofperUser")]
        public async Task<int> nombreCommits(int userId , int idBadge , DateTime? update)
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            int? IdsUserGitlab = await VerifyIdUser(userId);
            IList<Project> userprojects = await LoadProjects();
            var UserBadge = _UserbadgeRepository.GetUserBadge(userId, idBadge);
            update = UserBadge.StartedAt;
            foreach (var p in userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(p.Id, c => c.Since = update);
                if (commits.Count() != 0)
                {
                    var Committer = await ListCommiterName(p.Id, IdsUserGitlab);
                    if (Committer != null)
                    {
                        var UserCommits = commits.Where(c => c.CommitterName == Committer.Name).ToList();/*Where(c => c.CommittedDate >= DateUserprog).*/
                        nbrCommit += UserCommits.Count();
                    }
                    else
                    {
                        nbrCommit += 0;
                    }
                }
            }
            if (UserBadge.UserProgression <= UserBadge.Badge.BadgeCriteria && UserBadge.State != "Done" && DateTime.Now <= UserBadge.BadgeDeadline)
            {
                var progression = new Progression()
                {
                    UserBadgeId = UserBadge.Id,
                    DateUserprog = DateTime.Now,
                    Userprogression = nbrCommit,
                    UserName = UserBadge.User.UserName
                };
                _UserbadgeRepository.UpdateProgression(progression);
                var prog = UserBadge.Progressions.ToList().LastOrDefault();
                UserBadge.UserProgression = prog.Userprogression;
                var result = _userRepository.UpdateUserProgression(UserBadge);
            }
            return nbrCommit;
        }

       

        //[ActionName("CountCommits")]
        public async Task<int> CountCommits(int userId, int idBadge, DateTime? update)
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            int? IdsUserGitlab = await VerifyIdUser(userId);
            IList<Project> userprojects = await LoadProjects();
            var UserBadge = _UserbadgeRepository.GetUserBadge(userId, idBadge);
            foreach (var p in userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(p.Id, c => c.Since = update);
                var Committer = await ListCommiterName(p.Id, IdsUserGitlab);
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
            return nbrCommit;
        }

        //[ActionName("FirstCommit")]
        public async Task<bool> FirstCommiter()
        {
            bool result = new bool();
            var Badge = _badgeRepository.GetBadgeByTitle("FirstCommiter");
            var userBadges = _UserbadgeRepository.GetUsersBadge(Badge);
            foreach(var ub in userBadges)
            {
                var nbcommit = await CountCommits(ub.UserId , ub.BadgeId , ub.StartedAt);
                if(nbcommit >= 1 && ub.State != "Done")
                {
                    ub.UserProgression = 1;
                    ub.State = "Done";
                    ub.ObtainedAt = DateTime.Now;
                    result = _userRepository.UpdateUserProgression(ub);
                }
            }
            return result;
        }



        //[ActionName("counter")]
        public async Task<int> nbreCommits(int userId, int idBadge, DateTime? update)
        {
            int nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            int? IdsUserGitlab = await VerifyIdUser(userId);
            IList<Project> userprojects = await LoadProjects();
            var UserBadge = _UserbadgeRepository.GetUserBadge(userId, idBadge);
            update = UserBadge.StartedAt;
            foreach (var p in userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(p.Id, c => c.Since = update);
                if (commits.Count() != 0)
                {
                    var Committer = await ListCommiterName(p.Id, IdsUserGitlab);
                    if (Committer != null)
                    {
                        var UserCommits = commits.Where(c => c.CommitterName == Committer.Name).ToList();/*Where(c => c.CommittedDate >= DateUserprog).*/
                        nbrCommit += UserCommits.Count();
                    }
                    else
                    {
                        nbrCommit += 0;
                    }
                }
            }
            if (UserBadge.UserProgression == 0 || UserBadge.UserProgression <= UserBadge.Badge.BadgeCriteria)
            {
                var progression = new Progression()
                {
                    UserBadgeId = UserBadge.Id,
                    DateUserprog = DateTime.Now,
                    Userprogression = nbrCommit,
                    UserName = UserBadge.User.UserName
                };
                _UserbadgeRepository.UpdateProgression(progression);
                var prog = UserBadge.Progressions.ToList().LastOrDefault();
                UserBadge.UserProgression = prog.Userprogression;
                var result = _userRepository.UpdateUserProgression(UserBadge);
            }
            return nbrCommit;
        }



        
    }
}