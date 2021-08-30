using Quartz;
using Quartz.Impl;

namespace LibraryAccounting.Timer
{
    public class BookingDeleteShedule
    {
        readonly static public int Days = 3;
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<DeletingOverdueBookings>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("deleting all expired user bookings", "bookings")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(Days * 24)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
