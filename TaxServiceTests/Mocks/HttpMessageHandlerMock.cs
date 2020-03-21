using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaxService.Models.Order;

namespace TaxServiceTests.Mocks
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsoluteUri == "https://api.taxjar.com/v2/rates/90404")
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
       
                response.Content = new StringContent("{\"rate\": {\"city\": \"SANTA MONICA\",\"city_rate\": \"0.0\",\"combined_district_rate\": \"0.03\",\"combined_rate\": \"0.1025\"," +
                    "\"country\": \"US\",\"country_rate\": \"0.0\",\"county\": \"LOS ANGELES\",\"county_rate\": \"0.01\",\"freight_taxable\": false,\"state\": \"CA\"," +
                    "\"state_rate\": \"0.0625\",\"zip\": \"90404\"}}", Encoding.UTF8, "application/json");
                return response;
            }

            if (request.RequestUri.AbsoluteUri == "https://api.taxjar.com/v2/taxes" && request.Method == HttpMethod.Post)
            {
                var json = await request.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<Order>(json);
                if (order.amount == 16.5 && order.shipping == 1.5)
                {
                    var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    response.Content = new StringContent("{\"tax\":{\"amount_to_collect\":1.09,\"breakdown\":{\"city_tax_collectable\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.06625,\"county_tax_collectable\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"line_items\":[{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.06625,\"county_amount\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"id\":\"1\",\"special_district_amount\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_amount\":0.99,\"state_sales_tax_rate\":0.06625,\"state_taxable_amount\":15.0,\"tax_collectable\":0.99,\"taxable_amount\":15.0}],\"shipping\":{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.06625,\"county_amount\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"special_district_amount\":0.0,\"special_tax_rate\":0.0,\"special_taxable_amount\":0.0,\"state_amount\":0.1,\"state_sales_tax_rate\":0.06625,\"state_taxable_amount\":1.5,\"tax_collectable\":0.1,\"taxable_amount\":1.5},\"special_district_tax_collectable\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_tax_collectable\":1.09,\"state_tax_rate\":0.06625,\"state_taxable_amount\":16.5,\"tax_collectable\":1.09,\"taxable_amount\":16.5},\"freight_taxable\":true,\"has_nexus\":true,\"jurisdictions\":{\"city\":\"RAMSEY\",\"country\":\"US\",\"county\":\"BERGEN\",\"state\":\"NJ\"},\"order_total_amount\":16.5,\"rate\":0.06625,\"shipping\":1.5,\"tax_source\":\"destination\",\"taxable_amount\":16.5}}",
                        Encoding.UTF8, "application/json");
                    return response;
                }
            }

            return new HttpResponseMessage();
        }
    }
}
