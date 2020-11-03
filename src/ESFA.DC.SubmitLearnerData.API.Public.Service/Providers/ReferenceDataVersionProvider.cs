using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Providers
{
    public class ReferenceDataVersionProvider : IReferenceDataVersionProvider
    {
        private const string CacheEntry = "RefVersions";

        private readonly IRepositoryService _repositoryService;
        private readonly IAPICacheRetrievalService _apiCacheRetrieval;
        private readonly APIConfiguration _configuration;

        public ReferenceDataVersionProvider(
            IRepositoryService repositoryService,
            IAPICacheRetrievalService apiCacheRetrieval,
            APIConfiguration configuration)
        {
            _repositoryService = repositoryService;
            _apiCacheRetrieval = apiCacheRetrieval;
            _configuration = configuration;
        }

        public async Task<int> GetLatestVersion(string academicYear, int version, CancellationToken cancellationToken)
        {
            return await _apiCacheRetrieval.GetOrCreate(
                string.Concat(CacheEntry, academicYear),
                _configuration.CacheExpiration,
                GetVersionFromFiles(academicYear, version, cancellationToken));
        }

        private async Task<int> GetVersionFromFiles(string academicYear, int version, CancellationToken cancellationToken)
        {
            return await _repositoryService.LatestReferenceDataVersionAsync(academicYear, version, cancellationToken);
        }
    }
}