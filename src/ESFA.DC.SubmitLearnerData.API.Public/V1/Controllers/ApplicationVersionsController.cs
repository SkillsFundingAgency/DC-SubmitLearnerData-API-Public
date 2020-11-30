using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ApplicationVersionsController : ControllerBase
    {
        private readonly IApplicationVersionsProvider _applicationVersionsProvider;
        private readonly IPollyPolicies _policies;

        public ApplicationVersionsController(
            IApplicationVersionsProvider applicationVersionsProvider,
            IPollyPolicies policies)
        {
            _applicationVersionsProvider = applicationVersionsProvider;
            _policies = policies;
        } 

        [HttpGet]
        [Route("isLatest/{academicYear}/{major}/{minor}")]
        public async Task<bool> Get(string academicYear, int major, int minor, CancellationToken cancellationToken)
        {
            var version = new Version(major, minor);

            return await _policies.RequestTimeoutAsyncRetryPolicy
                .ExecuteAsync(() => _applicationVersionsProvider.IsLatestVersion(academicYear, version, cancellationToken));
        }

        [HttpGet]
        [Route("downloadLocation")]
        public async Task<ApplicationVersionLocation> Get()
        {
            return await _policies.RequestTimeoutAsyncRetryPolicy
                            .ExecuteAsync(() => _applicationVersionsProvider.DownloadLocation());
        }
    }
}