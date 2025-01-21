using CurrencyRatesGateway.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var httpClient = new HttpClient();
        var gateway = new EcbGateway(httpClient);

        try
        {
            // Fetch currency rates
            var rates = await gateway.FetchCurrencyRatesAsync();

            // Save to the database
            using (var context = new AppDbContext())
            {
                Console.WriteLine("Ensuring database is created...");
                context.Database.EnsureCreated();
                Console.WriteLine("Database created.");

                Console.WriteLine("Adding currency rates...");
                foreach (var rate in rates)
                {
                    Console.WriteLine($"Adding: {rate.Key} - {rate.Value}");
                    context.CurrencyRates.Add(new CurrencyRate
                    {
                        Currency = rate.Key,
                        Rate = rate.Value
                    });
                }

                context.SaveChanges();
                Console.WriteLine("Currency rates saved.");
            }

            Console.WriteLine("Process completed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
