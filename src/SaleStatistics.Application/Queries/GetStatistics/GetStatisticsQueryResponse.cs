using System.Collections.Generic;

namespace SaleStatistics.Application.Queries.GetStatistics
{
    public sealed class GetStatisticsQueryResponseItem
    {
        public string RealEstateAgent { get; set; }

        public int SalesCount { get; set; }
    }

    public sealed class GetStatisticsQueryResponseStatistic
    {
        public string Description { get; set; }

        public IEnumerable<GetStatisticsQueryResponseItem> Statistics { get; set; }
    }

    public sealed class GetStatisticsQueryResponse
    {
        public IEnumerable<GetStatisticsQueryResponseStatistic> Statistics { get; set; }
    }
}
