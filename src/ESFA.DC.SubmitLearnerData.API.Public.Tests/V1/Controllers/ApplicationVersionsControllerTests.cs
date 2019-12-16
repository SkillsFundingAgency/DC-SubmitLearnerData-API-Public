//using System.Collections.Generic;
//using System.Threading.Tasks;
//using ESFA.DC.Logging.Interfaces;
//using ESFA.DC.SubmitLearnerData.API.Public.Model.Application;
//using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
//using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly;
//using ESFA.DC.SubmitLearnerData.API.Public.Utils.Polly.Interface;
//using ESFA.DC.SubmitLearnerData.API.Public.V1.Controllers;
//using FluentAssertions;
//using Microsoft.Extensions.Caching.Memory;
//using Moq;
//using Polly;
//using Xunit;

//namespace ESFA.DC.SubmitLearnerData.API.Public.Tests.V1.Controllers
//{
//    public class ApplicationVersionsControllerTests
//    {
//        [Fact]
//        public async Task Get() 
//        {
//            var appVersions = new ApplicationVersions
//            {
//                Url = "this is a url",
//                Versions = new List<Version>
//                {
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.2.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.3.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.4.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.5.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.6.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) },
//                    new Version{ FileName = "DC-ILR-1920-FIS-Desktop.1.0.7.zip", ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0) }
//                }
//            };            

//            var providerMock = new Mock<IProvider<ApplicationVersions>>();
//            providerMock.Setup(pm => pm.ProvideVersions()).Returns(Task.FromResult(appVersions));

//            var loggerMock = new Mock<ILogger>();
//            var policies = new PollyPolicies(loggerMock.Object);

//            var controller = NewController(providerMock.Object, pollyPolicies: policies);

//            var result = await controller.Get();
//            result.Should().Be(appVersions);
//        }

//        private ApplicationVersionsController NewController(
//            IProvider<ApplicationVersions> applicationVersionsProvider = null,
//            IMemoryCache memoryCache = null, 
//            IPollyPolicies pollyPolicies = null)
//        {
//            return new ApplicationVersionsController(applicationVersionsProvider, pollyPolicies);
//        }
//    }
//}
