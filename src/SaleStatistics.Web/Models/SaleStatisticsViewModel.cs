using System.Collections.Generic;

namespace SaleStatistics.Web.Models
{
    public sealed class SaleStatisticsItem
    {
        public string Agent { get; set; }

        public int Count { get; set; }
    }

    public class SaleStatisticsViewModel
    {
        public IEnumerable<SaleStatisticsItem> TopTenAgentsWithSalesObjects { get; set; }

        public IEnumerable<SaleStatisticsItem> TopTenAgentsWithSalesObjectsAndGardens { get; set; }
    }
}
