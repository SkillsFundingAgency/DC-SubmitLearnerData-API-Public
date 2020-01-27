using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
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
            var cancellationToken = CancellationToken.None;

            var versions = new List<Version>
            {
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip", VersionName = "1.0.0", Major = 1, Minor = 0, Increment = 0,
                    ReferenceDataVersion = new ReferenceData { FileName = "FISReferenceData.1.0.1.zip", VersionName = "1.0.1", Major = 1, Minor = 0, Increment = 1, }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip", VersionName = "1.0.1", Major = 1, Minor = 0, Increment = 1,
                    ReferenceDataVersion = new ReferenceData { FileName = "FISReferenceData.1.0.1.zip", VersionName = "1.0.1", Major = 1, Minor = 0, Increment = 1, }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.1.0.zip", VersionName = "1.1.0", Major = 1, Minor = 1, Increment = 0,
                    ReferenceDataVersion = new ReferenceData { FileName = "FISReferenceData.1.1.1.zip", VersionName = "1.1.1", Major = 1, Minor = 1, Increment = 1, }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.1.1.zip", VersionName = "1.1.1", Major = 1, Minor = 1, Increment = 1,
                    ReferenceDataVersion = new ReferenceData { FileName = "FISReferenceData.1.1.1.zip", VersionName = "1.1.1", Major = 1, Minor = 1, Increment = 1, }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.2.0.zip", VersionName = "1.2.0", Major = 1, Minor = 2, Increment = 0,
                    ReferenceDataVersion = new ReferenceData { FileName = "FISReferenceData.1.2.1.zip", VersionName = "1.2.1", Major = 1, Minor = 2, Increment = 1, }  },
                new Version { FileName = "DC-ILR-1920-FIS-Desktop.1.2.1.zip", VersionName = "1.2.1", Major = 1, Minor = 2, Increment = 1,
                    ReferenceDataVersion = new ReferenceData { FileName = "FISReferenceData.1.2.1.zip", VersionName = "1.2.1", Major = 1, Minor = 2, Increment = 1, }  },
            };

            var appVersions = new ApplicationVersions
            {
                Url = "this is a url",
                Versions = versions
            };

            var providerMock = new Mock<IProvider<ApplicationVersions>>();
            providerMock.Setup(pm => pm.ProvideVersions(cancellationToken)).Returns(Task.FromResult(appVersions));

            var loggerMock = new Mock<ILogger>();
            var policies = new PollyPolicies(loggerMock.Object);

            var controller = NewController(providerMock.Object, pollyPolicies: policies);

            var result = await controller.Get(cancellationToken);
            result.Should().Be(appVersions);
        }

        private ApplicationVersionsController NewController(
            IProvider<ApplicationVersions> applicationVersionsProvider = null,
            IMemoryCache memoryCache = null,
            IPollyPolicies pollyPolicies = null)
        {
            return new ApplicationVersionsController(applicationVersionsProvider, pollyPolicies);
        }
    }
}
