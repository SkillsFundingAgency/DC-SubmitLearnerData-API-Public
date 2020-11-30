using ESFA.DC.SubmitLearnerData.API.Public.Model.Config;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Interface
{
    public interface IApplicationVersionsProvider
    {
        Task<bool> IsLatestVersion(string academicYear, Version version, CancellationToken cancellationToken);
        Task<ApplicationVersionLocation> DownloadLocation();
    }
}