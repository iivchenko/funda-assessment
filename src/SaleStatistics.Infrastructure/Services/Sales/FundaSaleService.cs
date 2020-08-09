using AutoMapper;
using LazyCache;
using LazyCache.Providers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SaleStatistics.Application.Services.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaSaleService : ISaleService
    {
        private readonly IAppCache _cache;
        private readonly IMapper _mapper;
        private readonly IFundaClientFactory _factory;
        private readonly FundaSettings _settings;

        private int _curentCalls = 0;

        public FundaSaleService(
            IOptions<FundaSettings> settings,
            IFundaClientFactory factory,
            IMapper mapper)
        {
            _settings = settings.Value;
            _mapper = mapper;
            _factory = factory;

            _cache = new CachingService(new MemoryCacheProvider(new MemoryCache(new MemoryCacheOptions())));
        }

        public async Task<IEnumerable<Sale>> ReadSales(string filter)
        {
            var policy = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow + _settings.CacheExpiration,
                SlidingExpiration = _settings.RequestDelay
            };

            return await _cache.GetOrAddAsync(filter, async () =>
            {
                var client = _factory.Create();

                var page = 1;
                var sales = new List<Sale>();

                while (true)
                {
                    if (Interlocked.CompareExchange(ref _curentCalls, 1, 0) == 0)
                    {
                        try
                        {
                            await Task.Delay(_settings.RequestDelay);

                            var response = await client.GetObjects(_settings.Key, filter, page, _settings.PageSize);

                            if (response.Objects == null || !response.Objects.Any())
                            {
                                break;
                            }

                            sales.AddRange(_mapper.Map<IEnumerable<FundaObject>, IEnumerable<Sale>>(response.Objects));

                            page++;

                        }
                        finally
                        {
                            Interlocked.Exchange(ref _curentCalls, 0);
                        }
                    }
                }

                return sales;
            }, policy);
        }
    }
}
