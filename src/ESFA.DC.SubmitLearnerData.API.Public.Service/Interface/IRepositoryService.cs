using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Model;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IRepositoryService
    {
        Task<IEnumerable<Version>> DesktopApplicationVersions(CancellationToken cancellationToken);

        Task<Stream> GetReferenceDataFile(string fileName, CancellationToken cancellationToken);
    }
}
