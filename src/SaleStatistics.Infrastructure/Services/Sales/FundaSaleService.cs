using SaleStatistics.Application.Services.Sales;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaSaleService : ISaleService
    {
        public readonly IFundaClient _client;

        public FundaSaleService(IFundaClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Sale>> ReadSales(string filter)
        {
            switch (filter)
            {
                case "/amsterdam/tuin":

                    return new List<Sale>
                      {
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  1,
                                  "Agent 1"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  1,
                                  "Agent 1"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  1,
                                  "Agent 1"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  2,
                                  "Agent 2"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  3,
                                  "Agent 3"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  1,
                                  "Agent 1"
                              ),
                      };
                default:
                    return new List<Sale>
                      {
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  2,
                                  "Agent 2"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  2,
                                  "Agent 2"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  1,
                                  "Agent 1"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  2,
                                  "Agent 2"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  3,
                                  "Agent 3"
                              ),
                              new Sale
                              (
                                  Guid.NewGuid(),
                                  1,
                                  "Agent 1"
                              ),
                      };
            }
        }
    }
}
