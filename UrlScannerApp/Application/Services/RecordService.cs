using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UrlScannerApp.Data.Models;
using UrlScannerApp.Data.Repositories;

namespace UrlScanner.Application.Services
{
    public class RecordService : IRecordService
    {
        private readonly IRecordRepository _recordRepository;
        private readonly ILogger<RecordService> _logger;

        public RecordService(IRecordRepository recordRepository, ILogger<RecordService> logger)
        {
            if (recordRepository == null) throw new ArgumentNullException(nameof(recordRepository));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            _recordRepository = recordRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Record>> GetAll()
        {
            return await _recordRepository.Get();
        }

        public async Task ProcessRecords()
        {
            var records = await _recordRepository.Get();

            var tasks = CreateTaskArray(records);
            Task.WaitAll(tasks);
        }

        private Task[] CreateTaskArray(IEnumerable<Record> records)
        {
            var tasks = new List<Task>();
            var recordArray = records.ToArray();
            for (var i = 0; i < recordArray.Length; i++)
            {
                var record = recordArray[i];
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var task = GetContent(record).ContinueWith(completed =>
                {
                    record.HasGoogle = ScanContent(completed.Result);
                    stopwatch.Stop();
                    record.LastTimeScanned = DateTime.Now;
                    record.ScanDuration = stopwatch.ElapsedMilliseconds;
                    _recordRepository.Update(record);
                });
                tasks.Add(task);
            }

            return tasks.ToArray();
        }

        private async Task<string> GetContent(Record record)
        {
            var client = new RestClient("https://" + record.Website);

            var result = await client.ExecuteAsync(new RestRequest());

            return result.Content;
        }

        private bool ScanContent(string content)
        {
            return content.Contains("www.google-analytics.com");
        }
    }
}
