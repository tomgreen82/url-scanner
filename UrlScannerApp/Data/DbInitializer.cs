using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(UrlScannerDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Records.Any())
            {
                return;
            }

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
    }
}
