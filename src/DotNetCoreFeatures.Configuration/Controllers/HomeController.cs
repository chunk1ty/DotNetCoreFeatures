using DotNetCoreFeatures.Configuration.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DotNetCoreFeatures.Configuration.Controllers
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel(bool enableAnkFeature, string ankText)
        {
            EnableAnkFeature = enableAnkFeature;
            AnkText = ankText;
        }

        public bool EnableAnkFeature { get; private set; }

        public string AnkText { get; private set; }
    }

    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly FeaturesConfiguration _featuresConfiguration;
        private readonly FeaturesConfiguration _featuresConfigurationSnapshot;
        private readonly FeaturesConfiguration _featuresConfigurationMonitor;

        private readonly FeaturesConfigurationWithSimpleValidation _featuresConfigurationWithSimpleValidation;

        public HomeController(IConfiguration configuration,
                              IOptions<FeaturesConfiguration> featuresConfiguration,
                              IOptionsSnapshot<FeaturesConfiguration> featuresConfigurationSnapshot,
                              IOptionsMonitor<FeaturesConfiguration> featuresConfigurationMonitor,
                              IOptions<FeaturesConfigurationWithSimpleValidation> featuresConfigurationWithSimpleValidation)
        {
            _configuration = configuration;
            _featuresConfiguration = featuresConfiguration.Value;
            _featuresConfigurationSnapshot = featuresConfigurationSnapshot.Value;
            _featuresConfigurationMonitor = featuresConfigurationMonitor.CurrentValue;
            _featuresConfigurationWithSimpleValidation = featuresConfigurationWithSimpleValidation.Value;
        }

        public IActionResult Index()
        {
            // 1) directly 
            //var enableAnkFeature =  _configuration.GetValue<bool>("Features:EnableAnkFeature");
            //var ankText = _configuration.GetValue<string>("Features:AnkText");
            //return View(new HomeIndexViewModel(enableAnkFeature, ankText));

            // 2) section
            //var featuresSection = _configuration.GetSection("Features");
            //var enableAnkFeature = featuresSection.GetValue<bool>("EnableAnkFeature");
            //var ankText = featuresSection.GetValue<string>("AnkText");
            //return View(new HomeIndexViewModel(enableAnkFeature, ankText));

            // 3) binding (names should match Features:EnableAnkFeature = feature.EnableAnkFeature)
            //var feature = new FeaturesConfiguration();
            //_configuration.Bind("Features", feature);
            //return View(new HomeIndexViewModel(feature.EnableAnkFeature, feature.AnkText));

            // 4) IOptions<FeaturesConfiguration> featuresConfiguration
            // Registered as singleton. It should be configured like:   
            // services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            //return View(new HomeIndexViewModel(_featuresConfiguration.EnableAnkFeature, _featuresConfiguration.AnkText));

            // 4) IOptionsSnapshot<FeaturesConfiguration> featuresConfiguration
            // Registered as scoped. It should be configured like: 
            // services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            // return View(new HomeIndexViewModel(_featuresConfigurationSnapshot.EnableAnkFeature, _featuresConfigurationSnapshot.AnkText));

            // 5) IOptionsMonitor<FeaturesConfiguration> featuresConfiguration
            // Registered as singleton but returns newest configurations. It should be configured like: 
            // services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            // return View(new HomeIndexViewModel(_featuresConfigurationMonitor.EnableAnkFeature, _featuresConfigurationMonitor.AnkText));

            // 6) simple option validation
            //return View(new HomeIndexViewModel(_featuresConfigurationWithSimpleValidation.EnableAnkFeature,
            //                                   _featuresConfigurationWithSimpleValidation.AnkText));

            // 7) advanced option validation
            return View(new HomeIndexViewModel(_featuresConfiguration.EnableAnkFeature, _featuresConfiguration.AnkText));
        }
    }
}
