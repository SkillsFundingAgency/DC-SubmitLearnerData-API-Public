using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model.ReferenceData;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Providers
{
    public class ReferenceDataVersionsProvider : IProvider<ReferenceDataVersions>
    {
        private const string _cacheEntry = "ReferenceDataVersions";
        private const int _cacheExpiration = 60;

        private readonly IRepositoryService _applicationVersionsRepositoryService;
        private readonly IAPICacheRetrievalService _apiCacheRetrieval;
        private readonly IAPIConfiguration _configuration;

        public ReferenceDataVersionsProvider(IRepositoryService applicationVersionsRepositoryService, IAPICacheRetrievalService apiCacheRetrieval, IAPIConfiguration configuration)
        {
            _applicationVersionsRepositoryService = applicationVersionsRepositoryService;
            _apiCacheRetrieval = apiCacheRetrieval;
            _configuration = configuration;
        }

        public async Task<ReferenceDataVersions> ProvideVersions()
        {
            return await _apiCacheRetrieval.GetOrCreate(_cacheEntry, _cacheExpiration, BuildReferenceDataVersions());
        }

        private async Task<ReferenceDataVersions> BuildReferenceDataVersions()
        {
            var versions = await _applicationVersionsRepositoryService.DesktopReferenceDataVersions();

            return new ReferenceDataVersions
            {
                LastUpdated = versions.Max(v => v.ReleaseDateTime),
                Versions = versions.ToList()
            };
        }
    }
}
