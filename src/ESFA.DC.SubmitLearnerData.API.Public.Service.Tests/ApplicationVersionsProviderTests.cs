using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Model;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
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
            var academicYear = "1920";
            var cancellationToken = CancellationToken.None;
            IEnumerable<Version> versions = new List<Version>
            {
                BuildTestVersion(0),
                BuildTestVersion(1),
                BuildTestVersion(2),
                BuildTestVersion(3),
                BuildTestVersion(4),
                BuildTestVersion(5),
                BuildTestVersion(6),
                BuildTestVersion(7)
            };

            var appVersions = new ApplicationVersions
            {
                Url = "TestUrl",
                LastUpdated = new System.DateTime(2019, 8, 1, 9, 0, 0),
                Versions = versions.ToList()
            };

            var repositoryServiceMock = new Mock<IRepositoryService>();
            repositoryServiceMock.Setup(rs => rs.DesktopApplicationVersions(academicYear, cancellationToken)).Returns(Task.FromResult(versions));

            var apiCacheProviderMock = new Mock<IAPICacheRetrievalService>();
            apiCacheProviderMock.Setup(cp => cp.GetOrCreate("Versions1920", It.IsAny<int>(), It.IsAny<Task<ApplicationVersions>>())).Returns(Task.FromResult(appVersions));

            var configMock = new Mock<IAPIConfiguration>();
            configMock.Setup(c => c.SubmitLearnerDataDownloadsUrl).Returns("TestURl");
            configMock.Setup(c => c.CacheExpiration).Returns(5);

            var result = await NewService(repositoryServiceMock.Object, apiCacheProviderMock.Object, configMock.Object).ProvideVersions(academicYear, cancellationToken);

            result.Should().BeEquivalentTo(appVersions);
        }

        private Version BuildTestVersion(int versionNumber)
        {
            return new Version
            {
                FileName = "DC-ILR-1920-FIS-Desktop.1.0." + versionNumber.ToString() + ".zip",
                VersionName = "1.0." + versionNumber.ToString(),
                Major = 1,
                Minor = 0,
                Increment = versionNumber,
                ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0),
                ReferenceDataVersion = new ReferenceData
                {
                    Major = 1, 
                    Minor = 0,
                    Increment = 1
                }
            };
        }

        private ApplicationVersionsProvider NewService(IRepositoryService repositoryService = null, IAPICacheRetrievalService apiCacheProvider = null, IAPIConfiguration config = null)
        {
            return new ApplicationVersionsProvider(repositoryService, apiCacheProvider, config);
        }
    }
}
