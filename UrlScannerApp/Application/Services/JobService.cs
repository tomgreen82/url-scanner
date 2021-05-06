using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UrlScannerApp.Data.Models;
using UrlScannerApp.Data.Repositories;

namespace UrlScanner.Application.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<JobService> _logger;

        public JobService(IJobRepository jobRepository, ILogger<JobService> logger)
        {
            if (jobRepository == null) throw new ArgumentNullException(nameof(jobRepository));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _jobRepository = jobRepository;
            _logger = logger;
        }

        public async Task<CronJob> GetByName(string name)
        {
            return await _jobRepository.GetByName(name);
        }

        public async Task<int> AddOrUpdate(CronJob job)
        {
            var existingJob = await GetByName(job.Name);

            if (existingJob == null)
                return await _jobRepository.Add(job);

            existingJob.LastRun = job.LastRun;

            return await _jobRepository.Update(existingJob);
        }
    }
}
