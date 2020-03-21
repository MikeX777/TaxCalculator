namespace TaxCalculator.Models.Order
{
    public class LineItem
    {
        public int? ID { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string ProductTaxCode { get; set; }
    }
}
