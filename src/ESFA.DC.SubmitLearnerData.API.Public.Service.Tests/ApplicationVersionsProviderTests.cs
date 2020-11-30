using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Config;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Providers;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests
{
    public class ApplicationVersionsProviderTests
    {
        [Fact]
        public async Task ProvideVersions()
        {
            var version = new Version(1, 1);
            var academicYear = "1920";
            var cancellationToken = CancellationToken.None;

            var repositoryServiceMock = new Mock<IRepositoryService>();
            repositoryServiceMock
                .Setup(rs => rs.IsLatestDesktopApplicationVersion(academicYear, version, cancellationToken))
                .ReturnsAsync(true);

            var apiCacheProviderMock = new Mock<IAPICacheRetrievalService>();
            apiCacheProviderMock
                .Setup(cp => cp.GetOrCreate("Versions1920", It.IsAny<int>(), It.IsAny<Task<bool>>()))
                .ReturnsAsync(true);

            var config = new APIConfiguration
            {
                SubmitLearnerDataDownloadsUrl = "TestURl",
                CacheExpiration = 5
            };

            var service = NewService(repositoryServiceMock.Object, apiCacheProviderMock.Object, config);
            var result = await service.IsLatestVersion(academicYear, version, cancellationToken);

            result.Should().Be(true);
        }

        private ApplicationVersionsProvider NewService(IRepositoryService repositoryService = null, IAPICacheRetrievalService apiCacheProvider = null, APIConfiguration config = null)
        {
            return new ApplicationVersionsProvider(repositoryService, apiCacheProvider, config);
        }
    }
}
