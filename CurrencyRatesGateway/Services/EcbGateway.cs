using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurrencyRatesGateway.Services
{
    public class EcbGateway
    {
        private readonly HttpClient _httpClient;
        private const string EcbUrl = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";

        public EcbGateway(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<XDocument> FetchCurrencyRatesAsync()
        {
            var response = await _httpClient.GetStringAsync(EcbUrl);
            return XDocument.Parse(response);
        }
    }
}
