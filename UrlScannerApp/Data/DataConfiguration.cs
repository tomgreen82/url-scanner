using Microsoft.Extensions.DependencyInjection;
using UrlScannerApp.Data.Repositories;

namespace UrlScannerApp.Data
{
    public static class DataConfiguration
    {
        /// <summary>
        /// Register services from the UrlScannerApp.Data project
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Updated service collection</returns>
        public static IServiceCollection ConfigureData(this IServiceCollection services)
        {
            services.AddScoped<IRecordRepository, RecordRepository>();
            return services;
        }
    }
}
