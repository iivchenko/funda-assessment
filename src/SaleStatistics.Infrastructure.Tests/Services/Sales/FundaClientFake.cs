using SaleStatistics.Infrastructure.Services.Sales;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Tests.Services.Sales
{
    public sealed class FundaClientFake : IFundaClient
    {
        public const string NumberOfRequestsExceeded = "Number of requests exceeded!";
        private readonly int _itemsPerPage;
        private readonly int _maxPages;
        private readonly int _maxRequestsPerMinute;
        private readonly Stopwatch _stopwatch;

        private int _requests;

        private int _getObjectsMethodCallCount;

        public FundaClientFake(int itemsPerPage, int maxPages, int maxRequestsPerMinute)
        {
            _itemsPerPage = itemsPerPage;
            _maxPages = maxPages;
            _maxRequestsPerMinute = maxRequestsPerMinute;

            _stopwatch = new Stopwatch();

            _requests = 0;
            _getObjectsMethodCallCount = 0;
        }

        public int GetObjectsMethodCallCount => _getObjectsMethodCallCount;

        public Task<FundaResponse> GetObjects(Guid key, string query, int page, int pageSize)
        {
            _getObjectsMethodCallCount++;

            if (_stopwatch.ElapsedMilliseconds >= 3000)
            {
                _requests = 0;
            }

            if (_requests == 0)
            {
                _stopwatch.Restart();
            }

            _requests++;

            if (_requests > _maxRequestsPerMinute)
            {
                _stopwatch.Stop();
                throw new Exception(NumberOfRequestsExceeded);
            }

            var result = page > _maxPages
                ? new FundaResponse()
                    {
                        Objects = Enumerable.Empty<FundaObject>()
                    }
                : new FundaResponse()
                    {
                        Objects = Enumerable.Repeat(new FundaObject { AgentId = 1, AgentName = string.Empty, Id = Guid.NewGuid() }, _itemsPerPage)
                    };

            return Task.FromResult(result);
        }       
    }
}
