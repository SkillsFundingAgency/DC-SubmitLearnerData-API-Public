using System.Net.Http;
using Polly.Retry;

namespace ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface
{
    public interface IPollyPolicies
    {
        RetryPolicy FileSystemRetryPolicy { get; }

        AsyncRetryPolicy RequestTimeoutAsyncRetryPolicy { get; }
    }
}
