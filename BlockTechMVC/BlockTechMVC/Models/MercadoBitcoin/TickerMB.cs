using Newtonsoft.Json;
using System;

namespace BlockTechMVC.Models.MercadoBitcoin
{
    public class TickerMB
    {
        [JsonProperty("high", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string High { get; set; }
        [JsonProperty("low", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Low { get; set; }
        [JsonProperty("vol", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Vol { get; set; }
        [JsonProperty("last", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Last { get; set; }
        [JsonProperty("buy", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Buy { get; set; }
        [JsonProperty("sell", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Sell { get; set; }
        [JsonProperty("open", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Open { get; set; }
        [JsonProperty("date", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Date { get; set; }
    }
}
