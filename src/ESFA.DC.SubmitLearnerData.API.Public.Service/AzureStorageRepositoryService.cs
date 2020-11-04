using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service
{
    public class AzureStorageRepositoryService : IRepositoryService
    {
        private readonly APIConfiguration _configuration;
        private readonly IFileService _fileService;

        public AzureStorageRepositoryService(
            APIConfiguration configuration,
            IFileService fileService)
        {
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<bool> IsNewerDesktopApplicationVersion(
            string academicYear,
            Version currentVersion,
            CancellationToken cancellationToken)
        {
            var applicationVersions = await _fileService.GetFileMetaDataAsync(_configuration.Container, string.Concat(_configuration.ApplicationFilePathPrefix, "/", academicYear), true, cancellationToken, false);

            if (applicationVersions == null)
            {
                return false;
            }

            foreach (var fileData in applicationVersions.Where(x => x.FileName.Contains(_configuration.ApplicationFileNameReference)))
            {
                var version = new Version(SplitVersion(fileData.FileName, 1), SplitVersion(fileData.FileName, 2));

                if(version > currentVersion)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<int> LatestReferenceDataVersionAsync(string academicYear, int version, CancellationToken cancellationToken)
        {
            var referenceDataVersions = await _fileService.GetFileMetaDataAsync(_configuration.Container, string.Concat(_configuration.RefDataFilePathPrefix, "/", academicYear), true, cancellationToken, false);

            if (referenceDataVersions == null)
            {
                return version;
            }

            var latestVersion =  referenceDataVersions
                .Where(x => x.FileName.Contains(_configuration.RefDataFileNameReference))
                .Select(v => SplitVersion(v.FileName, 1))
                .Max();

            return latestVersion > version ? latestVersion : version;
        }

        public async Task<Stream> GetReferenceDataFile(string fileName, CancellationToken cancellationToken)
        {
            return await _fileService.OpenReadStreamAsync(Path.Combine(_configuration.RefDataFilePathPrefix, fileName), _configuration.Container, cancellationToken);
        }

        private int SplitVersion(string fileName, int index)
        {
            try
            {
                return int.Parse(fileName.Split('.')[index]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}