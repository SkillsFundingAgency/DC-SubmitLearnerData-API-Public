//using System.Collections.Generic;
//using System.Threading.Tasks;
//using ESFA.DC.SubmitLearnerData.API.Public.Interface;
//using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
//using FluentAssertions;
//using Microsoft.Azure.Storage.Blob;
//using Moq;
//using Xunit;

//namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests
//{
//    public class AzureStorageRepositoryServiceTests
//    {
//        [Fact]
//        public async Task DesktopApplicationVersions()
//        {
//            IEnumerable<Model.Application.Version> versions = new List<Model.Application.Version>
//            {
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.0.zip", ApplicationVersion = "1.0.0", Major = 1, Minor = 0, Increment = 0 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.1.zip", ApplicationVersion = "1.0.1", Major = 1, Minor = 0, Increment = 1 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.2.zip", ApplicationVersion = "1.0.2", Major = 1, Minor = 0, Increment = 2 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.3.zip", ApplicationVersion = "1.0.3", Major = 1, Minor = 0, Increment = 3 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.4.zip", ApplicationVersion = "1.0.4", Major = 1, Minor = 0, Increment = 4 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.5.zip", ApplicationVersion = "1.0.5", Major = 1, Minor = 0, Increment = 5 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.6.zip", ApplicationVersion = "1.0.6", Major = 1, Minor = 0, Increment = 6 },
//                new Model.Application.Version { FileName = "DC-ILR-1920-FIS-Desktop.1.0.7.zip", ApplicationVersion = "1.0.7", Major = 1, Minor = 0, Increment = 7 },
//            };

//            IReadOnlyCollection<CloudBlob> blobItems = new List<CloudBlob>
//            {
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.0.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.1.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.2.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.3.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.4.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.5.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.6.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/DC-ILR-1920-FIS-Desktop.1.0.7.zip")),
//            };

//            var configMock = new Mock<IAPIConfiguration>();
//            configMock.Setup(c => c.Container).Returns("Container");

//            var containerServiceMock = new Mock<IAzureContainerService>();
//            containerServiceMock.Setup(cs => cs.RetrieveContainerBlobs(configMock.Object.Container)).Returns(Task.FromResult(blobItems));

//            var result = await NewService(configMock.Object, containerServiceMock.Object).DesktopApplicationVersions();

//            result.Should().BeEquivalentTo(versions);
//        }

//        [Fact]
//        public async Task DesktopReferenceDataVersions()
//        {
//            IEnumerable<Model.ReferenceData.Version> versions = new List<Model.ReferenceData.Version>
//            {
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.0.zip", Major = 1, Minor = 0, Increment = 0 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.1.zip", Major = 1, Minor = 0, Increment = 1 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.2.zip", Major = 1, Minor = 0, Increment = 2 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.3.zip", Major = 1, Minor = 0, Increment = 3 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.4.zip", Major = 1, Minor = 0, Increment = 4 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.5.zip", Major = 1, Minor = 0, Increment = 5 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.6.zip", Major = 1, Minor = 0, Increment = 6 },
//                new Model.ReferenceData.Version { FileName = "FISReferenceData.1.0.7.zip", Major = 1, Minor = 0, Increment = 7 },
//            };

//            IReadOnlyCollection<CloudBlob> blobItems = new List<CloudBlob>
//            {
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.0.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.1.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.2.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.3.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.4.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.5.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.6.zip")),
//                new CloudBlob(new System.Uri("https://test.blob.core.windows.net/blob/desktop/1920/referencedata/FISReferenceData.1.0.7.zip")),
//            };
//            var configMock = new Mock<IAPIConfiguration>();
//            configMock.Setup(c => c.Container).Returns("Container");

//            var containerServiceMock = new Mock<IAzureContainerService>();
//            containerServiceMock.Setup(cs => cs.RetrieveContainerBlobs(configMock.Object.Container)).Returns(Task.FromResult(blobItems));

//            var result = await NewService(configMock.Object, containerServiceMock.Object).DesktopReferenceDataVersions();

//            result.Should().BeEquivalentTo(versions);
//        }

//        private AzureStorageRepositoryService NewService(IAPIConfiguration configuration = null, IAzureContainerService containerService = null)
//        {
//            return new AzureStorageRepositoryService(configuration, containerService);
//        }
//    }
//}
