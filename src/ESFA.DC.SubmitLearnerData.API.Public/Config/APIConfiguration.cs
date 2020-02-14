using ESFA.DC.SubmitLearnerData.API.Public.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Config
{
    public class APIConfiguration : IAPIConfiguration
    {
        public string AzureStorageConnectionString { get; set;}

        public string LoggerConnectionString { get; set; }

        public string SubmitLearnerDataDownloadsUrl { get; set; }

        public string Container { get; set; }

        public string RefDataFileNameReference { get; set; }

        public string RefDataFilePathPrefix { get; set; }

        public string ApplicationFileNameReference { get; set; }

        public string ApplicationFilePathPrefix { get; set; }

        public int CacheExpiration { get; set; }
    }
}
