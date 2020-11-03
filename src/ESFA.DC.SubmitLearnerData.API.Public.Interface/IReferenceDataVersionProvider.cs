using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IReferenceDataVersionProvider
    {
        Task<int> GetLatestVersion(string academicYear, int version, CancellationToken cancellationToken);
    }
}