using System.ComponentModel.DataAnnotations;

namespace DotNetCoreFeatures.Configuration.Configurations
{
    public class FeaturesConfigurationWithSimpleValidation
    {
        public bool EnableAnkFeature { get; set; }

        [Required]
        public string AnkText { get; set; }
    }
}
