using Hangfire;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using ProjectF.Controllers;
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
        private readonly IEventRepository _EventRepository;
        public HangfireRecurringJobScheduler(IEventRepository eventRepository,IBadgeRepository badgeRepository, IUserBadgeRepository userBadgeRepository, IUserRepository userRepository)
        {
            _BadgeRepository = badgeRepository;
            _UserbadgeRepository = userBadgeRepository;
            _UserRepository = userRepository;
            _EventRepository = eventRepository;
        
        }

        public void ScheduleEvent()
        {
            var events = _EventRepository.GetAll();
            foreach (var ev in events)
            {
                var date = DateTime.Today;
                if (ev.Date != date)
                {


                    RecurringJob.AddOrUpdate<EventController>("Today'sevent", gl => gl.Eventsperday(date), Cron.Minutely, TimeZoneInfo.Local);
                }
            }
        }

            public void ScheduleToDosbadgeTask()
        {
            var badge = _BadgeRepository.GetBadgeByTitle("The first Feature");
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
            var badge = _BadgeRepository.GetBadgeByTitle("Commits");
            var UserBadge = _UserbadgeRepository.GetUsersBadge(badge);
            foreach (var Ub in UserBadge)
            {
                if (Ub.State != "done")
                {
                    RecurringJob.AddOrUpdate<ICommitsController>($"{Ub.BadgeId}-{Ub.UserId}-{Ub.StartedAt}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.StartedAt), "55 16 * * *", TimeZoneInfo.Local);
                }
            }

            //var badge = _BadgeRepository.GetBadgeByTitle("Commits");
            //var Ub = _UserbadgeRepository.GetUsersBadge(badge).Where(ub => ub.UserId == 2).First();
            //if (Ub.State != "done")
            //{
            //    RecurringJob.AddOrUpdate<ICommitsController>($"2-2-{Ub.StartedAt}", gl => gl.nombreCommits(Ub.UserId, Ub.BadgeId, Ub.StartedAt), "40 15 * * *", TimeZoneInfo.Local);
            //}
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
                    if (badge.LastCreation == badge.Created)
                    {
                        _UserbadgeRepository.CreateUserBadge(user.Id, badge.Id);
                        badge.LastCreation = DateTime.Now;
                        _BadgeRepository.UpdateLastCreationDate((DateTime)badge.LastCreation, badge);
                    }
                    else
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

        }
        public void ScheduleUserbadgeTask()
        {
            RecurringJob.AddOrUpdate(() => CreationUserbadgeTask(), "0 0 1 * *", TimeZoneInfo.Local);
            //ScheduleCommitbadgeTask();
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