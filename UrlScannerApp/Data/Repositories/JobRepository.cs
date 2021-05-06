using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IDbContextFactory<UrlScannerDbContext> _contextFactory;
        public JobRepository(IDbContextFactory<UrlScannerDbContext> contextFactory)
        {
            if (contextFactory == null) throw new ArgumentNullException(nameof(contextFactory));

            _contextFactory = contextFactory;
        }

        public async Task<CronJob> GetByName(string name)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.CronJobs.SingleAsync(cj => cj.Name.ToLower() == name.ToLower());
            }
        }

        public async Task<int> Update(CronJob job)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.CronJobs.Update(job);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> Add(CronJob job)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.CronJobs.Add(job);
                return await context.SaveChangesAsync();
            }
        }
    }
}
