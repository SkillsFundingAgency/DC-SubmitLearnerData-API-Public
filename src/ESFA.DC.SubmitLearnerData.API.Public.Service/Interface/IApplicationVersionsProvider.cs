using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Model;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Interface
{
    public interface IApplicationVersionsProvider
    {
        Task<ApplicationVersions> ProvideVersions();
    }
}
