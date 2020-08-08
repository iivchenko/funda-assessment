using System;
using System.Collections.Generic;

namespace SaleStatistics.Web.Models
{
    public sealed class SaleStatisticItemViewModel
    {
        public string Agent { get; set; }

        public int Count { get; set; }
    }

    public sealed class SaleStatisticViewModel
    {
        public string Description { get; set; }

        public DateTime DateUpdated { get; set; }

        public IEnumerable<SaleStatisticItemViewModel> Statistics { get; set; }
    }

    public class SaleStatisticsViewModel
    {
        public IEnumerable<SaleStatisticViewModel> Statistics { get; set; }
    }
}
