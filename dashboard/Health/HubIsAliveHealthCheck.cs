

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace dashboard.Health
{
    public class HubIsAliveHealthCheck : IHealthCheck
    {
        private readonly ILogger<HubIsAliveHealthCheck> _logger;

        public HubIsAliveHealthCheck(ILogger<HubIsAliveHealthCheck> logger)
        {
            _logger = logger;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                _logger.LogDebug("Starting HubIsAliveHealthCheck");
           
                var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:4000/health")
                    .Build();

                connection.StartAsync().Wait();
                connection.StopAsync().Wait();

                return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy));
            }
            catch(Exception ex)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, exception: ex));
            }
        }
    }
}