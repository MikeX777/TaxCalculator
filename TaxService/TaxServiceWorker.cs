using System;
using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models.Rate;
using TaxService.Models.Order;
using TaxService.Models.TaxOnOrder;

namespace TaxService
{
    public class TaxServiceWorker
    {
        private readonly ITaxCalculator _taxCalculator;

        public TaxServiceWorker(ITaxCalculator taxCalculator)
        {
            _taxCalculator = taxCalculator;
        }

        public async Task<Rate> GetRate(string zip, string country = null, string city = null, string street = null)
        {
            return await _taxCalculator.GetRateAsync(zip, country, city, street);
        }

        public async Task<Tax> GetTaxOnOrder(Order order)
        {
            return await _taxCalculator.GetTaxOnOrderAsync(order);
        }
    }
}
