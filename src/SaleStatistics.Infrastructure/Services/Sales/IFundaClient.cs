using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaResponse
    {
        public IEnumerable<FundaObject> Objects { get; set; } 
        
        [JsonProperty("TotaalAantalObjecten")]
        public int TotalObjects { get; set; }
    }

    public sealed class FundaObject
    {
        public Guid Id { get; set; }

        [JsonProperty("MakelaarId")]
        public long AgentId { get; set; }

        [JsonProperty("MakelaarNaam")]
        public string AgentName { get; set; }
    }

    public interface IFundaClient
    {
        [Get("/feeds/Aanbod.svc/json/{key}?type=koop&zo={query}/&page={page}&pagesize={pageSize}")]
        Task<FundaResponse> GetObjects(Guid key, string query, int page, int pageSize);
    }
}
