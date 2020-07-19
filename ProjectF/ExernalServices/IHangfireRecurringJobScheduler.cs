namespace ProjectF.ExernalServices
{
    public interface IHangfireRecurringJobScheduler
    {
        void ScheduleUserbadgeTask();
        void EventEveryDay();
        public void ScheduleWinBadges();


    }
}