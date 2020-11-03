using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.SubmitLearnerData.API.Public.Interface;
using ESFA.DC.SubmitLearnerData.API.Public.Service.Providers;
using Moq;
using Xunit;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests
{
    public class FileProviderServiceTests
    {
        [Fact]
        public async Task ProvideFile()
        {
            var cancellationToken = CancellationToken.None;
            var fileRef = "FileReference.zip";
            Stream stream = new MemoryStream();

            var repositoryServiceMock = new Mock<IRepositoryService>();
            repositoryServiceMock.Setup(rs => rs.GetReferenceDataFile(fileRef, cancellationToken)).Returns(Task.FromResult(stream));

            var provider = await NewProvider(repositoryServiceMock.Object).ProvideFile(fileRef, cancellationToken);

            repositoryServiceMock.VerifyAll();
        }

        private FileProviderService NewProvider(IRepositoryService repositoryService)
        {
            return new FileProviderService(repositoryService);
        }
    }
}