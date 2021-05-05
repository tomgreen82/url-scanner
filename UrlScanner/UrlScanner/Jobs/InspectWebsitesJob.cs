using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;

namespace UrlScanner.Jobs
{
    [DisallowConcurrentExecution]
    public class InspectWebsitesJob : IJob
    {
        public readonly ILogger<InspectWebsitesJob> _logger;
        public InspectWebsitesJob(ILogger<InspectWebsitesJob> logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"{DateTime.Now}: {nameof(InspectWebsitesJob)} running!");
            return Task.CompletedTask;
        }
    }
}
