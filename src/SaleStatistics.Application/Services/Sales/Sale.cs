using System;

namespace SaleStatistics.Application.Services.Sales
{
    public sealed class Sale
    {
        public Guid Id { get; set; }

        public long AgentId { get; set; }

        public string AgentName { get; set; }
    }
}
