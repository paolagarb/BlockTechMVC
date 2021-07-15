using Newtonsoft.Json;

namespace BlockTechMVC.Models.MercadoBitcoin
{
    public class TickerMBResponse
    {
        [JsonProperty("ticker", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TickerMB Ticker { get; set; }
    }
}
