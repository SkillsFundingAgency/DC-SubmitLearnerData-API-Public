using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Tests.V1.Controllers
{
    public class ReferenceDataVersionsControllerTests : ControllerBase
    {
        [Fact]
        public async Task Get()
        {
            int currentReferenceVersion = 1;
            var academicYear = "1920";
            var cancellationToken = CancellationToken.None;
            var fileReference = $"FISReferenceData.{currentReferenceVersion}.zip";
            var filePath = string.Concat(academicYear, "/", fileReference);
            Stream stream = new MemoryStream();

            var refDataProviderMock = new Mock<IReferenceDataVersionProvider>();

            var fileProviderMock = new Mock<IFileProviderService>();
            fileProviderMock
                .Setup(pm => pm.ProvideFile(filePath, cancellationToken))
                .ReturnsAsync(stream);

            var controller = NewController(null, refDataProviderMock.Object, fileProviderMock.Object);

            var result = await controller.Get(academicYear, currentReferenceVersion, cancellationToken);

            result.FileDownloadName.Should().BeEquivalentTo(filePath);

            fileProviderMock.VerifyAll();
        }

        private ReferenceDataVersionsController NewController(APIConfiguration configuration = null, IReferenceDataVersionProvider provider = null, IFileProviderService fileProviderService = null)
        {
            var config = new APIConfiguration
            {
                Container = "Container",
                ApplicationFileNameReference = "Desktop",
                RefDataFileNameReference = "FISReferenceData",
                RefDataFilePathPrefix = "DesktopReferenceData"
            };

            return new ReferenceDataVersionsController(configuration ?? config, provider, fileProviderService);
        }
    }
}