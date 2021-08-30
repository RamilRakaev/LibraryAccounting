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

            ITrigger trigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("deleting all expired user bookings", "bookings")     // идентифицируем триггер с именем и группой
                .StartNow()                            // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x            // настраиваем выполнение действия
                    .WithIntervalInHours(Days * 24)          // через 1 минуту
                    .RepeatForever())                   // бесконечное повторение
                .Build();                               // создаем триггер

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
