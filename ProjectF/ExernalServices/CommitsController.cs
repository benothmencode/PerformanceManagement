using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GitLabApiClient;
using GitLabApiClient.Models.Commits.Responses;
using GitLabApiClient.Models.Projects.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectF.ExernalServices
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommitsController : ControllerBase
    {
        private readonly GitLabClient _gitLabClient;


        public CommitsController()
        {
            _gitLabClient = new GitLabClient("http://192.168.1.15", "xV2FEmqsEf2pgxsFwznH");
        }
        [ActionName("LoadProjects")]
        public async Task<IList<Project>> LoadProjects()
        {
            return await _gitLabClient.Projects.GetAsync();
        }


        [ActionName("LoadProjectsperUser")]
        public async Task<IList<Project>> ListProjectsPerUser(int IdsUserGitlab)
        {
            IList<Project> AllProjects = await LoadProjects();
            return AllProjects.Where(p => p.CreatorId == IdsUserGitlab).ToList();
        }

        [ActionName("LoadCommits")]
        public async Task<int?> CountCommitsUser(int IdsUserGitlab)
        {
            DateTime? BadgeCreatedAt = new DateTime();
            int? nbrCommit = new int();
            IList<Commit> commits = new List<Commit>();
            var Userprojects = await ListProjectsPerUser(IdsUserGitlab);
            foreach (var project in Userprojects)
            {
                commits = await _gitLabClient.Commits.GetAsync(project.Id, o => o.Since = BadgeCreatedAt);
                nbrCommit = nbrCommit + commits.Count();
            };
            return nbrCommit;
        }

    }
}