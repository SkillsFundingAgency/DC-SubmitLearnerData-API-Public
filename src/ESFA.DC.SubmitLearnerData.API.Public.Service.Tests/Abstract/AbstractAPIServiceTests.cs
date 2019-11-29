using ESFA.DC.SubmitLearnerData.API.Public.Model;

namespace ESFA.DC.SubmitLearnerData.API.Public.Service.Tests.Abstract
{
    public class AbstractAPIServiceTests
    {
        protected Version BuildTestVersion(int versionNumber)
        {
            return new Version
            {
                FileName = "DC-ILR-1920-FIS-Desktop.1.0." + versionNumber.ToString() + ".zip",
                ApplicationVersion = "1.0." + versionNumber.ToString(),
                Major = 1,
                Minor = 0,
                Increment = versionNumber,
                ReleaseDateTime = new System.DateTime(2019, 8, 1, 9, 0, 0)
            };
        }
    }
}
