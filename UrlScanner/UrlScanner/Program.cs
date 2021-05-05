using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using UrlScanner.Database;
using UrlScanner.Jobs;

namespace UrlScanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q =>
                    {
                        q.UseMicrosoftDependencyInjectionScopedJobFactory();
                        q.AddJob<InspectWebsitesJob>(opts => opts.WithIdentity(nameof(InspectWebsitesJob)));

                        q.AddTrigger(opts => opts
                            .ForJob(nameof(InspectWebsitesJob))
                            .WithIdentity("InspectWebsitesJob-Trigger")
                            .WithCronSchedule("0 0/5 * * * ?"));
                    });
                    services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
                    services.AddDbContext<UrlScannerContext>(options => options.UseInMemoryDatabase("UrlScanner"));
                    services.AddHostedService<Worker>();
                });
    }
}
