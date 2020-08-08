using AutoMapper;
using Microsoft.Extensions.Options;
using SaleStatistics.Application.Services.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaSaleService : ISaleService
    {
        private const int Page = 1;
        private const int PageSize = 1;

        private readonly Guid _key;
        private readonly IFundaClient _client;
        private readonly IMapper _mapper;

        public FundaSaleService(
            IOptions<FundaSettings> settings,
            IFundaClient client, 
            IMapper mapper)
        {
            _key = settings.Value.Key;
            _client = client;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sale>> ReadSales(string filter)
        {
            var countResponse = await _client.GetObjects(_key, filter, Page, PageSize);
            var salesResponse = await _client.GetObjects(_key, filter, Page, countResponse.TotalObjects);

            return _mapper.Map<IEnumerable<FundaObject>, IEnumerable<Sale>>(salesResponse.Objects);
        }
    }
}
