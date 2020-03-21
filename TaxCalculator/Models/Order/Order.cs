using System;
using System.Collections.Generic;

namespace TaxCalculator.Models.Order
{
    public class Order
    {
        public string ToStreet { get; set; }
        public string ToCity { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }
        public string ToCountry { get; set; }
        public string FromStreet { get; set; }
        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromZip { get; set; }
        public string FromCountry { get; set; }
        public double Amount { get; set; }
        public double Shipping { get; set; }
        public List<LineItem> LineItems { get; set; }
        public List<NexusAddress> NexusAddresses { get; set; }
    }
}
