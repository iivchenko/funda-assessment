using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Net.Http;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaClientFactory : IFundaClientFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FundaSettings _settings;

        public FundaClientFactory(IHttpClientFactory httpClientFactory, IOptions<FundaSettings> options)
        {
            _httpClientFactory = httpClientFactory;
            _settings = options.Value;
        }

        public IFundaClient Create()
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_settings.ApiAddress);

            return RestService.For<IFundaClient>(client);
        }
    }
}
