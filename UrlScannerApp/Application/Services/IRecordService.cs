using System.Collections.Generic;
using System.Threading.Tasks;
using UrlScannerApp.Data.Models;

namespace UrlScanner.Application.Services
{
    public interface IRecordService
    {
        /// <summary>
        /// Retrieve all records
        /// </summary>
        /// <returns>IEnumerable of all <see cref="Record"/></returns>
        Task<IEnumerable<Record>> GetAll();

        /// <summary>
        /// Process all records
        /// </summary>
        Task ProcessRecords();
    }
}
