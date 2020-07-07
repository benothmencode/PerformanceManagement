using Hangfire;
using PerformanceManagement.DATA.Repositories;
using PerformanceManagement.DATA.Repositories.BadgeRepository;
using PerformanceManagement.DATA.Repositories.EventsRepository;
using PerformanceManagement.DATA.Repositories.UserBadgeRepository;
using PerformanceManagement.ENTITIES;
using System;
using System.Linq;

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






        // execute every Sunday At 00:00 ”
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

        public void ScheduleWinBadges()
        {
            RecurringJob.AddOrUpdate<IUserBadgeRepository>("Win", win => win.WinBadgeJob(), "0 0 * * 0", TimeZoneInfo.Local);
        }

        public void ScheduleUserbadgeTask()
        {

            RecurringJob.AddOrUpdate("Test", () => CreationUserbadgeTask(), "0 0 * * 0", TimeZoneInfo.Local);
        }

        public void EventEveryDay()
        {
            RecurringJob.AddOrUpdate<IEventRepository>("event", e => e.createeventeveryday(), "0 8 * * *", TimeZoneInfo.Local);
        }
       
        








    }
}