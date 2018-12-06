using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace hub 
{
    public class HealthHub : Hub
    {
        private readonly ILogger<HealthHub> _logger;

        public HealthHub(ILogger<HealthHub> logger)
        {
            _logger = logger;
        }   

        public override Task OnConnectedAsync()
        {
            _logger.LogDebug("Client connected with ConnectionId " + this.Context.ConnectionId);
            return Task.FromResult(0);
        }
    }
}