using SaleStatistics.Application.Repositories.SaleStatistics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Repositories
{
    public sealed class InMemorySaleStatisticRepository : ISaleStatisticRepository
    {
        private IDictionary<Guid, SalesStatistic> _statistics;

        public InMemorySaleStatisticRepository()
        {
            _statistics = new Dictionary<Guid, SalesStatistic>();
        }

        public Task<IEnumerable<SalesStatistic>> GetSaleStatistics()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateSaleStatistics(SalesStatistic statistics)
        {
            throw new System.NotImplementedException();
        }
    }
}
