using Quartz;
using System;
using System.Threading.Tasks;

public class UpdateRatesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Executing UpdateRatesJob...");

        try
        {
            // Simulate fetching data and updating the database
            using (var dbContext = new AppDbContext())
            {
                Console.WriteLine("Fetching and updating currency rates...");
                // Add logic to fetch data from the ECB and update the database
            }

            Console.WriteLine("UpdateRatesJob completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateRatesJob: {ex.Message}");
        }
    }
}
