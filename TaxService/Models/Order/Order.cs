using System.Collections.Generic;

namespace TaxService.Models.Order
{
    public class Order
    {
        public string to_street { get; set; }
        public string to_city { get; set; }
        public string to_state { get; set; }
        public string to_zip { get; set; }
        public string to_country { get; set; }
        public string from_street { get; set; }
        public string from_city { get; set; }
        public string from_state { get; set; }
        public string from_zip { get; set; }
        public string from_country { get; set; }
        public double amount { get; set; }
        public double shipping { get; set; }
        public List<LineItem> line_items { get; set; }
        public List<NexusAddress> nexus_addresses { get; set; }
    }
}
