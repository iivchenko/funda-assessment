using MediatR;
using Microsoft.Extensions.Logging;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Application.Services.Sales;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Commands.UpdateTopSalesStatistics
{
    public sealed class UpdateTopSalesStatisticsCommandHandler : IRequestHandler<UpdateTopSalesStatisticsCommand, Unit>
    {
        private readonly ISaleService _saleService;
        private readonly ISaleStatisticRepository _saleStatisticRepository;
        private readonly ILogger<UpdateTopSalesStatisticsCommandHandler> _logger;

        public UpdateTopSalesStatisticsCommandHandler(
            ISaleService saleService, 
            ISaleStatisticRepository saleStatisticRepository,
            ILogger<UpdateTopSalesStatisticsCommandHandler> logger)
        {
            _saleService = saleService;
            _saleStatisticRepository = saleStatisticRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateTopSalesStatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var statistics = await _saleStatisticRepository.GetSaleStatistics();

                foreach (var statistic in statistics)
                {
                    var sales = await _saleService.ReadSales(statistic.Criteria.Filter);

                    statistic.Items =
                        sales
                            .GroupBy(x => x.AgentId)
                            .OrderByDescending(x => x.Count())
                            .Take(statistic.Criteria.Count)
                            .Select(x => new SaleStatisticItem { RealEstateAgent = x.First().AgentName, SalesCount = x.Count() });

                    statistic.DateUpdated = DateTime.UtcNow;

                    await _saleStatisticRepository.UpdateSaleStatistics(statistic);
                }
            }
            catch(Exception e)
            {
                _logger.LogCritical(e, "Fail to update statistics!");
            }

            return Unit.Value;
        }
    }
}
