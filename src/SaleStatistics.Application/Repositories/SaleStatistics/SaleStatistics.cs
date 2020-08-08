using System;
using System.Collections.Generic;

namespace SaleStatistics.Application.Repositories.SaleStatistics
{
    public sealed class SaleStatisticCriteria
    {
        public long Count { get; set; }

        public string Filter { get; set; }
    }

    public sealed class SaleStatisticItem
    {
        public string RealEstateAgent { get; set; }

        public int SalesCount { get; set; }
    }

    public sealed class SalesStatistic
    {
        public Guid Id { get; set; }

        public SaleStatisticCriteria Criteria { get; set; }

        public string Descripiton { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public IEnumerable<SaleStatisticItem> Items { get; set; }
    }
}
