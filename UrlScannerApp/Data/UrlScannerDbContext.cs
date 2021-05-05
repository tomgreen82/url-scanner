using Microsoft.EntityFrameworkCore;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data
{
    public class UrlScannerDbContext : DbContext
    {
        public UrlScannerDbContext(DbContextOptions<UrlScannerDbContext> options) : base(options)
        {
        }

        public DbSet<Record> Records { get; set; }
    }
}
