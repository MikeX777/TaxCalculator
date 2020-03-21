using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxCalculator.Models.Tax
{
    public class Tax
    {
        public double AmountToCollect { get; set; }
        public bool FreightTaxable { get; set; }
        public bool HasNexus { get; set; }
        public double OrderTotalAmount { get; set; }
        public double Rate { get; set; }
        public double Shipping { get; set; }
        public string TaxSource { get; set; }
        public double TaxableAmount { get; set; }
    }
}
