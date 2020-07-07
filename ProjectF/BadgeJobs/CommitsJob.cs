using Hangfire;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using ProjectF.ExernalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.BadgeJobs
{
    public class CommitsJob : CommitsController, IBadgeJob
    {
        private IBadgeRepository _BadgeRepository { get; set; }

        private readonly IUserBadgeRepository _UserbadgeRepository;

        public CommitsJob(IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository, IUserRepository userRepository) : base(userRepository, badgeRepository, userBadgeRepository)
        {
            _BadgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
        }

        public void execute()
        {
            var badge = _BadgeRepository.GetBadgeByJobId("CommitsJob");
            var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);
            if (UserBadge.Count != 0)
            {
                foreach (var Ub in UserBadge)
                {
                    if (Ub.State != "done")
                    {
                        RecurringJob.AddOrUpdate<CommitsJob>($"{Ub.BadgeId}-{Ub.UserId}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.StartedAt), "20 11 * * *", TimeZoneInfo.Local);
                    }
                }
            }
            Console.WriteLine("Hello");
        }
    }
}
