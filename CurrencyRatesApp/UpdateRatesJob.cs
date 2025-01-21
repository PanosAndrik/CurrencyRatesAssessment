using Quartz;
using CurrencyRatesGateway.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

public class UpdateRatesJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Executing UpdateRatesJob...");

        try
        {
            // currency rates ECB
            var gateway = new EcbGateway(new System.Net.Http.HttpClient());
            var rates = await gateway.FetchCurrencyRatesAsync();

            using (var dbContext = new AppDbContext())
            {
                // Ensure the database and table are created
                Console.WriteLine("Ensuring database and table exist...");
                dbContext.Database.EnsureCreated();

                Console.WriteLine("Updating currency rates in the database...");

                foreach (var rate in rates)
                {
                
                    var sql = @"
                        INSERT INTO CurrencyRates (Currency, Rate)
                        VALUES (@Currency, @Rate)
                        ON CONFLICT(Currency)
                        DO UPDATE SET Rate = excluded.Rate;
                    ";

                    dbContext.Database.ExecuteSqlRaw(sql, new[]
                    {
                        new SqliteParameter("@Currency", rate.Key),
                        new SqliteParameter("@Rate", rate.Value)
                    });
                }

                Console.WriteLine("Currency rates updated successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateRatesJob: {ex.Message}");
        }
    }
}
