namespace TaxService.Models.TaxOnOrder
{
    public class Tax
    {
        public double amount_to_collect { get; set; }
        public Breakdown breakdown { get; set; }
        public bool freight_taxable { get; set; }
        public bool has_nexus { get; set; }
        public Jurisdictions jurisdictions { get; set; }
        public double order_total_amount { get; set; }
        public double rate { get; set; }
        public double shipping { get; set; }
        public string tax_source { get; set; }
        public double taxable_amount { get; set; }
    }
}
