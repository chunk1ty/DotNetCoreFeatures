using Microsoft.Extensions.Options;

namespace DotNetCoreFeatures.Configuration.Configurations
{
    public class FeaturesConfigurationWithAdvancedValidation : IValidateOptions<FeaturesConfiguration>
    {
        // i can inject services and include them in validation
        private AnkService _ankService;

        public ValidateOptionsResult Validate(string name, FeaturesConfiguration options)
        {
            //if (options.AnkText.Length <= 5 && _ankService.IsEnabled)
            //{
            //}

            if (options.AnkText.Length <= 5)
            {
                return ValidateOptionsResult.Fail("Text should be at least 5 characters.");
            }

            if (options.AnkText.Length >= 500)
            {
                return ValidateOptionsResult.Fail("Text shouldn't be more than 500 characters.");
            }

            return ValidateOptionsResult.Success;
        }
    }

    public class AnkService
    {
        public bool IsEnabled => true;
    }
}
