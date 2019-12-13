using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model.Application;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Providers
{
    public class ApplicationVersionsProvider : IProvider<ApplicationVersions>
    {
        private const string _cacheEntry = "Versions";
        private const int _cacheExpiration = 5;

        private readonly IRepositoryService _applicationVersionsRepositoryService;
        private readonly IAPICacheRetrievalService _apiCacheRetrieval;
        private readonly IAPIConfiguration _configuration;

        public ApplicationVersionsProvider(IRepositoryService applicationVersionsRepositoryService, IAPICacheRetrievalService apiCacheRetrieval, IAPIConfiguration configuration)
        {
            _applicationVersionsRepositoryService = applicationVersionsRepositoryService;
            _apiCacheRetrieval = apiCacheRetrieval;
            _configuration = configuration;
        }

        public async Task<ApplicationVersions> ProvideVersions()
        {
            return await _apiCacheRetrieval.GetOrCreate(_cacheEntry, _cacheExpiration, BuildApplicationVersions());
        }

        private async Task<ApplicationVersions> BuildApplicationVersions()
        {
            var versions = await _applicationVersionsRepositoryService.DesktopApplicationVersions();

            return new ApplicationVersions
            {
                Url = _configuration.SubmitLearnerDataDownloadsUrl,
                LastUpdated = versions.Max(v => v.ReleaseDateTime),
                Versions = versions.ToList()
            };
        }
    }
}
