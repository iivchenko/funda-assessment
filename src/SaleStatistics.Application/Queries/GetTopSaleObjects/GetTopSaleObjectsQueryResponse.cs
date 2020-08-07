using System.Collections.Generic;

namespace SaleStatistics.Application.Queries.GetTopSaleObjects
{
    public sealed class GetTopSaleObjectsQueryResponseItem
    {
        public string RealEstateAgent { get; set; }

        public int SalesCount { get; set; }
    }

    public sealed class GetTopSaleObjectsQueryResponse
    {
        public IEnumerable<GetTopSaleObjectsQueryResponseItem> Statistics { get; set; }
    }
}
