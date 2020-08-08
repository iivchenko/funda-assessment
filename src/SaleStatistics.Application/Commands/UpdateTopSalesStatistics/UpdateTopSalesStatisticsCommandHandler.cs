using MediatR;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Application.Services.Sales;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Commands.UpdateTopSalesStatistics
{
    public sealed class UpdateTopSalesStatisticsCommandHandler : IRequestHandler<UpdateTopSalesStatisticsCommand, Unit>
    {
        private readonly ISaleService _saleService;
        private readonly ISaleStatisticRepository _saleStatisticRepository;

        public UpdateTopSalesStatisticsCommandHandler(ISaleService saleService, ISaleStatisticRepository saleStatisticRepository)
        {
            _saleService = saleService;
            _saleStatisticRepository = saleStatisticRepository;
        }

        public async Task<Unit> Handle(UpdateTopSalesStatisticsCommand request, CancellationToken cancellationToken)
        {
            var statistics = await _saleStatisticRepository.GetSaleStatistics();

            foreach(var statistic in statistics)
            {
                var sales = await _saleService.ReadSales(statistic.Criteria.Filter);

                statistic.Items =
                    sales
                        .GroupBy(x => x.AgentId)
                        .OrderByDescending(x => x.Count())
                        .Take(statistic.Criteria.Count)
                        .Select(x => new SaleStatisticItem { RealEstateAgent = x.First().AgentName, SalesCount = x.Count() });

                await _saleStatisticRepository.UpdateSaleStatistics(statistic);
            }

            return Unit.Value;
        }
    }
}
