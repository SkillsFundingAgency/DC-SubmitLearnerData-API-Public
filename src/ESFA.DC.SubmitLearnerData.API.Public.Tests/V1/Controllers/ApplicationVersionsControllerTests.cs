using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly;
using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Tests.V1.Controllers
{
    public class ApplicationVersionsControllerTests
    {
        [Fact]
        public async Task Get()
        {
            var academicYear = "1920";
            var currentMajor = 1;
            var currentMinor = 2;
            var cancellationToken = CancellationToken.None;
            var version = new Version(currentMajor, currentMinor);

            var providerMock = new Mock<IApplicationVersionsProvider>();
            providerMock
                .Setup(pm => pm.IsNewerVersion(academicYear, version, cancellationToken))
                .ReturnsAsync(false);

            var policies = new PollyPolicies(Mock.Of<ILogger>());

            var controller = NewController(providerMock.Object, policies);

            var result = await controller.Get(academicYear, currentMajor, currentMinor, cancellationToken);
            result.Should().Be(false);
        }

        private ApplicationVersionsController NewController(
            IApplicationVersionsProvider applicationVersionsProvider = null,
            IPollyPolicies pollyPolicies = null)
        {
            return new ApplicationVersionsController(applicationVersionsProvider, pollyPolicies);
        }
    }
}
