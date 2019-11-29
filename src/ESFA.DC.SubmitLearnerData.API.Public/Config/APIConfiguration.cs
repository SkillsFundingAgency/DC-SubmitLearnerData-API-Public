using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Config
{
    public class APIConfiguration : IAPIConfiguration
    {
        private const string LoggerConnectionStringProp = "LoggerConnectionString";
        private const string AzureStorageConnectionStringProp = "AzureStorageConnectionString";
        private const string SubmitLearnerDataDownloadsUrlProp = "SubmitLearnerDataDownloadsUrl";
        private const string ContainerProp = "Container";

        public string AzureStorageConnectionString { get; set;}

        public string LoggerConnectionString { get; set; }

        public string SubmitLearnerDataDownloadsUrl { get; set; }

        public string Container { get; set; }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            AzureStorageConnectionString = ConfigurationManager.AppSettings[AzureStorageConnectionStringProp];
            LoggerConnectionString = ConfigurationManager.AppSettings[LoggerConnectionStringProp];
            SubmitLearnerDataDownloadsUrl = ConfigurationManager.AppSettings[SubmitLearnerDataDownloadsUrlProp];
            Container = ConfigurationManager.AppSettings[ContainerProp];
        }
    }
}
