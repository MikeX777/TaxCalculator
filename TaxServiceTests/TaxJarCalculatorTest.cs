using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService.Calculators;
using TaxService.Models.Order;
using TaxServiceTests.Mocks;

namespace TaxServiceTests
{
    [TestClass]
    public class TaxJarCalculatorTest
    {
        [TestMethod]
        public async Task GetRateTest()
        {
            var httpMessageHandlerMock = new HttpMessageHandlerMock();
            var calc = new TaxJarCalculator(httpMessageHandlerMock, "testKey");

            var rate = await calc.GetRateAsync("90404");

            Assert.AreEqual(rate.state_rate, 0.0625);

        }

        [TestMethod]
        public async Task GetTaxOnOrderTest()
        {
            var httpMessageHandlerMock = new HttpMessageHandlerMock();
            var calc = new TaxJarCalculator(httpMessageHandlerMock, "testKey");
            var order = new Order()
            {
                from_country = "US",
                from_zip = "07001",
                from_state = "NJ",
                to_country = "US",
                to_zip = "07446",
                to_state = "NJ",
                amount = 16.50,
                shipping = 1.5,
                line_items = new List<LineItem>
                {
                    new LineItem()
                    {
                        quantity = 1,
                        unit_price = 15.0,
                        product_tax_code = "31000"
                    }
                }
            };

            var tax = await calc.GetTaxOnOrderAsync(order);
            Assert.AreEqual(tax.amount_to_collect, 1.09);
        }
    }
}
