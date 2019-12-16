using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Model.ReferenceData;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ReferenceDataVersionsController : ControllerBase
    {
        private readonly IProvider<ReferenceDataVersions> _referenceDataVersionsProvider;
        private readonly IFileProviderService _fileProviderService;
        private readonly IPollyPolicies _policies;

        public ReferenceDataVersionsController(IProvider<ReferenceDataVersions> referenceDataVersionsProvider, IFileProviderService fileProviderService, IPollyPolicies policies)
        {
            _referenceDataVersionsProvider = referenceDataVersionsProvider;
            _fileProviderService = fileProviderService;
            _policies = policies;
        } 

        // GET api/values
        [HttpGet]
        [Route("")]
        [Route("Get")]
        public async Task<ReferenceDataVersions> Get(CancellationToken cancellationToken)
        {
            return await _policies.RequestTimeoutAsyncRetryPolicy.ExecuteAsync(() => _referenceDataVersionsProvider.ProvideVersions(cancellationToken));
        }

        [HttpGet]
        [Route("GetFile/{fileReference}")]
        public async Task<FileResult> GetFile(string fileReference, CancellationToken cancellationToken)
        {
            using (var fileStream = await _fileProviderService.ProvideFile(fileReference, cancellationToken))
            {
                MemoryStream output = new MemoryStream();
                await fileStream.CopyToAsync(output);

                return File(output, "application/octet-stream", fileReference);
            }
        }
    }
}
