using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.FileService.Interface.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests
{
    public class AzureStorageRepositoryServiceTests
    {
        [Fact]
        public async Task DesktopApplicationVersions()
        {
            var cancellationToken = new CancellationToken();
            IEnumerable<Version> versions = new List<Version>
            {
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip", VersionName = "1.0.0", Major = 1, Minor = 0, Increment = 0 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip", VersionName = "1.0.1", Major = 1, Minor = 0, Increment = 1 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.2.zip", VersionName = "1.0.2", Major = 1, Minor = 0, Increment = 2 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.3.zip", VersionName = "1.0.3", Major = 1, Minor = 0, Increment = 3 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.4.zip", VersionName = "1.0.4", Major = 1, Minor = 0, Increment = 4 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.5.zip", VersionName = "1.0.5", Major = 1, Minor = 0, Increment = 5 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.6.zip", VersionName = "1.0.6", Major = 1, Minor = 0, Increment = 6 },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.7.zip", VersionName = "1.0.7", Major = 1, Minor = 0, Increment = 7 },
            };

            IEnumerable<FileMetaData> fileData = new List<FileMetaData>
            {
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.2.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.3.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.4.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.5.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.6.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.7.zip" },
            };

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(fs => fs.GetFileMetaDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>(), false)).Returns(Task.FromResult(fileData));

            var result = await NewService(fileService: fileServiceMock.Object).DesktopApplicationVersions("1920", cancellationToken);

            result.Should().BeEquivalentTo(versions);
        }

        [Fact]
        public async Task DesktopApplicationVersions_WithRefData()
        {
            var cancellationToken = new CancellationToken();
            IEnumerable<Version> versions = new List<Version>
            {
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip", VersionName = "1.0.0", Major = 1, Minor = 0, Increment = 0,
                    ReferenceDataVersion = new ReferenceData
                    { FileName = "FISReferenceData.1.0.1.202003010900.zip", VersionName = "1.0.1.202003010900", Major = 1, Minor = 0, Increment = 1, ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0) }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip", VersionName = "1.0.1", Major = 1, Minor = 0, Increment = 1,
                    ReferenceDataVersion = new ReferenceData
                    { FileName = "FISReferenceData.1.0.1.202003010900.zip", VersionName = "1.0.1.202003010900", Major = 1, Minor = 0, Increment = 1, ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0) }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.1.0.zip", VersionName = "1.1.0", Major = 1, Minor = 1, Increment = 0,
                    ReferenceDataVersion = new ReferenceData
                    { FileName = "FISReferenceData.1.1.1.202003010900.zip", VersionName = "1.1.1.202003010900", Major = 1, Minor = 1, Increment = 1, ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0) }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.1.1.zip", VersionName = "1.1.1", Major = 1, Minor = 1, Increment = 1,
                    ReferenceDataVersion = new ReferenceData
                    { FileName = "FISReferenceData.1.1.1.202003010900.zip", VersionName = "1.1.1.202003010900", Major = 1, Minor = 1, Increment = 1, ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0) }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.2.0.zip", VersionName = "1.2.0", Major = 1, Minor = 2, Increment = 0,
                    ReferenceDataVersion = new ReferenceData
                    { FileName = "FISReferenceData.1.2.1.202003010900.zip", VersionName = "1.2.1.202003010900", Major = 1, Minor = 2, Increment = 1, ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0) }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.2.1.zip", VersionName = "1.2.1", Major = 1, Minor = 2, Increment = 1,
                    ReferenceDataVersion = new ReferenceData
                    { FileName = "FISReferenceData.1.2.1.202003010900.zip", VersionName = "1.2.1.202003010900", Major = 1, Minor = 2, Increment = 1, ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0) }  },
            };

            IEnumerable<FileMetaData> fileDataAppVersion = new List<FileMetaData>
            {
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.1.0.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.1.1.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.2.0.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.2.1.zip" },
            };

            IEnumerable<FileMetaData> fileDataRefDataVersion = new List<FileMetaData>
            {
                new FileMetaData { FileName = "FISReferenceData.1.0.0.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.0.1.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.1.0.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.1.1.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.2.0.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.2.1.202003010900.zip" },
            };

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.SetupSequence(fs => fs.GetFileMetaDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>(), false)).Returns(Task.FromResult(fileDataAppVersion)).Returns(Task.FromResult(fileDataRefDataVersion));

            var result = await NewService(fileService: fileServiceMock.Object).DesktopApplicationVersions("1920", cancellationToken);

            result.Should().BeEquivalentTo(versions.OrderByDescending(f => f.FileName));
        }

        [Fact]
        public async Task DesktopReferenceDataVersions()
        {
            var cancellationToken = new CancellationToken();

            IEnumerable<FileMetaData> fileData = new List<FileMetaData>
            {
                new FileMetaData { FileName = "FISReferenceData.1.0.0.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.0.1.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.1.0.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.1.1.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.2.0.202003010900.zip" },
                new FileMetaData { FileName = "FISReferenceData.1.2.1.202003010900.zip" },
            };

            var expectedVersion = new ReferenceData
            {
                FileName = "FISReferenceData.1.2.1.202003010900.zip",
                VersionName = "1.2.1.202003010900",
                Major = 1,
                Minor = 2,
                Increment = 1,
                ReleaseDateTime = new System.DateTime(2020, 03, 01, 9, 0, 0)
            };

            var result = await NewService().LatestReferenceDataVersionForSchema(2, fileData, cancellationToken);

            result.Should().BeEquivalentTo(expectedVersion);
        }

        [Fact]
        public async Task GetReferenceDataFile()
        {
            var cancellationToken = new CancellationToken();
            Stream stream = new MemoryStream();

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(fs => fs.OpenReadStreamAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(stream));

            var result = await NewService(fileService: fileServiceMock.Object).GetReferenceDataFile("file", cancellationToken);

            fileServiceMock.Verify();
        }

        private AzureStorageRepositoryService NewService(IAPIConfiguration configuration = null, IFileService fileService = null)
        {
            var configMock = new Mock<IAPIConfiguration>();
            configMock.Setup(c => c.Container).Returns("Container");
            configMock.Setup(c => c.ApplicationFileNameReference).Returns("Desktop");
            configMock.Setup(c => c.RefDataFileNameReference).Returns("FISReferenceData");
            configMock.Setup(c => c.RefDataFilePathPrefix).Returns("DesktopReferenceData/1920");

            return new AzureStorageRepositoryService(configuration ?? configMock.Object, fileService);
        }
    }
}
