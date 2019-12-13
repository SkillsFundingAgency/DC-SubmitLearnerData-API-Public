using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IRepositoryService
    {
        Task<IEnumerable<Model.Application.Version>> DesktopApplicationVersions();

        Task<IEnumerable<Model.ReferenceData.Version>> DesktopReferenceDataVersions();
    }
}
