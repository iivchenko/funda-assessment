using SaleStatistics.Application.Services.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaSaleService : ISaleService
    {
        public readonly IFundaClient _client;

        public FundaSaleService(IFundaClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<Sale>> ReadSales(string filter)
        {
            throw new System.NotImplementedException();
        }
    }
}
