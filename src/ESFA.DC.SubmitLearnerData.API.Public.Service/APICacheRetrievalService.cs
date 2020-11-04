using System;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class APICacheRetrievalService : IAPICacheRetrievalService
    {
        private readonly IMemoryCache _memoryCache;

        public APICacheRetrievalService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreate<T>(string key, int expiration, Task<T> cacheData)
        {
            return await _memoryCache.GetOrCreateAsync(key, mc =>
            {
                mc.SetSlidingExpiration(TimeSpan.FromMinutes(expiration));
                return cacheData;
            });
        }
    }
}
