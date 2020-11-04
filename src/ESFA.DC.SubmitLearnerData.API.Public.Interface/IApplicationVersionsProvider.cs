using System;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IApplicationVersionsProvider
    {
        Task<bool> IsNewerVersion(string academicYear, Version version, CancellationToken cancellationToken);
    }
}