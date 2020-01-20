using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Providers
{
    public class FileProviderService : IFileProviderService
    {
        private readonly IRepositoryService _applicationVersionsRepositoryService;

        public FileProviderService(IRepositoryService applicationVersionsRepositoryService)
        {
            _applicationVersionsRepositoryService = applicationVersionsRepositoryService;
        }

        public async Task<Stream> ProvideFile(string fileReference, CancellationToken cancellationToken)
        {
            return await _applicationVersionsRepositoryService.GetReferenceDataFile(fileReference, cancellationToken);
        }
    }
}
