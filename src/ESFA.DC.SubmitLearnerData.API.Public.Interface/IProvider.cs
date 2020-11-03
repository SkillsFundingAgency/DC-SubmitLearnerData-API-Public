using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IProvider<T>
    {
        Task<T> ProvideVersions(string acaddemicYear, CancellationToken cancellationToken);
    }
}
