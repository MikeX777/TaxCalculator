using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models.Order;
using TaxService.Models.Rate;
using TaxService.Models.TaxOnOrder;

namespace TaxService.Calculators
{
    public class TaxJarCalculator : ITaxCalculator
    {
        private readonly HttpClient _httpClient;

        public TaxJarCalculator(HttpMessageHandler handler, string apiKey)
        {
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Token", $"token=\"{apiKey}\"");
        }

        public async Task<Rate> GetRateAsync(string zip, string country = null, string city = null, string street = null)
        {
            var queryBuilder = new StringBuilder("?");
            if (country != null)
            {
                queryBuilder.Append($"country={country}");
            }

            if (city != null)
            {
                if (queryBuilder.Length > 1) queryBuilder.Append("&");
                queryBuilder.Append($"city={city}");
            }

            if (street != null)
            {
                if (queryBuilder.Length > 1) queryBuilder.Append("&");
                queryBuilder.Append($"street={street}");
            }

            HttpResponseMessage responseMessage;

            if (queryBuilder.Length == 1)
            {
                responseMessage = await _httpClient.GetAsync($"https://api.taxjar.com/v2/rates/{zip}");
            }
            else
            {
                responseMessage = await _httpClient.GetAsync($"https://api.taxjar.com/v2/rates/{zip}{queryBuilder}");
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RateRootObject>(json).rate;
            }
            else
            {
                return new Rate();
            }
        }

        public async Task<Tax> GetTaxOnOrderAsync(Order order)
        {
            var responseMessage = await _httpClient.PostAsync("https://api.taxjar.com/v2/taxes",
                new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json"));

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TaxOnOrderRootObject>(json).tax;
            }
            else
            {
                return new Tax();
            }
        }
    }
}
