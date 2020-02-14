namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IAPIConfiguration
    {
        string AzureStorageConnectionString { get; }

        string LoggerConnectionString { get; }

        string SubmitLearnerDataDownloadsUrl { get; }

        string Container { get; }

        string RefDataFileNameReference { get; }

        string RefDataFilePathPrefix { get; }

        string ApplicationFileNameReference { get; }

        string ApplicationFilePathPrefix { get; }

        int CacheExpiration { get; }
    }
}
