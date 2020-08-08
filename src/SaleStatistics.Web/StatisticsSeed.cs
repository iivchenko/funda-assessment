using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaleStatistics.Application.Repositories.SaleStatistics;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SaleStatistics.Web
{
    public static class StatisticsSeed
    {
        private const int TopCount = 10;
        private const string AmsterdamSalesFilter = "/amsterdam/";
        private const string AmsterdamWithGarenSalesFilter = "/amsterdam/tuin";

        public static void Seed(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var repository = scope.ServiceProvider.GetService<ISaleStatisticRepository>();

                var statistics = repository.GetSaleStatistics().Result;

                if(statistics.All(x => x.Criteria.Filter != AmsterdamSalesFilter))
                {
                    var topSalesAgentsStatistics = new SalesStatistic
                    {
                        Id = Guid.NewGuid(),
                        Descripiton = "Top 10 real estate agents by sale objects in Amsterdam!",
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                        Criteria = new SaleStatisticCriteria
                        {
                            Count = TopCount,
                            Filter = AmsterdamSalesFilter
                        },
                        Items = new Collection<SaleStatisticItem>()
                    };

                    repository.UpdateSaleStatistics(topSalesAgentsStatistics).Wait();
                }

                if (statistics.All(x => x.Criteria.Filter != AmsterdamWithGarenSalesFilter))
                {
                    var topSalesWithGardenAgentsStatistics = new SalesStatistic
                    {
                        Id = Guid.NewGuid(),
                        Descripiton = "Top 10 real estate agents by sale objects with garden in Amsterdam!",
                        DateCreated = DateTime.UtcNow,
                        DateUpdated = DateTime.UtcNow,
                        Criteria = new SaleStatisticCriteria
                        {
                            Count = TopCount,
                            Filter = AmsterdamWithGarenSalesFilter
                        },
                        Items = new Collection<SaleStatisticItem>()
                    };

                    repository.UpdateSaleStatistics(topSalesWithGardenAgentsStatistics).Wait();
                }
            }
        }
    }
}
