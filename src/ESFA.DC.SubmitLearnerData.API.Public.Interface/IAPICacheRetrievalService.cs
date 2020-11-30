using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IAPICacheRetrievalService
    {
        Task<T> GetOrCreate<T>(string key, int expiration, Task<T> cacheData);
    }
}
