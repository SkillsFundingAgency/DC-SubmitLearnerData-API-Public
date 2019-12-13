using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class AzureStorageRepositoryService : IRepositoryService
    {
        private const string _applicationFileNameReference = "Desktop";
        private const string _refdataFileNameReference = "FISReferenceData";
        private readonly IAPIConfiguration _configuration;
        private readonly IAzureContainerService _containerService;

        public AzureStorageRepositoryService(IAPIConfiguration configuration, IAzureContainerService containerService)
        {
            _configuration = configuration;
            _containerService = containerService;
        }

        public async Task<IEnumerable<Model.Application.Version>> DesktopApplicationVersions()
        {
            var desktopVersions = new List<Model.Application.Version>();

            var containerResults = await _containerService.RetrieveContainerBlobs(_configuration.Container);

            if (containerResults != null)
            {
                foreach (var blob in containerResults?.Where(x => x.Name.Contains(_applicationFileNameReference)))
                {
                    var fileName = blob.Name.Split('/')[2];
                    var major = SplitVersion(fileName, 1);
                    var minor = SplitVersion(fileName, 2);
                    var increment = SplitVersion(fileName, 3);

                    desktopVersions.Add(new Model.Application.Version
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

        public async Task<IEnumerable<Model.ReferenceData.Version>> DesktopReferenceDataVersions()
        {
            var refDataVersions = new List<Model.ReferenceData.Version>();

            var containerResults = await _containerService.RetrieveContainerBlobs(_configuration.Container);

            if (containerResults != null)
            {
                foreach (var blob in containerResults?.Where(x => x.Name.Contains(_refdataFileNameReference)))
                {
                    var fileName = blob.Name.Split('/')[3];
                    var major = SplitVersion(fileName, 1);
                    var minor = SplitVersion(fileName, 2);
                    var increment = SplitVersion(fileName, 3);

                    refDataVersions.Add(new Model.ReferenceData.Version
                    {
                        FileName = fileName,
                        Major = major,
                        Minor = minor,
                        Increment = increment,
                        ReleaseDateTime = blob.Properties.Created.HasValue ? blob.Properties.Created.Value.Date : (System.DateTime?)null
                    });
                }
            }

            return refDataVersions.OrderBy(o => o.FileName);
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
