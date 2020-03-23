using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.FileService.Interface.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class AzureStorageRepositoryService : IRepositoryService
    {
        private readonly string _referenceDataDateFormat = "yyyyMMdHHmm";

        private readonly IAPIConfiguration _configuration;
        private readonly IFileService _fileService;

        public AzureStorageRepositoryService(IAPIConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<IEnumerable<Version>> DesktopApplicationVersions(CancellationToken cancellationToken)
        {
            var desktopVersions = new List<Version>();

            var applicationVersions = await _fileService.GetFileMetaDataAsync(_configuration.Container, _configuration.ApplicationFilePathPrefix, true, cancellationToken);
            var referenceDataVersions = await _fileService.GetFileMetaDataAsync(_configuration.Container, _configuration.RefDataFilePathPrefix, true, cancellationToken);

            if (applicationVersions != null)
            {
                foreach (var fileData in applicationVersions?.Where(x => x.FileName.Contains(_configuration.ApplicationFileNameReference)))
                {
                    var major = SplitVersion(fileData.FileName, 1);
                    var minor = SplitVersion(fileData.FileName, 2);
                    var increment = SplitVersion(fileData.FileName, 3);

                    desktopVersions.Add(new Version
                    {
                        FileName = fileData.FileName,
                        VersionName = BuildVersionString(major, minor, increment),
                        Major = major,
                        Minor = minor,
                        Increment = increment,
                        ReleaseDateTime = fileData.DateCreated.HasValue ? fileData.DateCreated.Value.Date : (System.DateTime?)null,
                        ReferenceDataVersion = await LatestReferenceDataVersionForSchema(minor, referenceDataVersions, cancellationToken)
                    });
                };
            }

            return desktopVersions.OrderBy(o => o.FileName);
        }

        public async Task<ReferenceData> LatestReferenceDataVersionForSchema(int applicationMinorVersion, IEnumerable<FileMetaData> ReferenceDataVersions, CancellationToken cancellationToken)
        {
            var refDataVersions = new List<ReferenceData>();

            if (ReferenceDataVersions != null)
            {
                foreach (var fileData in ReferenceDataVersions?.Where(x => x.FileName.Contains(_configuration.RefDataFileNameReference)))
                {
                    var filename = BuildFileName(fileData.FileName);
                    var major = SplitVersion(fileData.FileName, 1);
                    var minor = SplitVersion(fileData.FileName, 2);
                    var increment = SplitVersion(fileData.FileName, 3);
                    var dateString = filename.Split('.')[4];
                    var date = System.DateTime.TryParseExact(dateString, _referenceDataDateFormat, null, System.Globalization.DateTimeStyles.None, out var releaseDate) ? releaseDate : default(System.DateTime?);

                    if (applicationMinorVersion == 1)
                    {

                    }

                    if (minor == applicationMinorVersion)
                    {
                        refDataVersions.Add(new ReferenceData
                        {
                            FileName = filename,
                            VersionName = BuildVersionString(major, minor, increment, dateString),
                            Major = major,
                            Minor = minor,
                            Increment = increment,
                            ReleaseDateTime = date
                        });
                    }
                }
            }

            return refDataVersions.OrderByDescending(o => o.FileName).FirstOrDefault();
        }

        public async Task<Stream> GetReferenceDataFile(string fileName, CancellationToken cancellationToken)
        {
            return await _fileService.OpenReadStreamAsync(Path.Combine(_configuration.RefDataFilePathPrefix, fileName), _configuration.Container, cancellationToken);
        }

        private string BuildFileName(string fileNamePath)
        {
            return Path.GetFileName(fileNamePath);
        }

        private string BuildVersionString(int major, int minor, int increment)
        {
            return string.Concat(major, ".", minor, ".", increment);
        }

        private string BuildVersionString(int major, int minor, int increment, string dateString)
        {
            return string.Concat(major, ".", minor, ".", increment, ".", dateString);
        }

        private int SplitVersion(string fileName, int index)
        {
            return int.Parse(fileName.Split('.')[index]);
        }
    }
}
