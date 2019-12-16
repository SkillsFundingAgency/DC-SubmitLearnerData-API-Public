using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Model.Application;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ApplicationVersionsController : ControllerBase
    {
        private readonly IProvider<ApplicationVersions> _applicationVersionsProvider;
        private readonly IPollyPolicies _policies;

        public ApplicationVersionsController(IProvider<ApplicationVersions> applicationVersionsProvider, IPollyPolicies policies)
        {
            _applicationVersionsProvider = applicationVersionsProvider;
            _policies = policies;
        } 

        // GET api/values
        [HttpGet]
        public async Task<ApplicationVersions> Get(CancellationToken cancellationToken)
        {
            return await _policies.RequestTimeoutAsyncRetryPolicy.ExecuteAsync(() => _applicationVersionsProvider.ProvideVersions(cancellationToken));
        }
    }
}
