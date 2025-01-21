using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting Quartz Scheduler...");

        // Create a scheduler instance
        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = await schedulerFactory.GetScheduler();

        // Start the scheduler
        await scheduler.Start();

        // Define the job and trigger
        var job = JobBuilder.Create<UpdateRatesJob>()
            .WithIdentity("updateRatesJob", "group1")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("updateRatesTrigger", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(1) // Run every minute
                .RepeatForever())
            .Build();

        // Schedule the job with the trigger
        await scheduler.ScheduleJob(job, trigger);

        Console.WriteLine("Quartz Scheduler started. Press [Enter] to exit...");
        Console.ReadLine();

        // Shut down the scheduler gracefully
        await scheduler.Shutdown();
        Console.WriteLine("Quartz Scheduler stopped.");
    }
}
