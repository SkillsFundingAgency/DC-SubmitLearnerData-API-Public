using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ReferenceDataVersionsController : ControllerBase
    {
        private readonly IFileProviderService _fileProviderService;
        private readonly IPollyPolicies _policies;

        public ReferenceDataVersionsController(IFileProviderService fileProviderService, IPollyPolicies policies)
        {
            _fileProviderService = fileProviderService;
            _policies = policies;
        } 

        [HttpGet]
        [Route("{fileReference}")]
        public async Task<FileStreamResult> Get(string fileReference, CancellationToken cancellationToken)
        {
            var fileStream = await _fileProviderService.ProvideFile(fileReference, cancellationToken);

            return new FileStreamResult(fileStream, "application/zip") { FileDownloadName = fileReference };
        }
    }
}
