using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests
{
    public class APICacheRetrievalServiceTests
    {
        [Fact]
        public async Task GetOrCreate()
        {
            var list = new List<string>
            {
                "Item1",
                "Item2",
                "Item3"
            };

            IMemoryCache memoryCache = MemoryCache();

            memoryCache.TryGetValue("Key", out bool value);

            value.Should().BeFalse();

            var result = await NewService(memoryCache).GetOrCreate("Key", 1, TestCacheItem());

            result.Should().BeEquivalentTo(list);
        }

        private async Task<List<string>> TestCacheItem()
        {
            return new List<string>
            {
                "Item1",
                "Item2",
                "Item3"
            };
        }

        private IMemoryCache MemoryCache() => new MemoryCache(new MemoryCacheOptions());

        private APICacheRetrievalService NewService(IMemoryCache memoryCache = null)
        {
            return new APICacheRetrievalService(memoryCache);
        }
    }
}
