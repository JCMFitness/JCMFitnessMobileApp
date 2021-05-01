using System.Threading.Tasks;

namespace JCMFitnessMobileApp.Services
{
    public interface ISyncService
    {
        Task PullSync();
        Task PushSync();
        Task PopulateLocalDBInitial();
    }
}