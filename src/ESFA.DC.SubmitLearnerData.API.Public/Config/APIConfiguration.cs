using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Config
{
    public class APIConfiguration : IAPIConfiguration
    {
        public string AzureStorageConnectionString { get; set;}

        public string LoggerConnectionString { get; set; }

        public string SubmitLearnerDataDownloadsUrl { get; set; }

        public string Container { get; set; }
    }
}
