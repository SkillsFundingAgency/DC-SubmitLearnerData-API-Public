using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using Microsoft.Azure.Storage.Blob;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class AzureContainerService : IAzureContainerService
    {
        private readonly IAPIConfiguration _configuration;
        private readonly ICloudBlobContainerFactory _blobFactory;

        public AzureContainerService(IAPIConfiguration configuration, ICloudBlobContainerFactory blobFactory)
        {
            _configuration = configuration;
            _blobFactory = blobFactory;
        }

        public async Task<IReadOnlyCollection<CloudBlob>> RetrieveContainerBlobs(string containerName)
        {
            BlobContinuationToken continuationToken = null;

            var cloudContainer = _blobFactory.Build(containerName, _configuration.AzureStorageConnectionString);

            var response = await cloudContainer.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.None, new int?(), continuationToken, null, null);

            continuationToken = response.ContinuationToken;
            foreach (var blob in response.Results)
            {
                //yield return blob;
            } while (continuationToken != null) ;

            return response?.Results?.Select(result => (CloudBlob)result).ToList();
        }
    }
}
