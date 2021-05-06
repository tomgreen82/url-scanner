using System.Threading.Tasks;
using UrlScannerApp.Data.Models;

namespace UrlScanner.Application.Services
{
    public interface IJobService
    {
        Task<CronJob> GetByName(string name);

        Task<int> AddOrUpdate(CronJob job);
    }
}
