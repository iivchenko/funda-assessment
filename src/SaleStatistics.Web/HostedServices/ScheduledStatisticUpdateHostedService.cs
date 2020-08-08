using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SaleStatistics.Application.Commands.UpdateTopSalesStatistics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Web.HostedServices
{
    public sealed class ScheduledStatisticUpdateHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ScheduledStatisticUpdateHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async  override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var schedule = 60000d;

            using (var scope = _serviceProvider.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                schedule = TimeSpan.Parse(configuration.GetValue<string>("StatisticsUpdateInterval")).TotalMilliseconds;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new UpdateTopSalesStatisticsCommand());
                }

                await Task.Delay((int)schedule, stoppingToken);
            }
        }
    }
}
