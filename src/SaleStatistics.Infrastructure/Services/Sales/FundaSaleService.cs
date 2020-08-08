using AutoMapper;
using SaleStatistics.Application.Services.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaSaleService : ISaleService
    {
        public readonly IFundaClient _client;
        private readonly IMapper _mapper;

        public FundaSaleService(IFundaClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Sale>> ReadSales(string filter)
        {
            var countResponse = await _client.GetObjects(new Guid("ac1b0b1572524640a0ecc54de453ea9f"), filter, 1, 1);
            var salesResponse = await _client.GetObjects(new Guid("ac1b0b1572524640a0ecc54de453ea9f"), filter, 1, countResponse.TotalObjects);

            return _mapper.Map<IEnumerable<FundaObject>, IEnumerable<Sale>>(salesResponse.Objects);
        }
    }
}
