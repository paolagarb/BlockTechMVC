using Newtonsoft.Json;
using System;

namespace BlockTechMVC.Models.MercadoBitcoin
{
    public class DaySummaryMBResponse
    {
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Date { get; set; }
        [JsonProperty("opening", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Opening { get; set; }
        [JsonProperty("closing", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Closing { get; set; }
        [JsonProperty("lowest", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Lowest { get; set; }
        [JsonProperty("highest", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Highest { get; set; }
        [JsonProperty("volume", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Volume { get; set; }
        [JsonProperty("quantity", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Quantity { get; set; }
        [JsonProperty("amount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Amount { get; set; }
        [JsonProperty("avg_price", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AvgPrice { get; set; }
    }
}
