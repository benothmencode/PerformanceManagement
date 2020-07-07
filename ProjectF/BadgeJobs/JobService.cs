using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.BadgeJobs
{
    public interface IJobService
    {
        List<string> getJobIds();
        Dictionary<string, IBadgeJob> getJobs();
        void startAllJobs();
        void startJob(string jobId);
    }

    public class JobService : IJobService
    {
        private Dictionary<string, IBadgeJob> serviceRegistry;
        private IBadgeRepository _badgeRepository;

        public JobService(IUserRepository userRepository, IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository , IEventRepository eventRepository)
        {
            _badgeRepository = badgeRepository;

            serviceRegistry = new Dictionary<string, IBadgeJob>()
            {
                {
                  "CommitsJob", new CommitsJob(badgeRepository, userBadgeRepository, userRepository)
                },
                {

                  "TodosJob", new TodosJob(badgeRepository, userBadgeRepository, userRepository, eventRepository)
                }
            };
        }

        public Dictionary<string, IBadgeJob> getJobs()
        {
            return serviceRegistry;
        }

        public List<string> getJobIds()
        {
            return new List<string>(serviceRegistry.Keys);
        }

        public void startJob(string jobId)
        {
            serviceRegistry[jobId].execute();
        }

        public void startAllJobs()
        {
            var badges = _badgeRepository.GetAll();
            foreach (var badge in badges)
            {
                if (badge.jobId != null)
                    serviceRegistry[badge.jobId].execute();
            }
        }
    }
}
