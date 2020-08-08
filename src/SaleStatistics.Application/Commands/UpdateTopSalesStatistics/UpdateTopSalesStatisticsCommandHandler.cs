using MediatR;
using SaleStatistics.Application.Repositories.SaleStatistics;
using SaleStatistics.Application.Services.Sales;
using System;
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
            // TODO:
            // Get all statistics from repoitory
            // for each statistics get creterias; query data from SalesService; calculate statistcs; update repository record
            throw new NotImplementedException();
        }
    }
}
