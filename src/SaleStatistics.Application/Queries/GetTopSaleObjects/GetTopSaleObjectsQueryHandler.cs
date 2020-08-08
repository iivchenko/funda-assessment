using MediatR;
using SaleStatistics.Application.Services.Sales;
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

        public Task<GetTopSaleObjectsQueryResponse> Handle(GetTopSaleObjectsQuery request, CancellationToken cancellationToken)
        {
            // TODO:
            // Get all sales
            // Group by Agent Id
            // Select Angent name and sales count
            // Sort by sales
            // Filter top count
            throw new System.NotImplementedException();
        }
    }
}
