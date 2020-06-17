using Hangfire;
using Microsoft.AspNetCore.Components;
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

      

        public void ScheduleToDosbadgeTask()
        {
            var badge = _BadgeRepository.GetBadgeByTitle("the first featured");
            var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);

            foreach (var Ub in UserBadge)
            {
                if (Ub.State != "done")
                {

                    RecurringJob.AddOrUpdate<ToDosController>("Progression", gl => gl.IssueProgression(), Cron.Minutely,TimeZoneInfo.Local);
                }
            }
        }



            public void ScheduleCommitbadgeTask()
        {
            //var badge = _BadgeRepository.GetBadgeByTitle("Commits");
            //var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);
            //foreach (var Ub in UserBadge)
            //{
            //    if (Ub.State != "done")
            //    {
            //        RecurringJob.AddOrUpdate<ICommitsController>($"{Ub.BadgeId}-{Ub.UserId}-{Ub.StartedAt}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.StartedAt), "44 16 * * *" , TimeZoneInfo.Local);
            //    }
            //}

            var badge = _BadgeRepository.GetBadgeByTitle("the first featured");
            var Ub = _UserbadgeRepository.GetUsersBadge(badge).Where(ub => ub.UserId == 3).First();
            if (Ub.State != "done")
            {
                RecurringJob.AddOrUpdate<ICommitsController>($"2-2-{Ub.StartedAt}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.StartedAt), "40 15 * * *", TimeZoneInfo.Local);

            }


            var Ubadge = _UserbadgeRepository.GetUsersBadge(badge).Where(Ubadge => Ubadge.UserId == 3).First();
            if (Ubadge.State != "done")
            {
                RecurringJob.AddOrUpdate<ICommitsController>($"3-2-{Ubadge.StartedAt}", gl => gl.nombreCommits(Ubadge.UserId, Ubadge.BadgeId, Ubadge.StartedAt), "29 22 * * *", TimeZoneInfo.Local);
            }

        }
    }
}
