using SaleStatistics.Application.Services.Sales;

namespace SaleStatistics.Application.Queries.GetTopSaleObjects
{
    public sealed class GetTopSaleObjectsQueryHandler
    {
        private readonly ISaleService _saleService;

        public GetTopSaleObjectsQueryHandler(ISaleService saleService)
        {
            _saleService = saleService;
        }
    }
}
