using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Models.Order;
using TaxCalculator.Models.Rate;
using TaxCalculator.Models.Tax;
using TaxService;

namespace TaxCalculator.Controllers
{
    [Route("[controller]/[action]")]
    public class TaxJarController : Controller
    {
        private readonly TaxServiceWorker _taxService;

        public TaxJarController(TaxServiceWorker taxService)
        {
            _taxService = taxService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRate()
        {

            // This most definetely is not the best way to make a form. I eneded up using this way because I thought this challange was more on the
            // Tax service itself. Had it been more on the client I would have focused on trying to use a Javascript library to make it work a little nicer.
            string country = null;
            if (Request.Query["Country"] != "") country = Request.Query["Country"];

            string city = null;
            if (Request.Query["City"] != "") city = Request.Query["City"];

            string street = null;
            if (Request.Query["Street"] != "") street = Request.Query["Street"];

            var serviceRate = await _taxService.GetRate(Request.Query["Zip"], country, city, street);

            // Instead of doing something like this I would most probably implement Automapper
            var rate =  new Rate()
            {
                City = serviceRate.city,
                CityRate = serviceRate.city_rate,
                CombinedDistrictRate = serviceRate.combined_district_rate,
                CombinedRate = serviceRate.combined_rate,
                Country = serviceRate.country,
                CountryRate = serviceRate.country_rate,
                County = serviceRate.county,
                CountyRate = serviceRate.county_rate,
                FreightTaxable = serviceRate.freight_taxable,
                State = serviceRate.state,
                StateRate = serviceRate.state_rate,
                Zip = serviceRate.zip
            };

            return View("Rate", rate);
        }

        [HttpPost]
        public async Task<Tax> TaxOnOrder([FromBody]Order order)
        {
            var serviceOrder = new TaxService.Models.Order.Order()
            {
                to_city = order.ToCity,
                to_country = order.ToCountry,
                to_state = order.ToState,
                to_street = order.ToStreet,
                to_zip = order.ToZip,
                from_city = order.FromCity,
                from_country = order.FromCountry,
                from_state = order.FromState,
                from_street = order.FromStreet,
                from_zip = order.FromZip,
                amount = order.Amount,
                shipping = order.Shipping,
                line_items = order.LineItems.Select(line => new TaxService.Models.Order.LineItem
                {
                    id = line.ID,
                    quantity = line.Quantity,
                    unit_price = line.UnitPrice,
                    product_tax_code = line.ProductTaxCode
                }).ToList(),
                nexus_addresses = order.NexusAddresses == null ? null : order.NexusAddresses.Select(address => new TaxService.Models.Order.NexusAddress
                {
                    country = address.Country,
                    state = address.State,
                    zip = address.Zip
                }).ToList()
            };

            var serviceTax = await _taxService.GetTaxOnOrder(serviceOrder);

            var tax = new Tax
            {
                AmountToCollect = serviceTax.amount_to_collect,
                FreightTaxable = serviceTax.freight_taxable,
                HasNexus = serviceTax.has_nexus,
                OrderTotalAmount = serviceTax.order_total_amount,
                Rate = serviceTax.rate,
                Shipping = serviceTax.shipping,
                TaxSource = serviceTax.tax_source,
                TaxableAmount = serviceTax.taxable_amount
            };

            return tax;
        }
    }
}
