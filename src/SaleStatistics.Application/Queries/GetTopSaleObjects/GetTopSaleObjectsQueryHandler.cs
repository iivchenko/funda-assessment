using MediatR;
using SaleStatistics.Application.Services.Sales;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Queries.GetTopSaleObjects
{
    public sealed class GetTopSaleObjectsQueryHandler : IRequestHandler<GetTopSaleObjectsQuery, GetTopSaleObjectsQueryResponse>
    {
        private readonly ISaleService _saleService;

        public GetTopSaleObjectsQueryHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }

        public async Task<GetTopSaleObjectsQueryResponse> Handle(GetTopSaleObjectsQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleService.ReadSales(request.Filter);
                
            return new GetTopSaleObjectsQueryResponse
            {
                Statistics =
                    sales
                        .GroupBy(x => x.AgentId)
                        .OrderByDescending(x => x.Count())
                        .Take(request.Count)
                        .Select(x => new GetTopSaleObjectsQueryResponseItem { RealEstateAgent = x.First().AgentName, SalesCount = x.Count() })
            };
        }
    }
}
