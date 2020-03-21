namespace TaxService.Models.Order
{
    public class LineItem
    {
        public int? id { get; set; }
        public int quantity { get; set; }
        public double unit_price { get; set; }
        public string product_tax_code { get; set; }
    }
}
