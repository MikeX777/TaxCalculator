using DeepEqual.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxService;
using TaxServiceTests.Mocks;
using TaxService.Models.Order;

namespace TaxServiceTests
{
    [TestClass]
    public class TaxServiceTests
    {
        // I understand these tests might be silly, but from what I was able to gather from the email, there should be a differentiation 
        // between the Service and the calcultor. However, the methods required on the service seem to already be solved by the 
        // TaxJar calculator, so I ended up just making the service essentially a passthrough. Which when working with multiple calculators may
        // be a helpful feature. 

        [TestMethod]
        public async Task GetRateAsyncTest()
        {
            var taxCalculatorMock = new TaxCalculatorMock();
            var taxService = new TaxServiceWorker(taxCalculatorMock);

            var serviceRate = await taxService.GetRate("90404", "US", "Santa Monica");
            var calculatorRate = await taxCalculatorMock.GetRateAsync("90404", "US", "Santa Monica");

            Assert.IsTrue(serviceRate.IsDeepEqual(calculatorRate));
        }

        [TestMethod]
        public async Task GetTaxOnOrderTest()
        {
            var taxCalculatorMock = new TaxCalculatorMock();
            var taxService = new TaxServiceWorker(taxCalculatorMock);

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

            var serviceTax = await taxService.GetTaxOnOrder(order);
            var calculatorTax = await taxCalculatorMock.GetTaxOnOrderAsync(order);

            Assert.IsTrue(serviceTax.IsDeepEqual(calculatorTax));
        }
    }
}
