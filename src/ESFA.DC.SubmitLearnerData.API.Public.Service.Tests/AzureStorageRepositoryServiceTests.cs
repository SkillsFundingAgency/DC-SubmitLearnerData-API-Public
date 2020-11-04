using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.FileService.Interface;
using ESFA.DC.FileService.Interface.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
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
            var currentVersion = new Version(1, 1);

            IEnumerable<FileMetaData> fileData = new List<FileMetaData>
            {
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.0.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.1.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.2.zip" },
                new FileMetaData { FileName = "DC-ILR-1920-FIS-Desktop.1.3.zip" }
            };

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock
                .Setup(fs => fs.GetFileMetaDataAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CancellationToken>(), false))
                .Returns(Task.FromResult(fileData));

            var service = NewService(null, fileServiceMock.Object);
            var result = await service.IsNewerDesktopApplicationVersion("1920", currentVersion, CancellationToken.None);

            result.Should().Be(true);
        }

        [Fact]
        public async Task LatestReferenceDataVersionAsync_Returns_Correct_Version()
        {
            IEnumerable<FileMetaData> fileData = new List<FileMetaData>
            {
                new FileMetaData { FileName = "FISReferenceData.5.zip" },
                new FileMetaData { FileName = "FISReferenceData.10.zip" },
                new FileMetaData { FileName = "FISReferenceData.22.zip" },
                new FileMetaData { FileName = "FISReferenceData.345.zip" }
            };

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock
                .Setup(m => m.GetFileMetaDataAsync(It.IsAny<string>(), It.IsAny<string>(), true, It.IsAny<CancellationToken>(), false))
                .ReturnsAsync(fileData);

            var service = NewService(null, fileServiceMock.Object);
            var result = await service.LatestReferenceDataVersionAsync("2021", 2, CancellationToken.None);

            result.Should().Be(345);
        }

        [Fact]
        public async Task GetReferenceDataFile()
        {
            var cancellationToken = new CancellationToken();
            Stream stream = new MemoryStream();

            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(fs => fs.OpenReadStreamAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(stream));

            await NewService(null, fileServiceMock.Object).GetReferenceDataFile("file", cancellationToken);

            fileServiceMock.Verify();
        }

        private AzureStorageRepositoryService NewService(APIConfiguration configuration = null, IFileService fileService = null)
        {
            var config = new APIConfiguration
            {
                Container = "Container",
                ApplicationFileNameReference = "Desktop",
                RefDataFileNameReference = "FISReferenceData",
                RefDataFilePathPrefix = "DesktopReferenceData/1920"
            };

            return new AzureStorageRepositoryService(configuration ?? config, fileService);
        }
    }
}
