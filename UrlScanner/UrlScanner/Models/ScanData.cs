using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlScanner.Models
{
    public class ScanData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }

        public bool HasGoogle { get; set; }

        public long ScanDuration { get; set; }

        public DateTime LastTimeScanned { get; set; }
    }
}
