using Hangfire;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.ExernalServices
{
    public class HangfireRecurringJobScheduler : IHangfireRecurringJobScheduler
    {
        private IBadgeRepository _BadgeRepository { get; set; }

        public HangfireRecurringJobScheduler(IBadgeRepository badgeRepository)
        {
            _BadgeRepository = badgeRepository;
        }

        public HangfireRecurringJobScheduler()
        {
        }

        public void ScheduleCommitbadgeTask()
        {

            var badge = _BadgeRepository.GetBadgeByTitle("Commits");
            var UserBadge = _BadgeRepository.GetUsersBadge(badge);
            foreach (var Ub in UserBadge)
            {
                if (Ub.State != "done")
                {
                    RecurringJob.AddOrUpdate<ICommitsController>($"{Ub.BadgeId}-{Ub.UserId}", gl => gl.ListProjectsUserMemberof(Ub.UserId , Ub.BadgeId), Cron.Daily);
                }
            }
        }
    }
}
