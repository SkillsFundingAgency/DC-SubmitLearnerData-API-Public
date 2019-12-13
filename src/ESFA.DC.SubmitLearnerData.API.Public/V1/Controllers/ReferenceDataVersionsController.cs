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
        private readonly IPollyPolicies _policies;

        public ReferenceDataVersionsController(IProvider<ReferenceDataVersions> referenceDataVersionsProvider, IPollyPolicies policies)
        {
            _referenceDataVersionsProvider = referenceDataVersionsProvider;
            _policies = policies;
        } 

        // GET api/values
        [HttpGet]
        public async Task<ReferenceDataVersions> Get()
        {
            return await _policies.RequestTimeoutAsyncRetryPolicy.ExecuteAsync(() => _referenceDataVersionsProvider.ProvideVersions());
        }
    }
}
