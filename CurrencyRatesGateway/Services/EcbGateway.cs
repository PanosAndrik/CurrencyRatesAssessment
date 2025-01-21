using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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

        public async Task<Dictionary<string, decimal>> FetchCurrencyRatesAsync()
        {
            var response = await _httpClient.GetStringAsync(EcbUrl);
            var xml = XDocument.Parse(response);

            return xml.Descendants()
                .Where(node => node.Name.LocalName == "Cube" 
                    && node.Attribute("currency") != null 
                    && node.Attribute("rate") != null)
                .ToDictionary(
                    node => node.Attribute("currency")!.Value, // Add `!` to indicate non-null
                    node => decimal.Parse(node.Attribute("rate")!.Value, CultureInfo.InvariantCulture)
                );
        }
    }
}
