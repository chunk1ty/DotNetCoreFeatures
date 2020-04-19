using System;
using System.Threading;
using System.Threading.Tasks;
using DotNetCoreFeatures.Hosting.WorkerServices.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreFeatures.Hosting.WorkerServices
{
    // send message on every 5 sec (Producer)
    public class SimpleWorker : BackgroundService
    {
        private readonly MyChannel _myChannel;
        private readonly ILogger<SimpleWorker> _logger;

        public SimpleWorker(MyChannel myChannel, ILogger<SimpleWorker> logger)
        {
            _myChannel = myChannel;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("SimpleWorker start executing ...");

               await _myChannel.AddMessage("message", stoppingToken);

               await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
