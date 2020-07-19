using Hangfire;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using ProjectF.ExernalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectF.BadgeJobs
{
    public class TodosJob : ToDosController, IBadgeJob
    {
        private IBadgeRepository _BadgeRepository { get; set; }

        private readonly IUserBadgeRepository _UserbadgeRepository;

        public TodosJob(IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository, IUserRepository userRepository , IEventRepository eventRepository) : base( eventRepository, userRepository, badgeRepository, userBadgeRepository)
        {
            _BadgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
        }

        public void execute()
        {
        var badges = _BadgeRepository.GetBadgesByJobId("TodosJob");
            foreach (var badge in badges)
            {
                var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);

                foreach (var Ub in UserBadge)
                {
                    if (Ub.State != "done")
                    {

                        RecurringJob.AddOrUpdate<ToDosController>("Progression", gl => gl.IssueProgression(), "00 11 * * *", TimeZoneInfo.Local);
                        RecurringJob.AddOrUpdate<ToDosController>("todos", gl => gl.TodosBadge(), "00 11 * * *", TimeZoneInfo.Local);

                    }
                }
            }
    }
}

}
