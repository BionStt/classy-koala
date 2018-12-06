using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace hub
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHubContext<HealthHub> _healthHub;

        public Worker(ILogger<Worker> logger, IHubContext<HealthHub> healthHub)
        {
            _logger = logger;
            _healthHub = healthHub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await _healthHub.Clients.All.SendAsync("HubHostPing", $"HubHostPing running at: {DateTime.Now}");
                await Task.Delay(3000);
            }
        }
    }
}