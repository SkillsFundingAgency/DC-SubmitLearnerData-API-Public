using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Factory
{
    public class CloudBlobContainerFactory : ICloudBlobContainerFactory
    {
        public CloudBlobContainer Build(string containerName, string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            return container;
        }
    }
}
