using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly IDbContextFactory<UrlScannerDbContext> _contextFactory;
        public RecordRepository(IDbContextFactory<UrlScannerDbContext> contextFactory)
        {
            if (contextFactory == null) throw new ArgumentNullException(nameof(contextFactory));

            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Record>> Get()
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.Records.ToListAsync();
            }
        }

        public async Task<int> Update(Record record)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                context.Records.Update(record);
                return await context.SaveChangesAsync();
            }
        }
    }
}
