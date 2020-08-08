using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaleStatistics.Application.Repositories.SaleStatistics;
using System;
using System.Collections.ObjectModel;

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

                repository.UpdateSaleStatistics(topSalesAgentsStatistics).Wait();
                repository.UpdateSaleStatistics(topSalesWithGardenAgentsStatistics).Wait();
            }
        }
    }
}
