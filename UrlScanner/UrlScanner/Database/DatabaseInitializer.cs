using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace UrlScanner.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize(UrlScannerContext context)
        {
            if (context.Websites.Any())
            {
                return; // Data was already seeded
            }

            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "UrlScanner.Data.websites.csv";
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
                    var websites = csvReader.GetRecords<Models.ScanData>().ToArray();
                    context.Websites.AddRange(websites);
                    context.SaveChanges();
                }
            }
        }
    }
}
