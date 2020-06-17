namespace ProjectF.ExernalServices
{
    public interface IHangfireRecurringJobScheduler
    {
        void ScheduleCommitbadgeTask();
        void ScheduleUserbadgeTask();
        void CreationUserbadgeTask();
    }
}