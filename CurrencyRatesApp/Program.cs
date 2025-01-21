using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // checking if db schema exists
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Ensuring database schema is created...");
            context.Database.EnsureCreated();
            Console.WriteLine("Database schema created successfully.");
        }

        Console.WriteLine("Starting Quartz Scheduler...");

        // Create a scheduler instance 
        var schedulerFactory = new StdSchedulerFactory();
        var scheduler = await schedulerFactory.GetScheduler();

        // Start the scheduler
        await scheduler.Start();

        // Define the job and trigger in order to take rates every min
        var job = JobBuilder.Create<UpdateRatesJob>()
            .WithIdentity("updateRatesJob", "group1")
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("updateRatesTrigger", "group1")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(1) // every minute as asked
                .RepeatForever())
            .Build();

        // Schedule the job with the trigger
        await scheduler.ScheduleJob(job, trigger);

        Console.WriteLine("Quartz Scheduler started. Press [Enter] to exit...");
        Console.ReadLine();

        
        await scheduler.Shutdown();
        Console.WriteLine("Quartz Scheduler stopped.");
    }
}
