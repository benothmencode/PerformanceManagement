using Hangfire;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ExernalServices
{
    public class HangfireRecurringJobScheduler : IHangfireRecurringJobScheduler
    {
        private IBadgeRepository _BadgeRepository { get; set; }

        private readonly IUserBadgeRepository _UserbadgeRepository;

        public HangfireRecurringJobScheduler(IBadgeRepository badgeRepository , IUserBadgeRepository userBadgeRepository)
        {
            _BadgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
        }

        public HangfireRecurringJobScheduler()
        {
        }

        public void ScheduleCommitbadgeTask()
        {
            var badge = _BadgeRepository.GetBadgeByTitle("Commits");
            var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);
            foreach (var Ub in UserBadge)
            {
                if (Ub.State != "done")
                {
                    RecurringJob.AddOrUpdate<ICommitsController>($"{Ub.BadgeId}-{Ub.UserId}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.LastUpdate), Cron.Daily);
                }
            }
        }
    }
}
