using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
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
            var cancellationToken = CancellationToken.None;
            var fileReference = "Filename.zip";
            Stream stream = new MemoryStream();

            var providerMock = new Mock<IFileProviderService>();
            providerMock.Setup(pm => pm.ProvideFile(fileReference, cancellationToken)).Returns(Task.FromResult(stream));

            var loggerMock = new Mock<ILogger>();
            var policies = new PollyPolicies(loggerMock.Object);

            var controller = NewController(providerMock.Object, policies);

            using (var fileStream = await providerMock.Object.ProvideFile(fileReference, cancellationToken))
            {
                var result = await controller.Get(fileReference, cancellationToken);

                result.FileDownloadName.Should().BeEquivalentTo(fileReference);

                providerMock.VerifyAll();
            }               
        }

        private ReferenceDataVersionsController NewController(
            IFileProviderService fileProviderService = null,
            IPollyPolicies pollyPolicies = null)
        {
            return new ReferenceDataVersionsController(fileProviderService, pollyPolicies);
        }
    }
}
