using System.Threading.Tasks;
using UrlScannerApp.Data.Models;

namespace UrlScannerApp.Data.Repositories
{
    public interface IJobRepository
    {
        Task<CronJob> GetByName(string name);

        Task<int> Update(CronJob job);

        Task<int> Add(CronJob job);
    }
}
