namespace TaxCalculator.Models.Rate
{
    public class Rate
    {
        public string City { get; set; }
        public double CityRate { get; set; }
        public double CombinedDistrictRate { get; set; }
        public double CombinedRate { get; set; }
        public string Country { get; set; }
        public double CountryRate { get; set; }
        public string County { get; set; }
        public double CountyRate { get; set; }
        public bool FreightTaxable { get; set; }
        public string State { get; set; }
        public double StateRate { get; set; }
        public string Zip { get; set; }
    }
}
