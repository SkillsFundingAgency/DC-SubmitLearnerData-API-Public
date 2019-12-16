using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IRepositoryService
    {
        Task<IEnumerable<Model.Application.Version>> DesktopApplicationVersions(CancellationToken cancellationToken);

        Task<IEnumerable<Model.ReferenceData.Version>> DesktopReferenceDataVersions(CancellationToken cancellationToken);

        Task<Stream> GetReferenceDataFile(string fileName, CancellationToken cancellationToken);
    }
}
