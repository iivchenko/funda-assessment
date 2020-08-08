using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Repositories.SaleStatistics
{
    public interface ISaleStatisticRepository
    {
        Task<IEnumerable<SalesStatistic>> GetSaleStatistics();

        Task UpdateSaleStatistics(SalesStatistic statistics);
    }
}
