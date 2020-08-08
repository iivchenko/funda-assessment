using SaleStatistics.Application.Repositories.SaleStatistics;
using System;
using System.Collections.Generic;

namespace SaleStatistics.Infrastructure.Repositories
{
    public sealed class InMemorySaleStatisticRepository : ISaleStatisticRepository
    {
        private IDictionary<Guid, SalesStatistic> _statistics;

        public InMemorySaleStatisticRepository()
        {
            _statistics = new Dictionary<Guid, SalesStatistic>();
        }

        public IEnumerable<SalesStatistic> GetSaleStatistics()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSaleStatistics(SalesStatistic statistics)
        {
            throw new System.NotImplementedException();
        }
    }
}
