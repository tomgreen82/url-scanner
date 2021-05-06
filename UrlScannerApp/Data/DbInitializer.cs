using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IDbContextFactory<UrlScannerDbContext> contextFactory)
        {
            var context = contextFactory.CreateDbContext();
            context.Database.EnsureCreated();

            InitializeRecords(context);
            InitializeJobs(context);
        }

        private static void InitializeRecords(UrlScannerDbContext context)
        {
            if (context.Records.Any()) return;

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "UrlScannerApp.Data.records.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null
                    });
                    var records = csvReader.GetRecords<Record>().ToArray();
                    context.Records.AddRange(records);
                    context.SaveChanges();
                }
            }
        }
        private static void InitializeJobs(UrlScannerDbContext context)
        {
            if (context.CronJobs.Any()) return;

            context.CronJobs.Add(new CronJob() { Name = "UrlScanJob"});
            context.SaveChanges();
        }
    }
}
