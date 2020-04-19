using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DotNetCoreFeatures.Hosts.WorkerServices
{
    public class AdvancedWorker : BackgroundService
    {
        private readonly ILogger<AdvancedWorker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public AdvancedWorker(ILogger<AdvancedWorker> logger, 
                              IHostApplicationLifetime hostApplicationLifetime)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // register delegate when CancellationToken is canceled
            stoppingToken.Register(() =>
            {
                _logger.LogInformation("Ending score processing.");
            });

            _logger.LogInformation("AdvancedWorker starting ...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    DoSomeLogic();

                    await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(new EventId(17, "ArgumentNullException"), ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Male ...");
            }
            finally
            {
                _hostApplicationLifetime.StopApplication();
            }
        }

        private void DoSomeLogic()
        {
            //throw new ArgumentNullException();
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();

            await base.StopAsync(cancellationToken);

            _logger.LogInformation("Completed shutdown in {Milliseconds}ms.", sw.ElapsedMilliseconds);
        }
    }
}
