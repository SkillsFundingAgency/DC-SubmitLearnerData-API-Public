using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using System.Collections.Generic;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IAzureContainerService
    {
        Task<IReadOnlyCollection<CloudBlob>> RetrieveContainerBlobs(string containerName);
    }
}