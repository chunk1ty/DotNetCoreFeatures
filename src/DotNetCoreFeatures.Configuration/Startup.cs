using DotNetCoreFeatures.Configuration.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace DotNetCoreFeatures.Configuration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //services.AddHostedService<ValidateOptionsService>();

            //services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));

            // simple option validation registration
            //services.AddOptions<FeaturesConfigurationWithSimpleValidation>()
            //        .Bind(Configuration.GetSection("Features"))
            //        .ValidateDataAnnotations();

            // advanced option validation registration
            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<FeaturesConfiguration>, FeaturesConfigurationWithAdvancedValidation>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseDeveloperExceptionPage();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
