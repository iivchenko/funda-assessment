using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaleStatistics.Web.HostedServices
{
    public sealed class ScheduledStatisticUpdateHostedService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // TODO: 
            // Start timer
            // By schedule push update statistics command
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // TODO:
            // stop timver
            // Wait for the update if in progress
            // dispose timer
            throw new NotImplementedException();
        }
    }
}
