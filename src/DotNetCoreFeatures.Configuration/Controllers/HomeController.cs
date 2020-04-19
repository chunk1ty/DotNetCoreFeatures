using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DotNetCoreFeatures.Configuration.Controllers
{
    public class FeaturesConfiguration
    {
        public bool EnableAnkFeature { get; set; }

        public string AnkText { get; set; }
    }

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
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly FeaturesConfiguration _featuresConfiguration;

        public HomeController(ILogger<HomeController> logger, 
                              IConfiguration configuration, 
                              IOptions<FeaturesConfiguration> featuresConfiguration)
        {
            _logger = logger;
            _configuration = configuration;
            _featuresConfiguration = featuresConfiguration.Value;
        }

        public IActionResult Index()
        {
            // 1) directly 
            //var enableAnkFeature =  _configuration.GetValue<bool>("Features:EnableAnkFeature");

            // 2) section
            var featuresSection = _configuration.GetSection("Features");
            var enableAnkFeature = featuresSection.GetValue<bool>("EnableAnkFeature");

            // 3) binding (names should match Features:EnableAnkFeature = feature.EnableAnkFeature)
            //var feature = new FeaturesConfiguration();
            //_configuration.Bind("Features", feature);

            // 4) IOptions<FeaturesConfiguration> featuresConfiguration
            // It should be registered  
            // services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));

            return View(new HomeIndexViewModel(_featuresConfiguration.EnableAnkFeature, _featuresConfiguration.AnkText));
        }
    }
}
