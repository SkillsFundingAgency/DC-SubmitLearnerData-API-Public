using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/")]
    public class ReferenceDataVersionsController : ControllerBase
    {
        private readonly APIConfiguration _configuration;
        private readonly IReferenceDataVersionProvider _provider;
        private readonly IFileProviderService _fileProviderService;

        public ReferenceDataVersionsController(
            APIConfiguration configuration,
            IReferenceDataVersionProvider provider,
            IFileProviderService fileProviderService)
        {
            _configuration = configuration;
            _provider = provider;
            _fileProviderService = fileProviderService;
        }

        [HttpGet("latestVersion/{academicYear}/{currentRefDataVersion}")]
        public async Task<int> LatestReferenceDataVersionAsync(string academicYear, int currentRefDataVersion, CancellationToken cancellationToken)
        {
            return await _provider.GetLatestVersion(academicYear, currentRefDataVersion, cancellationToken);
        }

        [HttpGet]
        [Route("file/{academicYear}/{refDataVersion}")]
        public async Task<FileStreamResult> Get(string academicYear, int refDataVersion, CancellationToken cancellationToken)
        {
            var fileReference = string.Concat(academicYear, "/", _configuration.RefDataFileNameReference, ".", refDataVersion, ".zip");

            var fileStream = await _fileProviderService.ProvideFile(fileReference, cancellationToken);

            return new FileStreamResult(fileStream, "application/zip")
            {
                FileDownloadName = Path.GetFileName(fileReference)
            };
        }
    }
}
