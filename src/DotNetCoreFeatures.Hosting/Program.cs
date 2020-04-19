//using DotNetCoreFeatures.Hosts.WebApplications;

using DotNetCoreFeatures.Hosting.WorkerServices;
using DotNetCoreFeatures.Hosting.WorkerServices.Channels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCoreFeatures.Hosting
{
    public class Program
    {        
        public static void Main(string[] args)
        {
            // project sdk should be Microsoft.NET.Sdk.Web
            // endpoint -> localhost:port/home
            //CreateWebHostBuilder(args).Build().Run();

            // project sdk should be Microsoft.NET.Sdk
            CreateServiceHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateServiceHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<SimpleWorker>();

                    services.AddSingleton<MyChannel>();
                    services.AddHostedService<ChannelWorker>();

                    services.AddHostedService<AdvancedWorker>();
                });
    }
}
