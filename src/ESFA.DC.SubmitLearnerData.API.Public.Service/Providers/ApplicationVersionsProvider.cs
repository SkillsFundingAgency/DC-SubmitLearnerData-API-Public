using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Providers
{
    public class ApplicationVersionsProvider : IProvider<ApplicationVersions>
    {
        private const string _cacheEntry = "Versions";

        private readonly IRepositoryService _applicationVersionsRepositoryService;
        private readonly IAPICacheRetrievalService _apiCacheRetrieval;
        private readonly IAPIConfiguration _configuration;

        public ApplicationVersionsProvider(IRepositoryService applicationVersionsRepositoryService, IAPICacheRetrievalService apiCacheRetrieval, IAPIConfiguration configuration)
        {
            _applicationVersionsRepositoryService = applicationVersionsRepositoryService;
            _apiCacheRetrieval = apiCacheRetrieval;
            _configuration = configuration;
        }

        public async Task<ApplicationVersions> ProvideVersions(string academicYear, CancellationToken cancellationToken)
        {
            return await _apiCacheRetrieval.GetOrCreate(string.Concat(_cacheEntry, academicYear), _configuration.CacheExpiration, BuildApplicationVersions(academicYear, cancellationToken));
        }

        private async Task<ApplicationVersions> BuildApplicationVersions(string academicYear, CancellationToken cancellationToken)
        {
            var versions = await _applicationVersionsRepositoryService.DesktopApplicationVersions(academicYear, cancellationToken);

            return new ApplicationVersions
            {
                Url = _configuration.SubmitLearnerDataDownloadsUrl,
                LastUpdated = versions.Max(v => v.ReleaseDateTime),
                Versions = versions.ToList()
            };
        }
    }
}
