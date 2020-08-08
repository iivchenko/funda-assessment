using SaleStatistics.Application.Repositories.SaleStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Repositories
{
    public sealed class InMemorySaleStatisticRepository : ISaleStatisticRepository
    {
        private IDictionary<Guid, SalesStatistic> _storage;

        public InMemorySaleStatisticRepository()
        {
            _storage = new Dictionary<Guid, SalesStatistic>();
        }

        public Task<IEnumerable<SalesStatistic>> GetSaleStatistics()
        {
            return Task.FromResult<IEnumerable<SalesStatistic>>(_storage.Values.ToList());
        }

        public Task UpdateSaleStatistics(SalesStatistic statistics)
        {
            lock (_storage)
            {
                if (_storage.ContainsKey(statistics.Id))
                {
                    _storage[statistics.Id] = statistics;
                }
                else
                {
                    _storage.Add(statistics.Id, statistics);
                }
            }

            return Task.CompletedTask;
        }
    }
}
