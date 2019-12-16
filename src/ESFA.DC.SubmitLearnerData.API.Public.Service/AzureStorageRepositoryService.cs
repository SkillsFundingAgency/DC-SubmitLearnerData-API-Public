using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class AzureStorageRepositoryService : IRepositoryService
    {
        private const string _applicationFileNameReference = "Desktop";
        private const string _refdataFileNameReference = "FISReferenceData";
        private const string _filePathPrefix = "desktop/1920";
        private const string _refDataPathPrefix = "desktop/1920/referencedata/";
        private readonly IAPIConfiguration _configuration;
        private readonly IAzureContainerService _containerService;
        private readonly IFileService _fileService;

        public AzureStorageRepositoryService(IAPIConfiguration configuration, IAzureContainerService containerService, IFileService fileService)
        {
            _configuration = configuration;
            _containerService = containerService;
            _fileService = fileService;
        }

        public async Task<IEnumerable<Model.Application.Version>> DesktopApplicationVersions(CancellationToken cancellationToken)
        {
            var desktopVersions = new List<Model.Application.Version>();

            var containerResults = await _fileService.GetFileReferencesAsync(_configuration.Container, _filePathPrefix, true, cancellationToken);

            if (containerResults != null)
            {
                foreach (var fileNamePath in containerResults?.Where(x => x.Contains(_applicationFileNameReference)))
                {
                    var fileName = fileNamePath.Split('/')[2];
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
                       // ReleaseDateTime = blob.Properties.Created.HasValue ? blob.Properties.Created.Value.Date : (System.DateTime?)null
                    });
                };
            }

            return desktopVersions.OrderBy(o => o.FileName);
        }

        public async Task<IEnumerable<Model.ReferenceData.Version>> DesktopReferenceDataVersions(CancellationToken cancellationToken)
        {
            var refDataVersions = new List<Model.ReferenceData.Version>();

            var containerResults = await _fileService.GetFileReferencesAsync(_configuration.Container, _filePathPrefix, true, cancellationToken);

            if (containerResults != null)
            {
                foreach (var fileNamePath in containerResults?.Where(x => x.Contains(_refdataFileNameReference)))
                {
                    var fileName = fileNamePath.Split('/')[3];
                    var major = SplitVersion(fileName, 1);
                    var minor = SplitVersion(fileName, 2);
                    var increment = SplitVersion(fileName, 3);

                    refDataVersions.Add(new Model.ReferenceData.Version
                    {
                        FileName = fileName,
                        Major = major,
                        Minor = minor,
                        Increment = increment,
                        //ReleaseDateTime = blob.Properties.Created.HasValue ? blob.Properties.Created.Value.Date : (System.DateTime?)null
                    });
                }
            }

            return refDataVersions.OrderBy(o => o.FileName);
        }

        public async Task<Stream> GetReferenceDataFile(string fileName, CancellationToken cancellationToken)
        {
            return await _fileService.OpenReadStreamAsync(_refDataPathPrefix + fileName, _configuration.Container, cancellationToken);
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
