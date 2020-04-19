using System.Threading;
using System.Threading.Tasks;
using DotNetCoreFeatures.Hosting.WorkerServices.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreFeatures.Hosting.WorkerServices
{
    // pick messages from channel - (Consumer)
    public class ChannelWorker : BackgroundService
    {
        private readonly MyChannel _myChannel;
        private readonly ILogger<ChannelWorker> _logger;

        public ChannelWorker(MyChannel myChannel, ILogger<ChannelWorker> logger)
        {
            _myChannel = myChannel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var message in _myChannel.ReadAllAsync(stoppingToken))
            {
                _logger.LogInformation($"ChannelWorker start processing message {message}.");
            }
        }
    }
}
