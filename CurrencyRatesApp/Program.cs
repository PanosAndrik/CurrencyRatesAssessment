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
            var rates = await gateway.FetchCurrencyRatesAsync();
            Console.WriteLine("Currency Rates:");

            // Iterate through the dictionary and display each currency and its rate
            foreach (var rate in rates)
            {
                Console.WriteLine($"{rate.Key}: {rate.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching rates: {ex.Message}");
        }
    }
}
