using System.Collections.Generic;

namespace SaleStatistics.Application.Repositories.SaleStatistics
{
    public interface ISaleStatisticRepository
    {
        IEnumerable<SalesStatistic> GetSaleStatistics();

        void UpdateSaleStatistics(SalesStatistic statistics);
    }
}
