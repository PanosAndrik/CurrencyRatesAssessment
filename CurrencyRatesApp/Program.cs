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
            Console.WriteLine(rates);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching rates: {ex.Message}");
        }
    }
}
