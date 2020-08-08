using System;

namespace SaleStatistics.Application.Services.Sales
{
    public sealed class Sale
    {
        public Sale(
            Guid id, 
            long agentId,
            string agentName)
        {
            Id = id;
            AgentId = agentId;
            AgentName = agentName;
        }

        public Guid Id { get; private set; }

        public long AgentId { get; private set; }

        public string AgentName { get; private set; }
    }
}
