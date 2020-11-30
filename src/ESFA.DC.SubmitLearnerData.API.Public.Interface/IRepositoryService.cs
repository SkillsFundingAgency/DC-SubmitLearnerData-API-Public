using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IRepositoryService
    {
        Task<bool> IsLatestDesktopApplicationVersion(
            string academicYear,
            Version currentVersion,
            CancellationToken cancellationToken);

        Task<int> LatestReferenceDataVersionAsync(string academicYear, int version, CancellationToken cancellationToken);

        Task<Stream> GetReferenceDataFile(string fileName, CancellationToken cancellationToken);
    }
}