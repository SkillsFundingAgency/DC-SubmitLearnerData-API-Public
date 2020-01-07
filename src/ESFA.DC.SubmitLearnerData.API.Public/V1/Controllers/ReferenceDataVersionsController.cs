using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IFileProviderService _fileProviderService;
        private readonly IPollyPolicies _policies;

        public ReferenceDataVersionsController(IFileProviderService fileProviderService, IPollyPolicies policies)
        {
            _fileProviderService = fileProviderService;
            _policies = policies;
        } 

        [HttpGet]
        [Route("Get/{fileReference}")]
        public async Task<FileResult> Get(string fileReference, CancellationToken cancellationToken)
        {
            using (var fileStream = await _fileProviderService.ProvideFile(fileReference, cancellationToken))
            {
                var output = new MemoryStream();
                await fileStream.CopyToAsync(output);

                return File(output, "application/octet-stream", fileReference);
            }
        }
    }
}
