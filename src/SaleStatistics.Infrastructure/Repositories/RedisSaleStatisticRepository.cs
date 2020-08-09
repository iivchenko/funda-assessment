using Newtonsoft.Json;
using SaleStatistics.Application.Repositories.SaleStatistics;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Repositories
{
    public sealed class RedisSaleStatisticRepository : ISaleStatisticRepository, IDisposable
    {
        private const string KeyPattern = "statistic:";

        private readonly ConnectionMultiplexer _redis;

        public RedisSaleStatisticRepository(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }

        public Task<IEnumerable<SalesStatistic>> GetSaleStatistics()
        {
            var server = _redis.GetServer(_redis.GetEndPoints().First());
            var db = _redis.GetDatabase();

            var statistics =
                 server
                 .Keys(pattern: $"{KeyPattern}*")
                 .Select(x => JsonConvert.DeserializeObject<SalesStatistic>(db.StringGet(x)));

            return Task.FromResult(statistics);
        }

        public Task UpdateSaleStatistics(SalesStatistic statistics)
        {
            var db = _redis.GetDatabase();
            db.StringSet($"{KeyPattern}{statistics.Id}", JsonConvert.SerializeObject(statistics));

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _redis.Dispose();
        }
    }
}
