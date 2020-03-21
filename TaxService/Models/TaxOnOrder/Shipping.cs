using System;
using System.Collections.Generic;
using System.Text;

namespace TaxService.Models.TaxOnOrder
{
    public class Shipping
    {
        public double city_amount { get; set; }
        public double city_tax_rate { get; set; }
        public double city_taxable_amount { get; set; }
        public double combined_tax_rate { get; set; }
        public double county_amount { get; set; }
        public double county_tax_rate { get; set; }
        public double county_taxable_amount { get; set; }
        public double special_district_amount { get; set; }
        public double special_tax_rate { get; set; }
        public double special_taxable_amount { get; set; }
        public double state_amount { get; set; }
        public double state_sales_tax_rate { get; set; }
        public double state_taxable_amount { get; set; }
        public double tax_collectable { get; set; }
        public double taxable_amount { get; set; }
    }
}
