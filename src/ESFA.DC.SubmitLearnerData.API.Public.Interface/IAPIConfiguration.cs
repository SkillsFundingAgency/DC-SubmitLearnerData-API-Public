using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IAPIConfiguration
    {
        string AzureStorageConnectionString { get; }

        string LoggerConnectionString { get; }

        string SubmitLearnerDataDownloadsUrl { get; }

        string Container { get; }
    }
}
