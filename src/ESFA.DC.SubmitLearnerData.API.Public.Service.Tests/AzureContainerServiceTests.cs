using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Interface;
using FluentAssertions;
using Microsoft.Azure.Storage.Blob;
using Moq;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests
{
    public class AzureContainerServiceTests
    {
        [Fact]
        public async Task RetrieveContainerBlobs()
        {
            BlobContinuationToken continuationToken = null;
            var containerName = "Container";
            var blobItems = new List<IListBlobItem>
            {
                new CloudBlob(new Uri("https://test.blob.core.windows.net/blob/desktop/1920/File1.zip")),
                new CloudBlob(new Uri("https://test.blob.core.windows.net/blob/desktop/1920/File2.zip")),
                new CloudBlob(new Uri("https://test.blob.core.windows.net/blob/desktop/1920/File3.zip")),
            };

            var cloudBlobContainerResponse = new BlobResultSegment(blobItems, continuationToken);

            var cloudBlobContainer = new Mock<CloudBlobContainer>(new Uri("https://test.blob.core.windows.net/blob"));
            cloudBlobContainer.Setup(c => c.ListBlobsSegmentedAsync(string.Empty, true, BlobListingDetails.None, new int?(), continuationToken, null, null)).Returns(Task.FromResult(cloudBlobContainerResponse));

            var blobFactoryMock = new Mock<ICloudBlobContainerFactory>();
            blobFactoryMock.Setup(fb => fb.Build(containerName, It.IsAny<string>())).Returns(cloudBlobContainer.Object);

            var configMock = new Mock<IAPIConfiguration>();
            configMock.Setup(c => c.SubmitLearnerDataDownloadsUrl).Returns("TestURl");

            var result = await NewService(configMock.Object, blobFactoryMock.Object).RetrieveContainerBlobs(containerName);

            result.Should().BeEquivalentTo(blobItems);
        }

        private AzureContainerService NewService(IAPIConfiguration configuration = null, ICloudBlobContainerFactory blobFactory = null)
        {
            return new AzureContainerService(configuration, blobFactory);
        }
    }
}
