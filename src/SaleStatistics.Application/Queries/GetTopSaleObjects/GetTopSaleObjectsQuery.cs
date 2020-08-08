using MediatR;

namespace SaleStatistics.Application.Queries.GetTopSaleObjects
{
    public sealed class GetTopSaleObjectsQuery : IRequest<GetTopSaleObjectsQueryResponse>
    {
        public int Count { get; set; }

        public string Filter { get; set; }
    }
}
