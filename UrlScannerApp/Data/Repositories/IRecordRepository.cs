using System.Collections.Generic;
using System.Threading.Tasks;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data.Repositories
{
    public interface IRecordRepository
    {
        /// <summary>
        /// Get all records from the database
        /// </summary>
        /// <returns>A List of <see cref="Record"/></returns>
        Task<IEnumerable<Record>> Get();

        /// <summary>
        /// Update a <see cref="Record"/>
        /// </summary>
        /// <returns>Number of entities updated</returns>
        Task<int> Update(Record record);
    }
}
