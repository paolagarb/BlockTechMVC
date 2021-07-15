using BlockTechMVC.Interfaces;
using BlockTechMVC.Models.MercadoBitcoin;
using BlockTechMVC.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BlockTechMVC.Repositories
{
    public class CriptomoedaMercadoBitcoinFacade : ICriptomoedaMercadoBitcoinFacade
    {
        private readonly MercadoBitcoinOptions optionsMonitor;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ICriptomoedaRepository criptomoedaRepository;
        private readonly ICriptomoedaHojeRepository criptomoedaHojeRepository;
        HttpClient client;

        public CriptomoedaMercadoBitcoinFacade(IOptionsMonitor<MercadoBitcoinOptions> optionsMonitor,
                                         IHttpClientFactory httpClientFactory,
                                         ICriptomoedaRepository criptomoedaRepository,
                                         ICriptomoedaHojeRepository criptomoedaHojeRepository)
        {
            this.optionsMonitor = optionsMonitor.CurrentValue;
            this.httpClientFactory = httpClientFactory;
            this.criptomoedaRepository = criptomoedaRepository;
            this.criptomoedaHojeRepository = criptomoedaHojeRepository;
            client = new HttpClient();
        }

        public async Task<DaySummaryMBResponse> ObterValor(string criptomoeda, DateTime data)
        {
            var uri = $"api/{criptomoeda}/day-summary/{data.Year}/{data.Month}/{data.Day}";

            client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(optionsMonitor.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DaySummaryMBResponse>(responseContent);

                //using var responseStream = await response.Content.ReadAsStreamAsync();
                //return await System.Text.Json.JsonSerializer.DeserializeAsync<DaySummaryMBResponse>(responseStream);
            }

            return new DaySummaryMBResponse();
        }

        public async Task<TickerMBResponse> ObterValorAtual(string criptomoeda)
        {
            var uri = $"api/{criptomoeda}/ticker";

            client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(optionsMonitor.BaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TickerMBResponse>(responseContent);

                //using var responseStream = await response.Content.ReadAsStreamAsync();
                //return await System.Text.Json.JsonSerializer.DeserializeAsync<TickerMBResponse>(responseStream);
            }

            return new TickerMBResponse();
        }
    }
}
