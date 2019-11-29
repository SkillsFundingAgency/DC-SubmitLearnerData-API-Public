using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Model;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IRepositoryService
    {
        Task<IEnumerable<Version>> DesktopApplicationVersions();
    }
}
