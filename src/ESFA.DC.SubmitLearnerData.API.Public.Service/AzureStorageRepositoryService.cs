using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class AzureStorageRepositoryService : IRepositoryService
    {
        private const string _fileNameReference = "Desktop";
        private readonly IAPIConfiguration _configuration;
        private readonly IAzureContainerService _containerService;

        public AzureStorageRepositoryService(IAPIConfiguration configuration, IAzureContainerService containerService)
        {
            _configuration = configuration;
            _containerService = containerService;
        }

        public async Task<IEnumerable<Version>> DesktopApplicationVersions()
        {
            var desktopVersions = new List<Version>();

            var containerResults = await _containerService.RetrieveContainerBlobs(_configuration.Container);

            if (containerResults != null)
            {
                foreach (var blob in containerResults?.Where(x => x.Name.Contains(_fileNameReference)))
                {
                    var fileName = blob.Name.Split('/')[2];
                    var major = SplitVersion(fileName, 1);
                    var minor = SplitVersion(fileName, 2);
                    var increment = SplitVersion(fileName, 3);

                    desktopVersions.Add(new Version
                    {
                        FileName = fileName,
                        ApplicationVersion = BuildVersionString(major, minor, increment),
                        Major = major,
                        Minor = minor,
                        Increment = increment,
                        ReleaseDateTime = blob.Properties.Created.HasValue ? blob.Properties.Created.Value.Date : (System.DateTime?)null
                    });
                }
            }

            return desktopVersions.OrderBy(o => o.FileName);
        }

        private string BuildVersionString(int major, int minor, int increment)
        {
            return string.Concat(major, ".", minor, ".", increment);
        }

        private int SplitVersion(string fileName, int index)
        {
            return int.Parse(fileName.Split('.')[index]);
        }
    }
}
