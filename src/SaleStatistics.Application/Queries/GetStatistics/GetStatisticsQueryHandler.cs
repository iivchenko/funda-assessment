using AutoMapper;
using MediatR;
using SaleStatistics.Application.Repositories.SaleStatistics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Queries.GetStatistics
{
    public sealed class GetStatisticsQueryHandler : IRequestHandler<GetStatisticsQuery, GetStatisticsQueryResponse>
    {
        private readonly ISaleStatisticRepository _saleStatisticRepository;
        private readonly IMapper _mapper;

        public GetStatisticsQueryHandler(ISaleStatisticRepository saleStatisticRepository, IMapper mapper)
        {
            _saleStatisticRepository = saleStatisticRepository;
            _mapper = mapper;
        }

        public async Task<GetStatisticsQueryResponse> Handle(GetStatisticsQuery request, CancellationToken cancellationToken)
        {
            var statistics = await _saleStatisticRepository.GetSaleStatistics();

            return new GetStatisticsQueryResponse 
            {
                Statistics = _mapper.Map<IEnumerable<SalesStatistic>, IEnumerable<GetStatisticsQueryResponseStatistic>>(statistics)
            };
        }
    }
}
