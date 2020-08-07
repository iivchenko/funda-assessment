using System.Collections.Generic;

namespace SaleStatistics.Application.Services.Sales
{
    public interface ISaleService
    {
        IEnumerable<Sale> ReadSales(string filter);
    }
}
