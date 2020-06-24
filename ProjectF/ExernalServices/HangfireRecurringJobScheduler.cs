using Hangfire;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
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
        private readonly IUserRepository _UserRepository;

        public HangfireRecurringJobScheduler(IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository, IUserRepository userRepository)
        {
            _BadgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
            _UserRepository = userRepository;
        }



        public void ScheduleToDosbadgeTask()
        {
            var badge = _BadgeRepository.GetBadgeByTitle("the first featured");
            var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);

            foreach (var Ub in UserBadge)
            {
                if (Ub.State != "done")
                {

                    RecurringJob.AddOrUpdate<ToDosController>("Progression", gl => gl.IssueProgression(), Cron.Minutely, TimeZoneInfo.Local);
                }
            }
        }



        public void ScheduleCommitbadgeTask()
        {
            var badge = _BadgeRepository.GetBadgeByTitle("Commit");
            var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);
            if (UserBadge.Count != 0)
            {
                foreach (var Ub in UserBadge)
                {
                    if (Ub.State != "done")
                    {
                        RecurringJob.AddOrUpdate<ICommitsController>($"{Ub.BadgeId}-{Ub.UserId}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.StartedAt), "55 16 * * *", TimeZoneInfo.Local);
                    }
                }
            }
           
        }




        // execute evrer frst day of month
        public void CreationUserbadgeTask()
        {
            var badges = _BadgeRepository.GetAll();
            var users = _UserRepository.GetUsers();

            foreach (var badge in badges)
            {
                foreach (var user in users)
                {
                        var LastCreationDate = (DateTime)badge.LastCreation;
                        if (LastCreationDate != null)
                        {
                            Periodicity weekly = Periodicity.Weekly;
                            Periodicity Monthly = Periodicity.Monthly;
                            Periodicity Yeary = Periodicity.Yearly;

                            if (badge.periodicity.Equals(weekly))
                            {
                                LastCreationDate = LastCreationDate.AddDays(7 * badge.ValueOfPeriodicity);
                            }
                            else if (badge.periodicity.Equals(Monthly))
                            {
                                LastCreationDate = LastCreationDate.AddMonths(badge.ValueOfPeriodicity);
                            }
                            else if (badge.periodicity.Equals(Yeary))
                            {
                                LastCreationDate = LastCreationDate.AddYears(badge.ValueOfPeriodicity);
                            }
                            if (DateTime.Now >= LastCreationDate)
                            {
                                _UserbadgeRepository.CreateUserBadge(user.Id, badge.Id);
                                LastCreationDate = DateTime.Now;
                                _BadgeRepository.UpdateLastCreationDate(LastCreationDate, badge);
                            }
                        }
                    }
            }

        }
        public void ScheduleUserbadgeTask()
        {

            RecurringJob.AddOrUpdate(() => CreationUserbadgeTask(), "0 0 1 * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate("Test",() => CreationUserbadgeTask(), "35 13 * * *", TimeZoneInfo.Local);
        }


       
        //{
        //    //Get badge periodicity
        //    //Get badge last creation date
        //    // if current date > = last creation date + periodicity
        //    // create batch for all users and update badge las creation date
        //    foreach (var user in users)
        //    {
        //        //RecurringJob.AddOrUpdate<IUserBadgeRepository>($"{user.Id}-{badge.Id}", ub => ub.CreateUserBadge(user.Id, badge.Id), " ", TimeZoneInfo.Local);

        //    }










    }
}