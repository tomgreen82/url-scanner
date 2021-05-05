using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlScanner.Application.Services;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRecordService _recordService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IRecordService recordService, ILogger<IndexModel> logger)
        {
            _recordService = recordService;
            _logger = logger;
        }

        public async Task OnGet()
        {
            Records = await _recordService.GetAll();
        }

        public async Task OnPost()
        {
            Records = await _recordService.GetAll();
        }

        public IEnumerable<Record> Records { get; set; }
    }
}
