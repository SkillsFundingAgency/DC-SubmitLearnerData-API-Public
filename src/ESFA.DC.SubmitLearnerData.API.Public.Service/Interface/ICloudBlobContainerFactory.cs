using Microsoft.Azure.Storage.Blob;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface ICloudBlobContainerFactory
    {
        CloudBlobContainer Build(string containerName, string connectionString);
    }
}
