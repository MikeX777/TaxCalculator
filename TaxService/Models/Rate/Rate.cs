namespace TaxService.Models.Rate
{
    public class Rate
    {
        public string city { get; set; }
        public double city_rate { get; set; }
        public double combined_district_rate { get; set; }
        public double combined_rate { get; set; }
        public string country { get; set; }
        public double country_rate { get; set; }
        public string county { get; set; }
        public double county_rate { get; set; }
        public bool freight_taxable { get; set; }
        public string state { get; set; }
        public double state_rate { get; set; }
        public string zip { get; set; }
    }
}
