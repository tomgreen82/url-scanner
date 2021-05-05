using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UrlScanner.Database;

namespace UrlScanner
{
    public class Worker : BackgroundService
    {
        private readonly UrlScannerContext _dbContext;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<UrlScannerContext>();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DatabaseInitializer.Initialize(_dbContext);
            while (!stoppingToken.IsCancellationRequested)
            {
                var websites = _dbContext.Websites.ToList();
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
