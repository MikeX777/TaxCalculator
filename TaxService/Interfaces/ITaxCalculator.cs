using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxService.Models.Rate;
using TaxService.Models.Order;
using TaxService.Models.TaxOnOrder;

namespace TaxService.Interfaces
{
    public interface ITaxCalculator
    {
        public Task<Rate> GetRateAsync(string zip, string country = null, string city = null, string street = null);
        public Task<Tax> GetTaxOnOrderAsync(Order order);
    }
}
