using Microsoft.Extensions.Logging;

namespace Continero.Homework
{
    public class ControlService
    {
        private readonly ILogger<ControlService> _logger;

        public ControlService(ILogger<ControlService> logger)
        {
            _logger = logger;
        }

        public async Task ExecuteAsync(CancellationToken stoppingToken = default)
        {
            _logger.LogInformation("Doing something");
        }
    }
}