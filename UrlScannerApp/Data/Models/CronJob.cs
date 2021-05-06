using System;

namespace UrlScannerApp.Data.Models
{
    public class CronJob
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
    }
}
