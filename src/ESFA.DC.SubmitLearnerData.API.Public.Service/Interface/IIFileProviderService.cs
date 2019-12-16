using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IFileProviderService
    {
        Task<Stream> ProvideFile(string fileReference, CancellationToken cancellationToken);
    }
}
