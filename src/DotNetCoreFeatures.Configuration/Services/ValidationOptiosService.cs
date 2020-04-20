using System.Threading;
using System.Threading.Tasks;
using DotNetCoreFeatures.Configuration.Configurations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetCoreFeatures.Configuration.Services
{
    public class ValidateOptionsService : BackgroundService
    {
        private readonly ILogger<ValidateOptionsService> _logger;
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IOptions<FeaturesConfigurationWithSimpleValidation> _features;

        public ValidateOptionsService(ILogger<ValidateOptionsService> logger,
                                      IHostApplicationLifetime appLifetime,
                                      IOptions<FeaturesConfigurationWithSimpleValidation> features)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            _features = features;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                // accessing this triggers validation
                _ = _features.Value; 
            }
            catch (OptionsValidationException ex)
            {
                _logger.LogError("One or more options validation checks failed.");

                foreach (var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }

                _appLifetime.StopApplication();
            }

            return Task.CompletedTask;
        }
    }
}
