using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Application.Services.Sales
{
    public interface ISaleService
    {
        Task<IEnumerable<Sale>> ReadSales(string filter);
    }
}
