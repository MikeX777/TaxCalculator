using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxService.Interfaces;
using TaxService.Models.Order;
using TaxService.Models.Rate;
using TaxService.Models.TaxOnOrder;

namespace TaxServiceTests.Mocks
{
    public class TaxCalculatorMock : ITaxCalculator
    {
        public async Task<Rate> GetRateAsync(string zip, string country = null, string city = null, string street = null)
        {
            if (zip == "90404" && country == "US" && city == "Santa Monica" && street == null)
            {
                return new Rate()
                {
                    city = "SANTA MONICA",
                    city_rate = 0.0,
                    combined_district_rate = 0.03,
                    combined_rate = 0.1025,
                    country = "US",
                    country_rate = 0.0,
                    county = "LOS ANGELES",
                    county_rate = 0.01,
                    freight_taxable = false,
                    state = "CA",
                    state_rate = 0.0625,
                    zip = "90404"
                };
            }

            return new Rate();
        }

        public async Task<Tax> GetTaxOnOrderAsync(Order order)
        {
            var mockedOrder = new Order()
            {
                from_country = "US",
                from_zip = "07001",
                from_state = "NJ",
                to_country = "US",
                to_zip = "07446",
                to_state = "NJ",
                amount = 16.50,
                shipping = 1.5,
                line_items = new List<TaxService.Models.Order.LineItem>
                {
                    new TaxService.Models.Order.LineItem()
                    {
                        quantity = 1,
                        unit_price = 15.0,
                        product_tax_code = "31000"
                    }
                }
            };

            if (mockedOrder.from_country == order.from_country &&
                mockedOrder.from_zip == order.from_zip &&
                mockedOrder.from_state == order.from_state &&
                mockedOrder.to_country == order.to_country &&
                mockedOrder.to_zip == order.to_zip &&
                mockedOrder.to_state == order.to_state &&
                mockedOrder.amount == order.amount &&
                mockedOrder.shipping == order.shipping &&
                mockedOrder.line_items.Count == order.line_items.Count &&
                mockedOrder.line_items[0].quantity == order.line_items[0].quantity &&
                mockedOrder.line_items[0].unit_price == order.line_items[0].unit_price &&
                mockedOrder.line_items[0].product_tax_code == order.line_items[0].product_tax_code)
            {
                return new Tax
                {
                    amount_to_collect = 25.69
                };
            }

            return new Tax();
        }
    }
}
