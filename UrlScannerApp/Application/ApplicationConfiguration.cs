using Microsoft.Extensions.DependencyInjection;
using System;
using UrlScanner.Application.Jobs;
using UrlScanner.Application.Models;
using UrlScanner.Application.Services;

namespace UrlScanner.Application
{
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Register services from the UrlScannerApp.Application project
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.AddCronJob<UrlScanJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = @"*/1 * * * *";
            });
            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IJobService, JobService>();
            return services;
        }

        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
