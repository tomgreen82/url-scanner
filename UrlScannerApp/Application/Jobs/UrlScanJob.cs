using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using UrlScanner.Application.Models;
using UrlScanner.Application.Services;

namespace UrlScanner.Application.Jobs
{
    public class UrlScanJob : CronJobService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<UrlScanJob> _logger;

        public UrlScanJob(IScheduleConfig<UrlScanJob> config, IServiceProvider serviceProvider, ILogger<UrlScanJob> logger)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("UrlScanJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} UrlScanJob is working.");
            var recordService = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IRecordService>();
            return recordService.ProcessRecords();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("UrlScanJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
