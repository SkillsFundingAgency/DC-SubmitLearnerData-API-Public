using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IProvider<T>
    {
        Task<T> ProvideVersions(CancellationToken cancellationToken);
    }
}
