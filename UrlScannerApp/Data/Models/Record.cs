using System;

namespace UrlScannerApp.Data.Models
{
    public class Record
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public bool HasGoogle { get; set; }

        public long ScanDuration { get; set; }

        public DateTime LastTimeScanned { get; set; }
    }
}
