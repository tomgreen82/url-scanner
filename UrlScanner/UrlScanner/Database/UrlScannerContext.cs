using Microsoft.EntityFrameworkCore;
using UrlScanner.Models;

namespace UrlScanner.Database
{
    public class UrlScannerContext : DbContext
    {
        public UrlScannerContext(DbContextOptions<UrlScannerContext> options)
       : base(options) { }

        public DbSet<ScanData> Websites { get; set; }
    }
}
