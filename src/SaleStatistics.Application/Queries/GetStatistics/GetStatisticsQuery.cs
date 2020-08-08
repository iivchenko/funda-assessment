using MediatR;

namespace SaleStatistics.Application.Queries.GetStatistics
{
    public sealed class GetStatisticsQuery : IRequest<GetStatisticsQueryResponse>
    {
    }
}
